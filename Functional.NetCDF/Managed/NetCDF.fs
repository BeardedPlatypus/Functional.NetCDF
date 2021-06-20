namespace BeardedPlatypus.Functional.NetCDF.Managed

open BeardedPlatypus.Functional.NetCDF.Native.NetCDF
open Common

module internal NetCDF =
    // TODO: extend open modes.
    let OpenExisting (path: string) : Result<File, NCReturnCode> =
        let mutable id = -1
        let returnCode = nc_open(path, NCOpenMode.NoWrite, &id)

        match returnCode with 
        | NCReturnCode.NC_NOERR -> new File(NcID id) |> Result.Ok
        | _                     -> returnCode |> Result.Error

