namespace BeardedPlatypus.Functional.NetCDF

/// <summary>
/// <see cref="NetCDF"/> module defines the query method with which to query
/// a NetCDF file.
/// </summary>
module public NetCDF =
    /// <summary>
    /// Query the NetCDF file at <paramref name="path"/> with the specified
    /// <paramref name="queries"/>.
    /// </summary>
    /// <param name="path">The path to the NetCDF file to query.</param>
    /// <param name="queries">The queries to execute.</param>
    /// <exception cref="NetCDFException">
    /// Thrown when an exception is thrown during the execution of the provided
    /// queries.
    /// </exception>
    let public Query (path: string, [<System.ParamArray>] queries: IQuery[]) : unit =
        ()
 

