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

