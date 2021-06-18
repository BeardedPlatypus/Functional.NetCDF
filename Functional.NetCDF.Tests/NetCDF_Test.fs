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

