namespace BeardedPlatypus.Functional.NetCDF.Exceptions

open BeardedPlatypus.Functional.NetCDF.Native.NetCDF

/// <summary>
/// <see cref="Exception"/> provides some convenience functions
/// related to the (NetCDF) exceptions.
/// </summary>
module internal Exception =
    /// <summary>
    /// Raise the default NetCDF exception
    /// </summary>
    let raiseDefault () = 
        ErrorCode.convert (NCReturnCode.NC2_ERR)
        |> NetCDFException
        |> raise

