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
    | NotRepresentableMathResult      = 13 // NC_ERANGE
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
    let internal convert (returnCode: NCReturnCode) : (ErrorCode * string) = 
        match returnCode with 
        | NCReturnCode.NC2_ERR           -> ( ErrorCode.Undefined, "Undefined error" )
        | NCReturnCode.NC_EBADID         -> ( ErrorCode.BadID, "Not a netcdf id" )
        | NCReturnCode.NC_EBADGRPID      -> ( ErrorCode.BadGroupID, "Bad group id" )    
        | NCReturnCode.NC_EBADNAME       -> ( ErrorCode.BadName, "Attribute or variable name contains illegal characters" )     
        | NCReturnCode.NC_EPERM          -> ( ErrorCode.WriteToReadOnly, "Write to read only" )     
        | NCReturnCode.NC_ENFILE         -> ( ErrorCode.TooManyFilesOpen, "Too many netcdfs open" )     
        | NCReturnCode.NC_EHDFERR        -> ( ErrorCode.HDF5Error, "Error at HDF5 layer" )    
        | NCReturnCode.NC_EDIMMETA       -> ( ErrorCode.DimensionMetaDataError, "Problem with dimension metadata" )    
        | NCReturnCode.NC_EATTMETA       -> ( ErrorCode.AttributeMetaDataError, "Problem with attribute metadata" )    
        | NCReturnCode.NC_ENOMEM         -> ( ErrorCode.MallocFailure, "Memory allocation (malloc) failure" )     
        | NCReturnCode.NC_ENOTVAR        -> ( ErrorCode.VariableNotFound , "Variable not found")
        | NCReturnCode.NC_ENOTATT        -> ( ErrorCode.AttributeNotFound , "Attribute not found")     
        | NCReturnCode.NC_ECHAR          -> ( ErrorCode.InvalidConversionTextNumbers , "Attempt to convert between text & numbers")     
        | NCReturnCode.NC_ERANGE         -> ( ErrorCode.NotRepresentableMathResult , "Math result not representable")
        | NCReturnCode.NC_EINVAL         -> ( ErrorCode.InvalidArgument , "Invalid Argument")     
        | NCReturnCode.NC_EINDEFINE      -> ( ErrorCode.OperationNotAllowedInDefineMode , "Operation not allowed in define mode")
        | _                              -> ( ErrorCode.Undefined, "Undefined error" )
