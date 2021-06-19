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
    /// <see cref="NCType"/> defines the possible data types.
    /// </summary>
    [<RequireQualifiedAccess>]
    type NCType = 
        | Byte = 1
        | Char = 2
        | Short = 3
        | Int = 4
        | Float  = 5
        | Double = 6
        | UByte = 7
        | UShort = 8
        | UInt = 9
        | Int64 = 10
        | UInt64 = 11
        | String = 12

    /// <summary>
    /// <see cref="NCOpenMode"/> defines the read and write modes of the NetCDf
    /// files.
    /// </summary>
    [<System.Flags>]
    [<RequireQualifiedAccess>]
    type NCOpenMode =
        | NoWrite = 0x0000
        | Write   = 0x0001
        | Share   = 0x0800

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
                              [<In>] NCOpenMode omode,
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

    /// <summary>
    /// Retrieve the name of the attribute at <paramref name="attnum"/>.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="attnum">Attribute number</param>
    /// <param name="name">Pointer to the location for the returned attribute name.</param>
    /// <returns>
    /// - NC_NOERR: no error.
    /// - NC_EBADID: bad ncid.
    /// - NC_ENOTVAR bad varid.
    /// - NC_EBADGRPID: bad group ID.
    /// - NC_EBADNAME: bad name.
    /// - NC_ENOTATT: attribute not found.
    /// - NC_ECHAR: illegal conversion to or from NC_CHAR.
    /// - NC_ENOMEM: out of memory.
    /// - NC_ERANGE: range error when converting data.
    /// </returns>
    /// <remarks>
    /// The attributes for each variable are numbered from 0 (the first 
    /// attribute) to natts-1, where natts is the number of attributes
    /// for the variable, as returned from a call to nc_inq_varnatts.
    /// </remarks>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern ReturnCode nc_inq_attname([<In>] int ncid, 
                                     [<In>] int varid,
                                     [<In>] int attnum,
                                     [<Out>] StringBuilder name)
    
    /// <summary>
    /// Retrieve the information about the attribute specified with 
    /// <paramref name="name"/>.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="name">Attribute name.</param>
    /// <param name="xtypep">Pointer to the location for the returned attribute type.</param>
    /// <param name="lenp">Pointer to the location for the returned number of values currently stored in the attribute.</param>
    /// <returns>
    /// - NC_NOERR: no error.
    /// - NC_EBADID: bad ncid.
    /// - NC_ENOTVAR: bad varid.
    /// - NC_EBADGRPID: bad group ID.
    /// - NC_EBADNAME: bad name.
    /// - NC_ENOTATT: attribute not found.
    /// - NC_ECHAR: illegal conversion to or from NC_CHAR.
    /// - NC_ENOMEM: out of memory.
    /// - NC_ERANGE: range error when converting data.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern ReturnCode nc_inq_att([<In>] int ncid,
                                 [<In>] int varid,
                                 [<In>] string name,
                                 [<Out>] NCType& xtypep,
                                 [<Out>] int& lenp)
