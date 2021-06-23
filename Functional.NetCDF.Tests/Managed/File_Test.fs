namespace BeardedPlatypus.Functional.NetCDF.Managed.Tests

open NUnit.Framework
open FsUnit

open BeardedPlatypus.Functional.NetCDF.Native.NetCDF
open BeardedPlatypus.Functional.NetCDF.Managed

[<TestFixture>]
type File_Test () =
    let openFile (path: string) : IFile = 
        let mutable id : int = -1
        nc_open("./test-data/map.nc", NCOpenMode.NoWrite, &id) |> ignore

        new File(Common.NcID id) :> IFile

    [<Test>]
    member this.``RetrieveNVariables should retrieve the correct number of variables`` () =
        use ncFile = openFile "./test-data/map.nc"
        let result = ncFile.RetrieveNVariables ()

        let expectedResult: Result<int, NCReturnCode> = Result.Ok 34
        result |> should equal expectedResult

    [<Test>]
    member this.``RetrieveVariableName should retrieve the correct name of variables`` () = 
        use ncFile = openFile "./test-data/map.nc"
        let result = ncFile.RetrieveVariableName (Common.VarID 0)
        
        let expectedResult: Result<string, NCReturnCode> =  Result.Ok "projected_coordinate_system"
        result |> should equal expectedResult

    [<Test>]
    member this.``RetrieveNAttributes should retrieve the correct number of attributes`` () =
        use ncFile = openFile "./test-data/map.nc"
        let result = ncFile.RetrieveNAttributes (Common.VarID 0)

        let expectedResult: Result<int, NCReturnCode> = Result.Ok 11
        result |> should equal expectedResult

    [<Test>]
    member this.``RetrieveAttributeInformation should retrieve the correct attribute name`` () =
        use ncFile = openFile "./test-data/map.nc"
        let result = ncFile.RetrieveAttributeName (Common.VarID 0) 0

        let expectedResult: Result<string, NCReturnCode> = Result.Ok "name"
        result |> should equal expectedResult

    [<Test>]
    member this.``RetrieveAttributeInformation should retrieve the correct attribute type and length`` () =
        use ncFile = openFile "./test-data/map.nc"
        let result = ncFile.RetrieveAttributeInformation (Common.VarID 0) "name"

        let expectedResult: Result<Common.AttributeTypeInformation, NCReturnCode> =
            Result.Ok { Common.AttributeTypeInformation.T = NCType.Char
                        Common.AttributeTypeInformation.Size = 17 
                      }
        result |> should equal expectedResult

    [<Test>]
    [<TestCase("name", "Unknown projected")>]
    [<TestCase("grid_mapping_name", "Unknown projected")>]
    [<TestCase("value", "value is equal to EPSG code")>]
    [<TestCase("EPSG_code", "EPSG:0")>]
    [<TestCase("proj_string", "")>]
    member this.``RetrieveAttributeValueText should retrieve the correct string`` ((attributeName: string), (expectedResultString: string)) =
        use ncFile = openFile "./test-data/map.nc"

        let varId = (Common.VarID 0)
        let result = 
            ncFile.RetrieveAttributeInformation varId attributeName
            |> Result.bind (fun (v: Common.AttributeTypeInformation) -> ncFile.RetrieveAttributeValueText varId attributeName (v.Size - 1) )

        let expectedResult: Result<string,NCReturnCode> = Result.Ok expectedResultString
        result |> should equal expectedResult
        

    [<Test>]
    member this.``RetrieveAttributeValueInt should retrieve the correct int array`` () =
        use ncFile = openFile "./test-data/map.nc"

        let varId = (Common.VarID 0)
        let attributeName = "epsg"

        let result = 
            ncFile.RetrieveAttributeInformation varId attributeName
            |> Result.bind (fun (v: Common.AttributeTypeInformation) -> ncFile.RetrieveAttributeValueInt varId attributeName v.Size )

        let expectedResult: Result<int32[],NCReturnCode> = Result.Ok [| 0 |]
        result |> should equal expectedResult

    [<Test>]
    [<TestCase("longitude_of_prime_meridian", 0.0)>]
    [<TestCase("semi_major_axis", 6378137.0)>]
    [<TestCase("semi_minor_axis", 6356752.314245)>]
    [<TestCase("inverse_flattening", 298.257223563)>]
    member this.``RetrieveAttributeDouble should retrieve the correct double array`` ((attributeName: string), (expectedResultValue: double)) =
        use ncFile = openFile "./test-data/map.nc"

        let varId = (Common.VarID 0)
        let result = 
            ncFile.RetrieveAttributeInformation varId attributeName
            |> Result.bind (fun (v: Common.AttributeTypeInformation) -> ncFile.RetrieveAttributeValueDouble varId attributeName v.Size )

        let expectedResult: Result<double[],NCReturnCode> = Result.Ok [| expectedResultValue |]
        result |> should equal expectedResult

    [<Test>]
    [<TestCase("time", 17)>]
    [<TestCase("mesh2d", 1)>]
    [<TestCase("mesh2d_taus", 31)>]
    [<TestCase("mesh2d_ucy", 26)>]
    member this.``RetrieveVariableID should retrieve the correct VarID`` ((variableName: string), (expectedID: int)) =
        use ncFile = openFile "./test-data/map.nc"

        let result = ncFile.RetrieveVariableID variableName
        
        let expectedResult: Result<Common.VarID, NCReturnCode> = Result.Ok (Common.VarID expectedID)
        result |> should equal expectedResult

    [<Test>]
    [<TestCase("time", 1)>]
    [<TestCase("mesh2d", 0)>]
    [<TestCase("mesh2d_taus", 2)>]
    [<TestCase("mesh2d_ucy", 2)>]
    member this.``RetrieveNumberDimensions should retrieve the correct dimension size`` ((name: string), (expectedDimensionSize: int)) =
        use ncFile = openFile "./test-data/map.nc"
        
        let mutable varId = 0

        let result = 
            ncFile.RetrieveVariableID name
            |> Result.bind (fun (id: Common.VarID) -> ncFile.RetrieveNumberDimensions id)

        let expectedResult: Result<int, NCReturnCode> = Result.Ok expectedDimensionSize
        result |> should equal expectedResult
