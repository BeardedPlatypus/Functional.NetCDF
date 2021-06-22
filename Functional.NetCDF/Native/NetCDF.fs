namespace BeardedPlatypus.Functional.NetCDF.Native

open System.Text
open System.Runtime.InteropServices

/// <summary>
/// The <see cref="NetCDF"/> module defines the external calls to the native 
/// NetCDF dll.
/// </summary>
module internal NetCDF =
    /// <summary>
    /// The maximum number of characters of a name within NetCDF
    /// </summary>
    let NC_MAX_NAME: int = 256

    /// <summary>
    /// <see cref="NCType"/> defines the possible data types.
    /// </summary>
    [<RequireQualifiedAccess>]
    type NCType = 
        | Byte   = 1
        | Char   = 2
        | Short  = 3
        | Int    = 4
        | Float  = 5
        | Double = 6
        | UByte  = 7
        | UShort = 8
        | UInt   = 9
        | Int64  = 10
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
    /// <see cref="NCReturnCode"/> describes the possible return codes by the 
    /// native NetCDF code.
    /// </summary>
    type NCReturnCode = 
        | NC_NOERR          =  0          
        | NC2_ERR           = -1
        | NC_EBADID         = -33
        | NC_ENFILE         = -34      
        | NC_EEXIST         = -35      
        | NC_EINVAL         = -36      
        | NC_EPERM          = -37      
        | NC_ENOTINDEFINE   = -38
        | NC_EINDEFINE      = -39
        | NC_EINVALCOORDS   = -40
        | NC_EMAXDIMS       = -41 
        | NC_ENAMEINUSE     = -42      
        | NC_ENOTATT        = -43      
        | NC_EMAXATTS       = -44      
        | NC_EBADTYPE       = -45      
        | NC_EBADDIM        = -46      
        | NC_EUNLIMPOS      = -47      
        | NC_EMAXVARS       = -48 
        | NC_ENOTVAR        = -49
        | NC_EGLOBAL        = -50      
        | NC_ENOTNC         = -51      
        | NC_ESTS           = -52      
        | NC_EMAXNAME       = -53      
        | NC_EUNLIMIT       = -54      
        | NC_ENORECVARS     = -55      
        | NC_ECHAR          = -56      
        | NC_EEDGE          = -57      
        | NC_ESTRIDE        = -58      
        | NC_EBADNAME       = -59      
        | NC_ERANGE         = -60
        | NC_ENOMEM         = -61      
        | NC_EVARSIZE       = -62      
        | NC_EDIMSIZE       = -63      
        | NC_ETRUNC         = -64      
        | NC_EAXISTYPE      = -65      
        | NC_EDAP           = -66      
        | NC_ECURL          = -67      
        | NC_EIO            = -68      
        | NC_ENODATA        = -69      
        | NC_EDAPSVC        = -70      
        | NC_EDAS           = -71      
        | NC_EDDS           = -72      
        | NC_EDATADDS       = -73      
        | NC_EDAPURL        = -74      
        | NC_EDAPCONSTRAINT = -75    
        | NC_ETRANSLATION   = -76      
        | NC_EACCESS        = -77      
        | NC_EAUTH          = -78      
        | NC_ENOTFOUND      = -90      
        | NC_ECANTREMOVE    = -91      
        | NC_EINTERNAL      = -92      
        | NC_EPNETCDF       = -93      
        | NC4_FIRST_ERROR   = -100    
        | NC_EHDFERR        = -101    
        | NC_ECANTREAD      = -102    
        | NC_ECANTWRITE     = -103    
        | NC_ECANTCREATE    = -104    
        | NC_EFILEMETA      = -105    
        | NC_EDIMMETA       = -106    
        | NC_EATTMETA       = -107    
        | NC_EVARMETA       = -108    
        | NC_ENOCOMPOUND    = -109    
        | NC_EATTEXISTS     = -110    
        | NC_ENOTNC4        = -111    
        | NC_ESTRICTNC3     = -112    
        | NC_ENOTNC3        = -113    
        | NC_ENOPAR         = -114    
        | NC_EPARINIT       = -115    
        | NC_EBADGRPID      = -116    
        | NC_EBADTYPID      = -117    
        | NC_ETYPDEFINED    = -118    
        | NC_EBADFIELD      = -119    
        | NC_EBADCLASS      = -120    
        | NC_EMAPTYPE       = -121    
        | NC_ELATEFILL      = -122    
        | NC_ELATEDEF       = -123    
        | NC_EDIMSCALE      = -124    
        | NC_ENOGRP         = -125    
        | NC_ESTORAGE       = -126    
        | NC_EBADCHUNK      = -127    
        | NC_ENOTBUILT      = -128    
        | NC_EDISKLESS      = -129    
        | NC_ECANTEXTEND    = -130    
        | NC_EMPI           = -131    
        | NC_EFILTER        = -132    
        | NC_ERCFILE        = -133    
        | NC_ENULLPAD       = -134    
        | NC_EINMEMORY      = -135    
        | NC_ENOFILTER      = -136    
        | NC_ENCZARR        = -137    
        | NC_ES3            = -138    
        | NC_EEMPTY         = -139    
        | NC_EFOUND         = -140    
        | NC4_LAST_ERROR    = -140    

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
    extern NCReturnCode nc_open([<In>] string path, 
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
    /// </remarks>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_close([<In>] int ncid)

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
    extern NCReturnCode nc_inq_nvars([<In>] int ncid, 
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
    extern NCReturnCode nc_inq_varname([<In>] int ncid, 
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
    extern NCReturnCode nc_inq_varnatts([<In>] int ncid,
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
    extern NCReturnCode nc_inq_attname([<In>] int ncid, 
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
    extern NCReturnCode nc_inq_att([<In>] int ncid,
                                   [<In>] int varid,
                                   [<In>] string name,
                                   [<Out>] NCType& xtypep,
                                   [<Out>] int& lenp)

    /// <summary>
    /// Get an attribute array of type double.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="name">Attribute name.</param>
    /// <param name="value">Pointer that will get array of attribute values.</param>
    /// <returns>
    /// - NC_NOERR: for success.
    /// - NC_EBADID: Bad ncid.
    ///  -NC_ENOTVAR: Bad varid.
    ///  -NC_EBADNAME: Bad name. See NetCDF Names.
    ///  -NC_EINVAL: Invalid parameters.
    ///  -NC_ENOTATT: Can't find attribute.
    ///  -NC_ECHAR: Can't convert to or from NC_CHAR.
    ///  -NC_ENOMEM: Out of memory.
    ///  -NC_ERANGE: Data conversion went out of range.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_att_double([<In>] int ncid,
                                          [<In>] int varid,
                                          [<In>] string name,
                                          [<Out>] double[] value)

    /// <summary>
    /// Get an attribute array of type float.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="name">Attribute name.</param>
    /// <param name="value">Pointer that will get array of attribute values.</param>
    /// <returns>
    /// - NC_NOERR: for success.
    /// - NC_EBADID: Bad ncid.
    ///  -NC_ENOTVAR: Bad varid.
    ///  -NC_EBADNAME: Bad name. See NetCDF Names.
    ///  -NC_EINVAL: Invalid parameters.
    ///  -NC_ENOTATT: Can't find attribute.
    ///  -NC_ECHAR: Can't convert to or from NC_CHAR.
    ///  -NC_ENOMEM: Out of memory.
    ///  -NC_ERANGE: Data conversion went out of range.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_att_float([<In>] int ncid,
                                         [<In>] int varid,
                                         [<In>] string name,
                                         [<Out>] float32[] value)

    /// <summary>
    /// Get an attribute array of type int.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="name">Attribute name.</param>
    /// <param name="value">Pointer that will get array of attribute values.</param>
    /// <returns>
    /// - NC_NOERR: for success.
    /// - NC_EBADID: Bad ncid.
    ///  -NC_ENOTVAR: Bad varid.
    ///  -NC_EBADNAME: Bad name. See NetCDF Names.
    ///  -NC_EINVAL: Invalid parameters.
    ///  -NC_ENOTATT: Can't find attribute.
    ///  -NC_ECHAR: Can't convert to or from NC_CHAR.
    ///  -NC_ENOMEM: Out of memory.
    ///  -NC_ERANGE: Data conversion went out of range.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_att_int([<In>] int ncid,
                                       [<In>] int varid,
                                       [<In>] string name,
                                       [<Out>] int16[] value)

    /// <summary>
    /// Get an attribute array of type long.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="name">Attribute name.</param>
    /// <param name="value">Pointer that will get array of attribute values.</param>
    /// <returns>
    /// - NC_NOERR: for success.
    /// - NC_EBADID: Bad ncid.
    ///  -NC_ENOTVAR: Bad varid.
    ///  -NC_EBADNAME: Bad name. See NetCDF Names.
    ///  -NC_EINVAL: Invalid parameters.
    ///  -NC_ENOTATT: Can't find attribute.
    ///  -NC_ECHAR: Can't convert to or from NC_CHAR.
    ///  -NC_ENOMEM: Out of memory.
    ///  -NC_ERANGE: Data conversion went out of range.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_att_long([<In>] int ncid,
                                        [<In>] int varid,
                                        [<In>] string name,
                                        [<Out>] int32[] value)

    /// <summary>
    /// Get an attribute array of type int64.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="name">Attribute name.</param>
    /// <param name="value">Pointer that will get array of attribute values.</param>
    /// <returns>
    /// - NC_NOERR: for success.
    /// - NC_EBADID: Bad ncid.
    ///  -NC_ENOTVAR: Bad varid.
    ///  -NC_EBADNAME: Bad name. See NetCDF Names.
    ///  -NC_EINVAL: Invalid parameters.
    ///  -NC_ENOTATT: Can't find attribute.
    ///  -NC_ECHAR: Can't convert to or from NC_CHAR.
    ///  -NC_ENOMEM: Out of memory.
    ///  -NC_ERANGE: Data conversion went out of range.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_att_longlong([<In>] int ncid,
                                            [<In>] int varid,
                                            [<In>] string name,
                                            [<Out>] int64[] value)

    /// <summary>
    /// Get an attribute array of type byte.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="name">Attribute name.</param>
    /// <param name="value">Pointer that will get array of attribute values.</param>
    /// <returns>
    /// - NC_NOERR: for success.
    /// - NC_EBADID: Bad ncid.
    ///  -NC_ENOTVAR: Bad varid.
    ///  -NC_EBADNAME: Bad name. See NetCDF Names.
    ///  -NC_EINVAL: Invalid parameters.
    ///  -NC_ENOTATT: Can't find attribute.
    ///  -NC_ECHAR: Can't convert to or from NC_CHAR.
    ///  -NC_ENOMEM: Out of memory.
    ///  -NC_ERANGE: Data conversion went out of range.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_att_schar([<In>] int ncid,
                                         [<In>] int varid,
                                         [<In>] string name,
                                         [<Out>] byte[] value)

    /// <summary>
    /// Get a text attribute.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="name">Attribute name.</param>
    /// <param name="value">Pointer that will get array of attribute values.</param>
    /// <returns>
    /// - NC_NOERR: for success.
    /// - NC_EBADID: Bad ncid.
    ///  -NC_ENOTVAR: Bad varid.
    ///  -NC_EBADNAME: Bad name. See NetCDF Names.
    ///  -NC_EINVAL: Invalid parameters.
    ///  -NC_ENOTATT: Can't find attribute.
    ///  -NC_ECHAR: Can't convert to or from NC_CHAR.
    ///  -NC_ENOMEM: Out of memory.
    ///  -NC_ERANGE: Data conversion went out of range.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_att_text([<In>] int ncid,
                                        [<In>] int varid,
                                        [<In>] string name,
                                        [<Out>] StringBuilder value)

    /// <summary>
    /// Get an attribute array of type uint.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="name">Attribute name.</param>
    /// <param name="value">Pointer that will get array of attribute values.</param>
    /// <returns>
    /// - NC_NOERR: for success.
    /// - NC_EBADID: Bad ncid.
    ///  -NC_ENOTVAR: Bad varid.
    ///  -NC_EBADNAME: Bad name. See NetCDF Names.
    ///  -NC_EINVAL: Invalid parameters.
    ///  -NC_ENOTATT: Can't find attribute.
    ///  -NC_ECHAR: Can't convert to or from NC_CHAR.
    ///  -NC_ENOMEM: Out of memory.
    ///  -NC_ERANGE: Data conversion went out of range.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_att_uint([<In>] int ncid,
                                        [<In>] int varid,
                                        [<In>] string name,
                                        [<Out>] uint16[] value)

    /// <summary>
    /// Get an attribute array of type ulong.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="name">Attribute name.</param>
    /// <param name="value">Pointer that will get array of attribute values.</param>
    /// <returns>
    /// - NC_NOERR: for success.
    /// - NC_EBADID: Bad ncid.
    ///  -NC_ENOTVAR: Bad varid.
    ///  -NC_EBADNAME: Bad name. See NetCDF Names.
    ///  -NC_EINVAL: Invalid parameters.
    ///  -NC_ENOTATT: Can't find attribute.
    ///  -NC_ECHAR: Can't convert to or from NC_CHAR.
    ///  -NC_ENOMEM: Out of memory.
    ///  -NC_ERANGE: Data conversion went out of range.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_att_ulong([<In>] int ncid,
                                         [<In>] int varid,
                                         [<In>] string name,
                                         [<Out>] uint32[] value)

    /// <summary>
    /// Get an attribute array of type uint64.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="name">Attribute name.</param>
    /// <param name="value">Pointer that will get array of attribute values.</param>
    /// <returns>
    /// - NC_NOERR: for success.
    /// - NC_EBADID: Bad ncid.
    ///  -NC_ENOTVAR: Bad varid.
    ///  -NC_EBADNAME: Bad name. See NetCDF Names.
    ///  -NC_EINVAL: Invalid parameters.
    ///  -NC_ENOTATT: Can't find attribute.
    ///  -NC_ECHAR: Can't convert to or from NC_CHAR.
    ///  -NC_ENOMEM: Out of memory.
    ///  -NC_ERANGE: Data conversion went out of range.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_att_ulonglong([<In>] int ncid,
                                             [<In>] int varid,
                                             [<In>] string name,
                                             [<Out>] uint64[] value)

    /// <summary>
    /// Read the specified <paramref name="varid"/> as an array of doubles
    /// into the pre-allocated <paramref name="ip"/> array.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="ip">Pointer to where the data will be copied.</param>
    /// <returns>
    /// - NC_NOERR: No error.
    /// - NC_ENOTVAR: Variable not found.
    /// - NC_ERANGE: One or more of the values are out of range.
    /// - NC_EINDEFINE: Operation not allowed in define mode.
    /// - NC_EBADID: Bad ncid.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_var_double([<In>] int ncid, 
                                          [<In>] int varid,
                                          [<Out>] double[] ip)

    /// <summary>
    /// Read the specified <paramref name="varid"/> as an array of floats 
    /// into the pre-allocated <paramref name="ip"/> array.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="ip">Pointer to where the data will be copied.</param>
    /// <returns>
    /// - NC_NOERR: No error.
    /// - NC_ENOTVAR: Variable not found.
    /// - NC_ERANGE: One or more of the values are out of range.
    /// - NC_EINDEFINE: Operation not allowed in define mode.
    /// - NC_EBADID: Bad ncid.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_var_float([<In>] int ncid, 
                                         [<In>] int varid,
                                         [<Out>] float32[] ip)

    /// <summary>
    /// Read the specified <paramref name="varid"/> as an array of int16
    /// into the pre-allocated <paramref name="ip"/> array.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="ip">Pointer to where the data will be copied.</param>
    /// <returns>
    /// - NC_NOERR: No error.
    /// - NC_ENOTVAR: Variable not found.
    /// - NC_ERANGE: One or more of the values are out of range.
    /// - NC_EINDEFINE: Operation not allowed in define mode.
    /// - NC_EBADID: Bad ncid.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_var_int([<In>] int ncid, 
                                       [<In>] int varid,
                                       [<Out>] int16[] ip)

    /// <summary>
    /// Read the specified <paramref name="varid"/> as an array of int32
    /// into the pre-allocated <paramref name="ip"/> array.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="ip">Pointer to where the data will be copied.</param>
    /// <returns>
    /// - NC_NOERR: No error.
    /// - NC_ENOTVAR: Variable not found.
    /// - NC_ERANGE: One or more of the values are out of range.
    /// - NC_EINDEFINE: Operation not allowed in define mode.
    /// - NC_EBADID: Bad ncid.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_var_long([<In>] int ncid, 
                                        [<In>] int varid,
                                        [<Out>] int32[] ip)

    /// <summary>
    /// Read the specified <paramref name="varid"/> as an array of int64
    /// into the pre-allocated <paramref name="ip"/> array.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="ip">Pointer to where the data will be copied.</param>
    /// <returns>
    /// - NC_NOERR: No error.
    /// - NC_ENOTVAR: Variable not found.
    /// - NC_ERANGE: One or more of the values are out of range.
    /// - NC_EINDEFINE: Operation not allowed in define mode.
    /// - NC_EBADID: Bad ncid.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_var_longlong([<In>] int ncid, 
                                            [<In>] int varid,
                                            [<Out>] int64[] ip)

    /// <summary>
    /// Read the specified <paramref name="varid"/> as an array of bytes
    /// into the pre-allocated <paramref name="ip"/> array.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="ip">Pointer to where the data will be copied.</param>
    /// <returns>
    /// - NC_NOERR: No error.
    /// - NC_ENOTVAR: Variable not found.
    /// - NC_ERANGE: One or more of the values are out of range.
    /// - NC_EINDEFINE: Operation not allowed in define mode.
    /// - NC_EBADID: Bad ncid.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_var_schar([<In>] int ncid, 
                                         [<In>] int varid,
                                         [<Out>] byte[] ip)

    /// <summary>
    /// Read the specified <paramref name="varid"/> as a string
    /// into the pre-allocated <paramref name="ip"/> string builder.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="ip">Pointer to where the data will be copied.</param>
    /// <returns>
    /// - NC_NOERR: No error.
    /// - NC_ENOTVAR: Variable not found.
    /// - NC_ERANGE: One or more of the values are out of range.
    /// - NC_EINDEFINE: Operation not allowed in define mode.
    /// - NC_EBADID: Bad ncid.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_var_text([<In>] int ncid, 
                                        [<In>] int varid,
                                        [<Out>] StringBuilder ip)

    /// <summary>
    /// Read the specified <paramref name="varid"/> as an array of uint16
    /// into the pre-allocated <paramref name="ip"/> array.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="ip">Pointer to where the data will be copied.</param>
    /// <returns>
    /// - NC_NOERR: No error.
    /// - NC_ENOTVAR: Variable not found.
    /// - NC_ERANGE: One or more of the values are out of range.
    /// - NC_EINDEFINE: Operation not allowed in define mode.
    /// - NC_EBADID: Bad ncid.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_var_uint([<In>] int ncid, 
                                        [<In>] int varid,
                                        [<Out>] uint16[] ip)

    /// <summary>
    /// Read the specified <paramref name="varid"/> as an array of uint32
    /// into the pre-allocated <paramref name="ip"/> array.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="ip">Pointer to where the data will be copied.</param>
    /// <returns>
    /// - NC_NOERR: No error.
    /// - NC_ENOTVAR: Variable not found.
    /// - NC_ERANGE: One or more of the values are out of range.
    /// - NC_EINDEFINE: Operation not allowed in define mode.
    /// - NC_EBADID: Bad ncid.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_var_ulong([<In>] int ncid, 
                                         [<In>] int varid,
                                         [<Out>] uint32[] ip)

    /// <summary>
    /// Read the specified <paramref name="varid"/> as an array of uint64
    /// into the pre-allocated <paramref name="ip"/> array.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="varid">Variable ID</param>
    /// <param name="ip">Pointer to where the data will be copied.</param>
    /// <returns>
    /// - NC_NOERR: No error.
    /// - NC_ENOTVAR: Variable not found.
    /// - NC_ERANGE: One or more of the values are out of range.
    /// - NC_EINDEFINE: Operation not allowed in define mode.
    /// - NC_EBADID: Bad ncid.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_get_var_ulonglong([<In>] int ncid, 
                                             [<In>] int varid,
                                             [<Out>] int64[] ip)

    /// <summary>
    /// Find the ID of a variable with the name <paramref name="name"/>.
    /// </summary>
    /// <param name="ncid">File and group ID</param>
    /// <param name="name">The name of the variable to retrieve</param>
    /// <param name="varidp">Pointer to the location for the returned variable ID</param>
    /// <returns>
    /// - NC_NOERR No error.
    /// - NC_EBADID Bad ncid.
    /// - NC_ENOTVAR Invalid variable ID.
    /// </returns>
    [<DllImport("netcdf.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern NCReturnCode nc_inq_varid([<In>] int ncid, 
                                     [<In>] string name,
                                     [<Out>] int& varidp)
