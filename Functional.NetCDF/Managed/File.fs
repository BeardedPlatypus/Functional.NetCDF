﻿namespace BeardedPlatypus.Functional.NetCDF.Managed

open BeardedPlatypus.Functional.NetCDF.Native.NetCDF
open BeardedPlatypus.Functional.NetCDF.Managed
open BeardedPlatypus.Functional.NetCDF.Managed.Common

/// <summary>
/// <see cref="File"/> implements the <see cref="IFile"/> interface
/// by providing a thin disposable wrapper around the Native.NetCDF code.
/// </summary>
[<Sealed>]
type internal File (ncId: NcID) =
    let localId = ncId.ToInt ()

    let toResult (fMap: 'TResult -> 'TReturn) (returnCode: NCReturnCode) (v: 'TResult) : Result<'TReturn,NCReturnCode> =
        match returnCode with 
        | NCReturnCode.NC_NOERR -> Ok (fMap v)
        | _                     -> Error returnCode
        
    let sb (s: System.Text.StringBuilder) = s.ToString()

    let dispose () =
        nc_close(localId) |> ignore

    interface IFile with
        member this.RetrieveNVariables () : Result<int, NCReturnCode> =
            let mutable nVariables = - 1
            let returnCode = nc_inq_nvars(localId, &nVariables)
            toResult id returnCode nVariables

        member this.RetrieveVariableName (variableID: VarID): Result<string,NCReturnCode> =
            let resultString = System.Text.StringBuilder("", NC_MAX_NAME)
            let result = nc_inq_varname(localId, variableID.ToInt(), resultString)
            toResult sb result resultString

        member this.RetrieveNAttributes(variableID: VarID): Result<int,NCReturnCode> = 
            let mutable nAttributes = -1
            let returnCode = nc_inq_varnatts(localId, variableID.ToInt(), &nAttributes)
            toResult id returnCode nAttributes

        member this.RetrieveAttributeName(variableID: VarID) (attributeNumber: int): Result<string,NCReturnCode> = 
            let resultString = System.Text.StringBuilder("", NC_MAX_NAME)
            let returnCode = nc_inq_attname(localId, variableID.ToInt(), attributeNumber, resultString)
            toResult sb returnCode resultString

        member this.RetrieveAttributeInformation(variableID: VarID) (attributeName: string): Result<AttributeTypeInformation,NCReturnCode> =
            let mutable ncType : NCType = NCType.Byte
            let mutable len: int = -1

            let returnCode = nc_inq_att(localId, variableID.ToInt(), attributeName, &ncType, &len)
            toResult (fun (nt: NCType, l: int) -> { AttributeTypeInformation.T = nt; AttributeTypeInformation.Size = l }) returnCode (ncType, len)

        member this.RetrieveAttributeValueDouble(variableID: VarID) (attributeName: string) (attributeSize: int): Result<double [],NCReturnCode> = 
            let resultArray = Array.zeroCreate attributeSize
            let returnCode = nc_get_att_double(localId, variableID.ToInt(), attributeName, resultArray)
            toResult id returnCode resultArray

        member this.RetrieveAttributeValueFloat(variableID: VarID) (attributeName: string) (attributeSize: int): Result<float32 [],NCReturnCode> = 
            let resultArray = Array.zeroCreate attributeSize
            let returnCode = nc_get_att_float(localId, variableID.ToInt(), attributeName, resultArray)
            toResult id returnCode resultArray

        member this.RetrieveAttributeValueInt(variableID: VarID) (attributeName: string) (attributeSize: int): Result<int16 [],NCReturnCode> = 
            let resultArray = Array.zeroCreate attributeSize
            let returnCode = nc_get_att_int(localId, variableID.ToInt(), attributeName, resultArray)
            toResult id returnCode resultArray
        
        member this.RetrieveAttributeValueLong(variableID: VarID) (attributeName: string) (attributeSize: int): Result<int32 [],NCReturnCode> = 
            let resultArray = Array.zeroCreate attributeSize
            let returnCode = nc_get_att_long(localId, variableID.ToInt(), attributeName, resultArray)
            toResult id returnCode resultArray

        member this.RetrieveAttributeValueLongLong(variableID: VarID) (attributeName: string) (attributeSize: int): Result<int64 [],NCReturnCode> = 
            let resultArray = Array.zeroCreate attributeSize
            let returnCode = nc_get_att_longlong(localId, variableID.ToInt(), attributeName, resultArray)
            toResult id returnCode resultArray

        member this.RetrieveAttributeValueText(variableID: VarID) (attributeName: string) (attributeSize: int): Result<string,NCReturnCode> = 
            let resultString = System.Text.StringBuilder("", attributeSize)
            let returnCode = nc_get_att_text(localId, variableID.ToInt(), attributeName, resultString)
            toResult sb returnCode resultString

        member this.RetrieveAttributeValueUInt(variableID: VarID) (attributeName: string) (attributeSize: int): Result<uint16 [],NCReturnCode> = 
            let resultArray = Array.zeroCreate attributeSize
            let returnCode = nc_get_att_uint(localId, variableID.ToInt(), attributeName, resultArray)
            toResult id returnCode resultArray

        member this.RetrieveAttributeValueULong(variableID: VarID) (attributeName: string) (attributeSize: int): Result<uint32 [],NCReturnCode> = 
            let resultArray = Array.zeroCreate attributeSize
            let returnCode = nc_get_att_ulong(localId, variableID.ToInt(), attributeName, resultArray)
            toResult id returnCode resultArray

        member this.RetrieveAttributeValueULongLong(variableID: VarID) (attributeName: string) (attributeSize: int): Result<uint64 [],NCReturnCode> = 
            let resultArray = Array.zeroCreate attributeSize
            let returnCode = nc_get_att_ulonglong(localId, variableID.ToInt(), attributeName, resultArray)
            toResult id returnCode resultArray

    interface System.IDisposable with
        member this.Dispose () = 
            dispose ()
            System.GC.SuppressFinalize(this)

    override this.Finalize() =
        dispose ()
