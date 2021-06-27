module Functional.NetCDF.Samples.FSharp

open System
open NUnit.Framework
open FsUnit
open BeardedPlatypus.Functional.NetCDF

/// <summary>
/// The <see cref="TimeQuery"/> implements the <see cref="IQuery"/> to obtain
/// the Time component of a NetCDF UGRID file.
/// It retrieves the start time and time steps.
/// </summary>
type TimeQuery () =
    let interpretUnitsString (unitsString: string) : string * DateTime =
        match unitsString.Split([| " since " |], StringSplitOptions.RemoveEmptyEntries) with
        | [| ts; dt |] -> (ts, DateTime.Parse dt)
        | _ -> failwith "Invalid units string"

    member val public StartTime : DateTime = DateTime() with get, set
    member val public TimeSteps : TimeSpan list = [] with get, set

    interface IQuery with
        member this.Execute (repository: IRepository) : unit =
            
            // Retrieve the id by finding the first component that defines a time component.
            // In the case of UGRID files, this is a variable that has a standard_name attribute
            // set to "time".
            let id = (repository.RetrieveVariablesWithAttributeWithValue ("standard_name", "time")) |> Seq.head


            // The time variable should always have a "units" attribute, that defines a string 
            // as "<time-quantity> since <date>", for example "seconds since 2021-06-14 00:00:00 +00:00"
            let unitsString = repository.RetrieveVariableAttribute (id, "units")
            let (timeStep: string, startTime: DateTime) = interpretUnitsString (unitsString.Values |> Seq.head)

            this.StartTime <- startTime

            // The "time" variable is a 1D sequence of doubles, we will convert this to time spans
            // by using the <time-quantity> obtained from the units string.
            let value : IVariableValue<double> = repository.RetrieveVariableValue<double> id

            let fTimeStep = 
                match timeStep with 
                | "seconds" -> TimeSpan.FromSeconds
                | "hours"   -> TimeSpan.FromHours
                | "days"    -> TimeSpan.FromDays
                | _         -> failwith "Invalid timestep"

            this.TimeSteps <- value.Values 
                              |> Seq.map fTimeStep
                              |> Seq.toList

/// <summary>
/// The <see cref="QueryTime"/> sample shows how to implement a simple NetCDF query
/// to retrieve the time data, the start time and time steps, from a UGRID map file.
///
/// It does so by implementing the <see cref="IQuery"/> interface while exposing the
/// relevant data as properties.
/// The query is executed within the actual test, and the data is compared with the
/// expected data.
/// </summary>
[<TestFixture>]
type QueryTime () =
    [<Test>]
    member this.``Query Time Sample.`` () =
        // Execute the query
        let query = TimeQuery()
        Service.Query ("./example-data/map.nc", query :> IQuery)

        // Validate the results
        let expectedStartTime = DateTime.Parse "2021-06-14 00:00:00 +00:00"
        query.StartTime |> should equal expectedStartTime

        let expectedTimeSteps : TimeSpan list = 
            [ for i in 0 .. 72 -> ((double) i * 1200.0) |> TimeSpan.FromSeconds ]
        query.TimeSteps |> should equal expectedTimeSteps
