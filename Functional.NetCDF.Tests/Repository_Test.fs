namespace BeardedPlatypus.Functional.NetCDF.Tests

open NUnit.Framework
open FsUnit

open BeardedPlatypus.Functional.NetCDF
open BeardedPlatypus.Functional.NetCDF.Managed

[<TestFixture>]
type Repository_Test () =
    [<Test>]
    member this.``RetrieveVariableValue with a VariableID returns the correct values.`` () =
        // Setup
        use file: Managed.IFile = Managed.NetCDF.OpenExisting "./test-data/map.nc"
        let repo : IRepository = Repository(file) :> IRepository

        // Call
        let result = repo.RetrieveVariableValue (VariableID(Common.VarID 17))

        // Assert
        let expectedTimeSteps : seq<double> = seq { for i in 0 .. 72 -> (double) i * 1200.0 }
        result |> should equal expectedTimeSteps
        

    [<Test>]
    member this.``RetrieveVariableValue with a Name returns the correct values.`` () =
        // Setup
        use file: Managed.IFile = Managed.NetCDF.OpenExisting "./test-data/map.nc"
        let repo : IRepository = Repository(file) :> IRepository

        // Call
        let result = repo.RetrieveVariableValue "time"

        // Assert
        let expectedTimeSteps : seq<double> = seq { for i in 0 .. 72 -> (double) i * 1200.0 }
        result |> should equal expectedTimeSteps

