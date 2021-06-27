namespace BeardedPlatypus.Functional.NetCDF.Tests.Exceptions

open NUnit.Framework
open FsUnit

open BeardedPlatypus.Functional.NetCDF.Exceptions

[<TestFixture>]
type Exception_Test () = 
    [<Test>]
    member this.``raiseDefault throws correct exception`` () =
        let exc = Assert.Throws<NetCDFException>(TestDelegate(Exception.raiseDefault))
        
        exc.Data0 |> should equal ErrorCode.Undefined
        exc.Data1 |> should equal "Undefined error"

