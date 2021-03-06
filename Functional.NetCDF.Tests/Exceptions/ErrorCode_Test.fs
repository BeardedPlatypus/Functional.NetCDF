namespace BeardedPlatypus.Functional.NetCDF.Tests.Exceptions

open NUnit.Framework
open FsUnit

open BeardedPlatypus.Functional.NetCDF.Native.NetCDF
open BeardedPlatypus.Functional.NetCDF.Exceptions

[<TestFixture>]
type ErrorCode_Test () = 
    [<Test>]
    [<TestCase(-1,  ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-33, ErrorCode.BadID, "Not a netcdf id")>]
    [<TestCase(-34, ErrorCode.TooManyFilesOpen, "Too many netcdfs open")>]
    [<TestCase(-35, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-36, ErrorCode.InvalidArgument, "Invalid Argument")>]
    [<TestCase(-37, ErrorCode.WriteToReadOnly, "Write to read only")>]
    [<TestCase(-38, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-39, ErrorCode.OperationNotAllowedInDefineMode, "Operation not allowed in define mode")>]
    [<TestCase(-40, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-41, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-42, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-43, ErrorCode.AttributeNotFound, "Attribute not found")>]
    [<TestCase(-44, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-45, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-46, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-47, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-48, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-49, ErrorCode.VariableNotFound, "Variable not found")>]
    [<TestCase(-50, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-51, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-52, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-53, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-54, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-55, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-56, ErrorCode.InvalidConversionTextNumbers, "Attempt to convert between text & numbers")>]
    [<TestCase(-57, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-58, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-59, ErrorCode.BadName, "Attribute or variable name contains illegal characters")>]
    [<TestCase(-60, ErrorCode.NotRepresentableMathResult, "Math result not representable")>]
    [<TestCase(-61, ErrorCode.MallocFailure, "Memory allocation (malloc) failure")>]
    [<TestCase(-62, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-63, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-64, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-65, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-66, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-67, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-68, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-69, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-70, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-71, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-72, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-73, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-74, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-75, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-76, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-77, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-78, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-90, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-91, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-92, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-93, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-100, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-101, ErrorCode.HDF5Error, "Error at HDF5 layer")>]
    [<TestCase(-102, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-103, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-104, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-105, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-106, ErrorCode.DimensionMetaDataError, "Problem with dimension metadata")>]
    [<TestCase(-107, ErrorCode.AttributeMetaDataError, "Problem with attribute metadata")>]
    [<TestCase(-108, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-109, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-110, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-111, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-112, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-113, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-114, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-115, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-116, ErrorCode.BadGroupID, "Bad group id")>]
    [<TestCase(-117, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-118, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-119, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-120, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-121, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-122, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-123, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-124, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-125, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-126, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-127, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-128, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-129, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-130, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-131, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-132, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-133, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-134, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-135, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-136, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-137, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-138, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-139, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-140, ErrorCode.Undefined, "Undefined error")>]
    [<TestCase(-140, ErrorCode.Undefined, "Undefined error")>]
    member this.``convert converts a NCReturnCode into the correct ErrorCode and message`` ((ncCodeInt: int),
                                                                                            (expectedErrorCode: ErrorCode), 
                                                                                            (expectedErrorMsg: string)) =
        // Due to NCReturnCode being internal, we can't pass it as NCReturnCode directly.
        let ncCode: NCReturnCode = enum<NCReturnCode>(ncCodeInt)
        let (errorCode, errorMsg) = ErrorCode.convert ncCode

        errorCode |> should equal expectedErrorCode
        errorMsg |> should equal expectedErrorMsg


