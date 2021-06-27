namespace BeardedPlatypus.Functional.NetCDF.Tests

open NUnit.Framework
open FsUnit

open BeardedPlatypus.Functional.NetCDF
open BeardedPlatypus.Functional.NetCDF.Managed

[<TestFixture>]
type Repository_Test () =
    [<Test>]
    member this.``RetrieveVariableValue with a VariableID returns the correct values for doubles.`` () =
        // Setup
        use file: Managed.IFile = Managed.NetCDF.OpenExisting "./test-data/map.nc"
        let repo : IRepository = Repository(file) :> IRepository

        // Call
        let result = repo.RetrieveVariableValue<double> (VariableID(Common.VarID 17))

        // Assert
        result.Shape |> should equal (seq { 73 })

        let expectedTimeSteps : seq<double> = seq { for i in 0 .. 72 -> (double) i * 1200.0 }
        result.Values |> should equal expectedTimeSteps
        

    [<Test>]
    member this.``RetrieveVariableValue with a Name returns the correct values for doubles.`` () =
        // Setup
        use file: Managed.IFile = Managed.NetCDF.OpenExisting "./test-data/map.nc"
        let repo : IRepository = Repository(file) :> IRepository

        // Call
        let result = repo.RetrieveVariableValue<double> "time"

        // Assert
        result.Shape |> should equal (seq { 73 })

        let expectedTimeSteps : seq<double> = seq { for i in 0 .. 72 -> (double) i * 1200.0 }
        result.Values |> should equal expectedTimeSteps

    [<Test>]
    member this.``RetrieveVariableValue with a VariableID returns the correct values for ints.`` () =
        // Setup
        use file: Managed.IFile = Managed.NetCDF.OpenExisting "./test-data/map.nc"
        let repo : IRepository = Repository(file) :> IRepository

        // Call
        let result = repo.RetrieveVariableValue<int> (VariableID(Common.VarID 9))

        // Assert
        result.Shape |> should equal (seq { 60; 2 })

        let expectedTimeSteps : seq<int> = seq { 1; 2; 1; 3; 2; 4; 2; 5; 3; 5; }
        (Seq.take 10 result.Values) |> should equal expectedTimeSteps

    [<Test>]
    member this.``RetrieveVariableValue with a Name returns the correct values for ints.`` () =
        // Setup
        use file: Managed.IFile = Managed.NetCDF.OpenExisting "./test-data/map.nc"
        let repo : IRepository = Repository(file) :> IRepository

        // Call
        let result = repo.RetrieveVariableValue<int> "mesh2d_edge_faces"

        // Assert
        result.Shape |> should equal (seq { 60; 2 })

        let expectedTimeSteps : seq<int> = seq { 1; 2; 1; 3; 2; 4; 2; 5; 3; 5; }
        (Seq.take 10 result.Values) |> should equal expectedTimeSteps

