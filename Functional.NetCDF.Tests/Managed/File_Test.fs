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
        
        let result = 
            ncFile.RetrieveVariableID name
            |> Result.bind (fun (id: Common.VarID) -> ncFile.RetrieveNumberDimensions id)

        let expectedResult: Result<int, NCReturnCode> = Result.Ok expectedDimensionSize
        result |> should equal expectedResult

    [<Test>]
    [<TestCase("time", [|5|])>]
    [<TestCase("mesh2d_taus", [| 5; 3 |])>]
    [<TestCase("mesh2d_ucy", [| 5; 3 |])>]
    member this.``RetrieveDimensionIDs should retrieve the correct dimension ids`` ((name: string), (expectedDimIDs: int[])) =
        use ncFile = openFile "./test-data/map.nc"

        let result = 
            ncFile.RetrieveVariableID name
            |> Result.bind (fun (id: Common.VarID) -> (ncFile.RetrieveNumberDimensions id)
                                                      |> Result.map (fun nDim -> (id, nDim)))
            |> Result.bind (fun ((id:Common.VarID), (nDim: int)) -> ncFile.RetrieveDimensionIDs id nDim)

        let expectedResult: Result<Common.DimID[], NCReturnCode> = Result.Ok (Array.map Common.DimID expectedDimIDs)
        result |> should equal expectedResult

    [<Test>]
    [<TestCase(3, 25)>]
    [<TestCase(5, 73)>]
    member this.``RetrieveDimensionValue should retrieve the correct dimension length`` ((dimID: int), (expectedDimLength: int)) =
        use ncFile = openFile "./test-data/map.nc"

        let result = ncFile.RetrieveDimensionValue (Common.DimID dimID)

        let expectedResult: Result<int, NCReturnCode> = Result.Ok expectedDimLength 
        result |> should equal expectedResult

    [<Test>]
    [<TestCase("time", 73, [| 0.0; 1200.0; 2400.0; 3600.0; 4800.0; 6000.0; 7200.0; 8400.0; 9600.0; 10800.0 |])>]
    [<TestCase("mesh2d_face_x_bnd", 100, [| 0.0; 100.0; 100.0; 0.0; 100.0; 200.0; 200.0; 100.0; 0.0; 100.0 |])>]
    member this.``RetrieveVariableValueDouble should retrieve the correct data`` ((name: string), 
                                                                                  (expectedDataSize: int), 
                                                                                  (firstValues: double[])) =
        use ncFile = openFile "./test-data/map.nc"

        let getSize (dimIDs: Common.DimID[]): Result<int, NCReturnCode> =
            let folder (accResult: Result<int, NCReturnCode>) (id: Common.DimID): Result<int, NCReturnCode> =
                let sizeResult = ncFile.RetrieveDimensionValue id

                match sizeResult, accResult with 
                | Result.Ok size, Result.Ok acc -> Result.Ok (size * acc)
                | _, Result.Error _ -> accResult
                | Result.Error _, _ -> sizeResult

            Array.fold folder (Result.Ok 1) dimIDs
              

        let result = 
            ncFile.RetrieveVariableID name
            |> Result.bind (fun (id: Common.VarID) -> (ncFile.RetrieveNumberDimensions id)
                                                      |> Result.map (fun nDim -> (id, nDim)))
            |> Result.bind (fun ((id:Common.VarID), (nDim: int)) -> (ncFile.RetrieveDimensionIDs id nDim)
                                                                    |> Result.map (fun dimIDs -> (id, dimIDs)))
            |> Result.bind (fun ((id:Common.VarID), (dimIDs: Common.DimID[])) -> getSize dimIDs
                                                                                 |> Result.map (fun size -> (id, size)))
            |> Result.bind (fun ((id:Common.VarID), (size: int)) -> ncFile.RetrieveVariableValueDouble id size )

        let expectedSize: Result<int, NCReturnCode> = 
            Result.Ok expectedDataSize
        let actualSize: Result<int, NCReturnCode> = 
            result |> Result.map(fun da -> da.Length)
        actualSize |> should equal expectedSize

        let expectedFirstValues: Result<double[], NCReturnCode> = 
            Result.Ok firstValues
        let actualFirstValues: Result<double[], NCReturnCode> = 
            result |> Result.map (fun (values) -> Array.take (firstValues.Length) values)
        actualFirstValues |> should equal expectedFirstValues

    [<Test>]
    [<TestCase("mesh2d_edge_faces", 120, [| 1; 2; 1; 3; 2; 4; 2; 5; 3; 5 |])>]
    [<TestCase("mesh2d_face_nodes", 100, [| 33; 1; 2; 3; 1; 4; 5; 2; 3; 2; 6 |])>]
    member this.``RetrieveVariableValueInt should retrieve the correct data`` ((name: string), (expectedDataSize: int), (firstValues: int[])) =
        use ncFile = openFile "./test-data/map.nc"

        let getSize (dimIDs: Common.DimID[]): Result<int, NCReturnCode> =
            let folder (accResult: Result<int, NCReturnCode>) (id: Common.DimID): Result<int, NCReturnCode> =
                let sizeResult = ncFile.RetrieveDimensionValue id

                match sizeResult, accResult with 
                | Result.Ok size, Result.Ok acc -> Result.Ok (size * acc)
                | _, Result.Error _ -> accResult
                | Result.Error _, _ -> sizeResult

            Array.fold folder (Result.Ok 1) dimIDs
              

        let result = 
            ncFile.RetrieveVariableID name
            |> Result.bind (fun (id: Common.VarID) -> (ncFile.RetrieveNumberDimensions id)
                                                      |> Result.map (fun nDim -> (id, nDim)))
            |> Result.bind (fun ((id:Common.VarID), (nDim: int)) -> (ncFile.RetrieveDimensionIDs id nDim)
                                                                    |> Result.map (fun dimIDs -> (id, dimIDs)))
            |> Result.bind (fun ((id:Common.VarID), (dimIDs: Common.DimID[])) -> getSize dimIDs
                                                                                 |> Result.map (fun size -> (id, size)))
            |> Result.bind (fun ((id:Common.VarID), (size: int)) -> ncFile.RetrieveVariableValueInt id size )

        let expectedSize: Result<int, NCReturnCode> = 
            Result.Ok expectedDataSize
        let actualSize: Result<int, NCReturnCode> = 
            result |> Result.map(fun da -> da.Length)
        actualSize |> should equal expectedSize

        let expectedFirstValues: Result<int[], NCReturnCode> = 
            Result.Ok firstValues
        let actualFirstValues: Result<int32[], NCReturnCode> = 
            result |> Result.map (fun (values) -> Array.take (firstValues.Length) values)
        actualFirstValues |> should equal expectedFirstValues
