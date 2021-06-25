namespace BeardedPlatypus.Functional.NetCDF

/// <summary>
/// <see cref="NetCDF"/> module defines the query method with which to query
/// a NetCDF file.
/// </summary>
module public NetCDF =
    /// <summary>
    /// Query the NetCDF file at <paramref name="path"/> with the specified
    /// <paramref name="query"/>.
    /// </summary>
    /// <param name="path">The path to the NetCDF file to query.</param>
    /// <param name="query">The query to execute.</param>
    /// <exception cref="NetCDFException">
    /// Thrown when an exception occurs during the execution of the provided query.
    /// </exception>
    let public Query (path: string, query: IQuery) : unit =
        use file: Managed.IFile = Managed.NetCDF.OpenExisting path
        let repo: IRepository = Repository(file) :> IRepository

        query.Execute repo
