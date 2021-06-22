namespace BeardedPlatypus.Functional.NetCDF.Native.Tests

open NUnit.Framework
open FsUnit

open BeardedPlatypus.Functional.NetCDF.Native.NetCDF

[<TestFixture>]
type NetCDF_Test () =
    let noError : NCReturnCode = NCReturnCode.NC_NOERR

    [<Test>]
    member this.``Opening and closing a NetCDF files should not return errors.`` () =
        let mutable id : int = -1
        let resultOpen = nc_open("./test-data/map.nc", NCOpenMode.NoWrite, &id)

        resultOpen |> should equal noError

        let resultClose = nc_close(id)

        resultClose |> should equal noError

    [<Test>]
    member this.``nc_inq_nvars should retrieve the correct number of variables`` () =
        let mutable id : int = -1
        nc_open("./test-data/map.nc", NCOpenMode.NoWrite, &id) |> ignore

        let mutable nVars : int = -1
        let result = nc_inq_nvars(id, &nVars)

        result |> should equal noError
        nVars |> should equal 34

        nc_close(id) |> ignore

    [<Test>]
    member this.``nc_inq_varname should retrieve the correct name of variables`` () = 
        let mutable id : int = -1
        nc_open("./test-data/map.nc", NCOpenMode.NoWrite, &id) |> ignore
        
        let resultString : System.Text.StringBuilder = System.Text.StringBuilder("", 256)

        let result = nc_inq_varname(id, 0, resultString)
        result |> should equal noError
        resultString.ToString() |> should equal "projected_coordinate_system"

        nc_close(id) |> ignore

    [<Test>]
    member this.``nc_inq_varnatts should retrieve the correct number of attributes`` () =
        let mutable id : int = -1
        nc_open("./test-data/map.nc", NCOpenMode.NoWrite, &id) |> ignore
        
        let mutable nAttributes : int = -1

        let result = nc_inq_varnatts(id, 0, &nAttributes)
        result |> should equal noError
        nAttributes |> should equal 11

        nc_close(id) |> ignore

    [<Test>]
    member this.``nc_inq_attname should retrieve the correct attribute name`` () =
        let mutable id : int = -1
        nc_open("./test-data/map.nc", NCOpenMode.NoWrite, &id) |> ignore
        
        let resultString : System.Text.StringBuilder = System.Text.StringBuilder("", 256)

        let result = nc_inq_attname(id, 0, 0, resultString)
        result |> should equal noError
        resultString.ToString() |> should equal "name"

        nc_close(id) |> ignore


    [<Test>]
    member this.``nc_inq_att should retrieve the correct attribute type and length`` () =
        let mutable id : int = -1
        nc_open("./test-data/map.nc", NCOpenMode.NoWrite, &id) |> ignore
        
        let mutable t: NCType = NCType.Byte
        let mutable l: int = -1

        let result = nc_inq_att(id, 0, "name", &t, &l)
        result |> should equal noError
        
        t |> should equal NCType.Char
        l |> should equal 17

        nc_close(id) |> ignore

    [<Test>]
    [<TestCase("name", "Unknown projected")>]
    [<TestCase("grid_mapping_name", "Unknown projected")>]
    [<TestCase("value", "value is equal to EPSG code")>]
    [<TestCase("EPSG_code", "EPSG:0")>]
    [<TestCase("proj_string", "")>]
    member this.``nc_get_att_text should retrieve the correct string`` ((attributeName: string), (expectedResult: string)) =
        let mutable id : int = -1
        nc_open("./test-data/map.nc", NCOpenMode.NoWrite, &id) |> ignore
        
        let mutable t: NCType = NCType.Byte
        let mutable l: int = -1

        let varId = 0

        nc_inq_att(id, varId, attributeName, &t, &l) |> ignore
        
        let resultString : System.Text.StringBuilder = System.Text.StringBuilder("", l - 1)

        let result = nc_get_att_text(id, varId, attributeName, resultString)

        result |> should equal noError
        resultString.ToString() |> should equal expectedResult

        nc_close(id) |> ignore

    [<Test>]
    member this.``nc_get_att_int should retrieve the correct int array`` () =
        let mutable id : int = -1
        nc_open("./test-data/map.nc", NCOpenMode.NoWrite, &id) |> ignore
        
        let mutable t: NCType = NCType.Byte
        let mutable l: int = -1

        let varId = 0
        let attributeName = "epsg"

        nc_inq_att(id, varId, attributeName, &t, &l) |> ignore
         
        let resultArray = Array.create l -1s
        let result = nc_get_att_int(id, varId, attributeName, resultArray)

        result |> should equal noError
        resultArray |> should equal [| 0s |]

        nc_close(id) |> ignore


    [<Test>]
    [<TestCase("longitude_of_prime_meridian", 0.0)>]
    [<TestCase("semi_major_axis", 6378137.0)>]
    [<TestCase("semi_minor_axis", 6356752.314245)>]
    [<TestCase("inverse_flattening", 298.257223563)>]
    member this.``nc_get_att_double should retrieve the correct double array`` ((attributeName: string), (expectedResult: double)) =
        let mutable id : int = -1
        nc_open("./test-data/map.nc", NCOpenMode.NoWrite, &id) |> ignore
        
        let mutable t: NCType = NCType.Byte
        let mutable l: int = -1

        let varId = 0
        nc_inq_att(id, varId, attributeName, &t, &l) |> ignore
         
        let resultArray = Array.create l (System.Double.NaN)
        let result = nc_get_att_double(id, varId, attributeName, resultArray)

        result |> should equal noError
        resultArray |> should equal [| expectedResult |]

        nc_close(id) |> ignore

    [<Test>]
    [<TestCase("time", 17)>]
    [<TestCase("mesh2d", 1)>]
    [<TestCase("mesh2d_taus", 31)>]
    [<TestCase("mesh2d_ucy", 26)>]
    member this.``nc_inq_varid should retrieve the correct variable id`` ((name: string), (expectedVarID: int)) =
        let mutable id : int = -1
        nc_open("./test-data/map.nc", NCOpenMode.NoWrite, &id) |> ignore
        
        let mutable varId = 0

        let result = nc_inq_varid(id, name, &varId)

        result |> should equal noError
        varId |> should equal expectedVarID

        nc_close(id) |> ignore
