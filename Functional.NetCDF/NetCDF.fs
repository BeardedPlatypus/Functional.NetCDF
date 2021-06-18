namespace BeardedPlatypus.Functional.NetCDF

open System
open System.Runtime.InteropServices

/// <summary>
/// The <see cref="NetCDF"/> module defines the external calls to the native 
/// NetCDF dll.
/// </summary>
module internal NetCDF =
    type ReturnCode = int

    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern ReturnCode nc_open([<In>] string path, 
                              [<In>] int omode,
                              [<Out>] int& ncidp)

    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern ReturnCode nc_close([<In>] int ncidp)

    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern ReturnCode nc_inq_nvars([<In>] int ncid, 
                                   [<Out>] int& nvarsp)
