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

    static member GetRetrieveAttributeValueString () : seq<TestCaseData> = 
        seq { TestCaseData("mesh2d", "cf_role", seq { "mesh_topology" }) 
              TestCaseData("mesh2d", "face_dimension", seq { "mesh2d_nFaces" })
              TestCaseData("mesh2d_ucmag", "mesh", seq { "mesh2d" })
              TestCaseData("time", "standard_name", seq { "time" })
              TestCaseData("time", "units", seq { "seconds since 2001-01-01 00:00:00 +00:00" })
            }

    [<Test>]
    [<TestCaseSource("GetRetrieveAttributeValueString")>]
    member this.``RetrieveVariableAttribute with a Name returns the correct values for strings.`` (variableName: string,
                                                                                                attributeName: string,
                                                                                                expectedValue : seq<string>) =
        // Setup
        use file: Managed.IFile = Managed.NetCDF.OpenExisting "./test-data/map.nc"
        let repo : IRepository = Repository(file) :> IRepository

        // Call
        let result = repo.RetrieveVariableAttribute<string> (variableName, attributeName)

        // Assert
        result.Values |> should equal expectedValue
        
    static member GetRetrieveAttributeValueInt () : seq<TestCaseData> = 
        seq { TestCaseData("mesh2d", "topology_dimension", seq { 2 }) 
              TestCaseData("projected_coordinate_system", "epsg", seq { 0 })
            }

    [<Test>]
    [<TestCaseSource("GetRetrieveAttributeValueInt")>]
    member this.``RetrieveVariableAttribute with a Name returns the correct values for ints.`` (variableName: string,
                                                                                             attributeName: string,
                                                                                             expectedValue : seq<int>) =
        // Setup
        use file: Managed.IFile = Managed.NetCDF.OpenExisting "./test-data/map.nc"
        let repo : IRepository = Repository(file) :> IRepository

        // Call
        let result = repo.RetrieveVariableAttribute<int> (variableName, attributeName)

        // Assert
        result.Values |> should equal expectedValue
        
    static member GetRetrieveAttributeValueDouble () : seq<TestCaseData> = 
        seq { TestCaseData("mesh2d_czu", "_FillValue", seq { -999.0 }) 
              TestCaseData("projected_coordinate_system", "longitude_of_prime_meridian", seq { 0.0 })
              TestCaseData("projected_coordinate_system", "semi_major_axis", seq { 6378137.0 })
              TestCaseData("projected_coordinate_system", "semi_minor_axis", seq { 6356752.314245 })
              TestCaseData("projected_coordinate_system", "inverse_flattening", seq { 298.257223563 })
            }

    [<Test>]
    [<TestCaseSource("GetRetrieveAttributeValueDouble")>]
    member this.``RetrieveVariableAttribute with a Name returns the correct values for doubles.`` (variableName: string,
                                                                                                attributeName: string,
                                                                                                expectedValue : seq<double>) =
        // Setup
        use file: Managed.IFile = Managed.NetCDF.OpenExisting "./test-data/map.nc"
        let repo : IRepository = Repository(file) :> IRepository

        // Call
        let result = repo.RetrieveVariableAttribute<double> (variableName, attributeName)

        // Assert
        result.Values |> should equal expectedValue

    [<Test>]
    [<TestCase(0, "projected_coordinate_system")>]
    [<TestCase(1, "mesh2d")>]
    [<TestCase(2, "mesh2d_node_x")>]
    [<TestCase(3, "mesh2d_node_y")>]
    [<TestCase(4, "mesh2d_node_z")>]
    member this.``RetrieveVariableName returns the correct variable name.`` (variableID: int, 
                                                                             expectedVariableName: string) =
        // Setup
        use file: Managed.IFile = Managed.NetCDF.OpenExisting "./test-data/map.nc"
        let repo : IRepository = Repository(file) :> IRepository
        let variableID = VariableID (Common.VarID variableID)

        // Call
        let result = repo.RetrieveVariableName variableID

        // Assert
        result |> should equal expectedVariableName
        
