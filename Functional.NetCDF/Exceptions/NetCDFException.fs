namespace BeardedPlatypus.Functional.NetCDF.Exceptions

/// <summary>
/// <see cref="NetCDFException"/> defines a NetCDF exception with the given <see cref="ReturnCode"/> 
/// and corresponding error string.
/// </summary>
exception public NetCDFException of ErrorCode * string
