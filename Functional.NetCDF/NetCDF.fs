namespace BeardedPlatypus.Functional.NetCDF

open System.Text
open System.Runtime.InteropServices

/// <summary>
/// The <see cref="NetCDF"/> module defines the external calls to the native 
/// NetCDF dll.
/// </summary>
module internal NetCDF =
    type ReturnCode = int

    /// <summary>
    /// Open an existing netCDF file.
    /// </summary>
    /// <param name="path">File name for the netCDF dataset to be opened</param>
    /// <param name="omode">The open mode flags</param>
    /// <param name="ncidp">Pointer to the location where the returned netCDF ID is stored</param>
    /// <returns>
    /// - NC_NOERR: No error.
    /// - NC_EPERM: Attempting to create a netCDF file in a directory where
    ///             you do not have permission to open files.
    /// - NC_ENFILE: Too many files open
    /// - NC_ENOMEM: Out of memory.
    /// - NC_EHDFERR: HDF5 error. (NetCDF-4 files only.)
    /// - NC_EDIMMETA: Error in netCDF-4 dimension metadata. (NetCDF-4 files only.)
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern ReturnCode nc_open([<In>] string path, 
                              [<In>] int omode,
                              [<Out>] int& ncidp)

    /// <summary>
    /// Close an open netCDF dataset.
    /// </summary>
    /// <param name="ncid">NetCDF ID, from a previous call to nc_open() or nc_create()</param>
    /// <returns>
    /// - NC_NOERR:     No error.
    /// - NC_EBADID:    Invalid id passed.
    /// - NC_EBADGRPID: ncid did not contain the root group id of this file. (NetCDF-4 only).
    /// </returns>
    /// <remarks>
    /// If the dataset in define mode, nc_enddef() will be called before 
    /// closing. (In this case, if nc_enddef() returns an error, nc_abort() 
    /// will automatically be called to restore the dataset to the consistent 
    /// state before define mode was last entered.) After an open netCDF 
    /// dataset is closed, its netCDF ID may be reassigned to the next netCDF
    /// dataset that is opened or created.
    /// <remarks>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern ReturnCode nc_close([<In>] int ncid)

    /// <summary>
    /// Retrieve the number of variables in a file or group.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="nvarsp">Pointer that gets number of variables. Ignored if null</param>
    /// <returns>
    /// - NC_NOERR:  No Error
    /// - NC_EBADID: Bad ncid
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern ReturnCode nc_inq_nvars([<In>] int ncid, 
                                   [<Out>] int& nvarsp)

    /// <summary>
    /// Retrieve the name of the specified <paramref name="varid"/>.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="name">Returned variable name</param>
    /// <returns>
    /// - NC_NOERR:   No error.
    /// - NC_EBADID:  Bad ncid.
    /// - NC_ENOTVAR: Invalid variable ID.
    /// </returns>
    /// <remarks>
    /// The caller must allocate the space for the returned name. The maximum 
    /// length is NC_MAX_NAME. The name is ignored if NULL.
    /// </remarks>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern ReturnCode nc_inq_varname([<In>] int ncid, 
                                     [<In>] int varid,
                                     [<Out>] StringBuilder name)

    /// <summary>
    /// Retrieve the number of attributes associated with a variable.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="nattsp">Pointer where number of attributes will be stored. Ignored if null</param>
    /// <returns>
    /// - NC_NOERR:   No error.
    /// - NC_EBADID:  Bad ncid.
    /// - NC_ENOTVAR: Invalid variable ID.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern ReturnCode nc_inq_varnatts([<In>] int ncid,
                                      [<In>] int varid,
                                      [<Out>] int& nattsp)
