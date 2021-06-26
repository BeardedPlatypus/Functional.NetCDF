namespace BeardedPlatypus.Functional.NetCDF.Managed

open BeardedPlatypus.Functional.NetCDF.Native.NetCDF
open BeardedPlatypus.Functional.NetCDF.Exceptions
open Common

/// <summary>
/// The <see cref="NetCDF"/> module provides convenient functions
/// related to the NetCDF.
/// </summary>
module internal NetCDF =
    // TODO: extend open modes.
    let OpenExisting (path: string) : IFile =
        let mutable id = -1
        let returnCode = nc_open(path, NCOpenMode.NoWrite, &id)

        match ErrorCode.Convert returnCode with 
        | Some error -> 
            raise (NetCDFException error)
        | None -> 
            new File(NcID id) :> IFile
