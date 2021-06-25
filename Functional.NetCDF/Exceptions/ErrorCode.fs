namespace BeardedPlatypus.Functional.NetCDF.Exceptions

open BeardedPlatypus.Functional.NetCDF.Native.NetCDF

/// <summary>
/// <see cref="ReturnCode"/> defines the possible error codes of NetCDF.
/// </summary>
type public ErrorCode = 
    | Undefined                       =  0
    | BadID                           =  1 // NC_EBADID
    | BadGroupID                      =  2 // NC_EBADGRPID
    | BadName                         =  3 // NC_EBADNAME
    | WriteToReadOnly                 =  4 // NC_EPERM
    | TooManyFilesOpen                =  5 // NC_ENFILE
    | HDF5Error                       =  6 // NC_EHDFERR
    | DimensionMetaDataError          =  7 // NC_EDIMMETA
    | AttributeMetaDataError          =  8 // NC_EATTMETA
    | MallocFailure                   =  9 // NC_ENOMEM
    | VariableNotFound                = 10 // NC_ENOTVAR
    | AttributeNotFound               = 11 // NC_ENOTATT
    | InvalidConversionTextNumbers    = 12 // NC_ECHAR
    | NotRepresantableMathResult      = 13 // NC_ERANGE
    | InvalidArgument                 = 14 // NC_EINVAL
    | OperationNotAllowedInDefineMode = 15 // NC_EINDEFINE

/// <summary>
/// The <see cref="ErrorCode"/> module provides methods to handling error codes.
/// </summary>
module internal ErrorCode =
    /// <summary>
    /// Convert the provided <paramref name="returnCode"/> to a corresponding <see cref="ErrorCode"/> 
    /// and error message.
    /// </summary>
    /// <param name="returnCode">The return code to convert.</param>
    /// <returns>
    /// The converted <see cref="ErrorCode"/> and error message if an error code; otherwise None.
    /// </returns>
    let internal Convert (returnCode: NCReturnCode) : (ErrorCode * string) option = 
        match returnCode with 
        | NCReturnCode.NC_NOERR          -> None
        | NCReturnCode.NC2_ERR           -> Some ( ErrorCode.Undefined, "Undefined error" )
        | NCReturnCode.NC_EBADID         -> Some ( ErrorCode.BadID, "Not a netcdf id" )
        | NCReturnCode.NC_EBADGRPID      -> Some ( ErrorCode.BadGroupID, "Bad group id" )    
        | NCReturnCode.NC_EBADNAME       -> Some ( ErrorCode.BadName, "Attribute or variable name contains illegal characters" )     
        | NCReturnCode.NC_EPERM          -> Some ( ErrorCode.WriteToReadOnly, "Write to read only" )     
        | NCReturnCode.NC_ENFILE         -> Some ( ErrorCode.TooManyFilesOpen, "Too many netcdfs open" )     
        | NCReturnCode.NC_EHDFERR        -> Some ( ErrorCode.HDF5Error, "Error at HDF5 layer" )    
        | NCReturnCode.NC_EDIMMETA       -> Some ( ErrorCode.DimensionMetaDataError, "Problem with dimension metadata" )    
        | NCReturnCode.NC_EATTMETA       -> Some ( ErrorCode.AttributeMetaDataError, "Problem with attribute metadata" )    
        | NCReturnCode.NC_ENOMEM         -> Some ( ErrorCode.MallocFailure, "Memory allocation (malloc) failure" )     
        | NCReturnCode.NC_ENOTVAR        -> Some ( ErrorCode.VariableNotFound , "Variable not found")
        | NCReturnCode.NC_ENOTATT        -> Some ( ErrorCode.AttributeNotFound , "Attribute not found")     
        | NCReturnCode.NC_ECHAR          -> Some ( ErrorCode.InvalidConversionTextNumbers , "Attempt to convert between text & numbers")     
        | NCReturnCode.NC_ERANGE         -> Some ( ErrorCode.NotRepresantableMathResult , "Math result not representable")
        | NCReturnCode.NC_EINVAL         -> Some ( ErrorCode.InvalidArgument , "Invalid Argument")     
        | NCReturnCode.NC_EINDEFINE      -> Some ( ErrorCode.OperationNotAllowedInDefineMode , "Operation not allowed in define mode")
        | _                              -> Some ( ErrorCode.Undefined, "Undefined error" )
