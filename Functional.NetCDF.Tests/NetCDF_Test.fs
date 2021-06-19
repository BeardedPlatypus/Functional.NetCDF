namespace BeardedPlatypus.Functional.NetCDF.Tests

open NUnit.Framework
open FsUnit

open BeardedPlatypus.Functional.NetCDF.NetCDF

[<TestFixture>]
type NetCDF_Test () =
    let noError : ReturnCode = 0

    [<Test>]
    member this.``Opening and closing a NetCDF files should not return errors.`` () =
        let mutable id : int = -1
        let resultOpen = nc_open("./test-data/map.nc", 0, &id)

        resultOpen |> should equal noError

        let resultClose = nc_close(id)

        resultClose |> should equal noError

    [<Test>]
    member this.``nc_inq_nvars should retrieve the correct number of variables`` () =
        let mutable id : int = -1
        nc_open("./test-data/map.nc", 0, &id) |> ignore

        let mutable nVars : int = -1
        let result = nc_inq_nvars(id, &nVars)

        result |> should equal noError
        nVars |> should equal 34

        nc_close(id) |> ignore

    [<Test>]
    member this.``nc_inq_varname should retrieve the correct name of variables`` () = 
        let mutable id : int = -1
        nc_open("./test-data/map.nc", 0, &id) |> ignore
        
        let resultString : System.Text.StringBuilder = System.Text.StringBuilder("", 256)

        let result = nc_inq_varname(id, 0, resultString)
        result |> should equal noError
        resultString.ToString() |> should equal "projected_coordinate_system"

        nc_close(id) |> ignore

    [<Test>]
    member this.``nc_inq_varnatts should retrieve the correct number of attributes`` () =
        let mutable id : int = -1
        nc_open("./test-data/map.nc", 0, &id) |> ignore
        
        let mutable nAttributes : int = -1

        let result = nc_inq_varnatts(id, 0, &nAttributes)
        result |> should equal noError
        nAttributes |> should equal 11

        nc_close(id) |> ignore

    [<Test>]
    member this.``nc_inq_attname should retrieve the correct attribute name`` () =
        let mutable id : int = -1
        nc_open("./test-data/map.nc", 0, &id) |> ignore
        
        let resultString : System.Text.StringBuilder = System.Text.StringBuilder("", 256)

        let result = nc_inq_attname(id, 0, 0, resultString)
        result |> should equal noError
        resultString.ToString() |> should equal "name"

        nc_close(id) |> ignore


    [<Test>]
    member this.``nc_inq_att should retrieve the correct attribute type and length`` () =
        let mutable id : int = -1
        nc_open("./test-data/map.nc", 0, &id) |> ignore
        
        let mutable t: int = -1
        let mutable l: int = -1

        let result = nc_inq_att(id, 0, "name", &t, &l)
        result |> should equal noError
        
        t |> should equal 2
        l |> should equal 17

        nc_close(id) |> ignore
