namespace BeardedPlatypus.Functional.NetCDF

/// <summary>
/// <see cref="IQuery"/> defines the interface of queries
/// with which consumers of the library can define their own
/// code to retrieve data from a NetCDF file.
///
/// The <see cref="IQuery"/> receives a <see cref="IRepository"/> upon 
/// execution, which can then be used by the implementing code to 
/// obtain the relevant data from the NetCDF file. 
/// The implementing class is expected to store (or delegate) the 
/// obtained data. 
///
/// The <see cref="IQuery"/> should not store the repository, its
/// state is cleaned up after the execution has been completed.
/// </summary>
[<Interface>]
type public IQuery = 
    /// <summary>
    /// Execute this <see cref="IQuery"/>.
    /// </summary>
    /// <param name="repository">The repository to obtain the data from.</param>
    /// <remarks>
    /// The <see cref="IQuery"/> should *not* store a reference to 
    /// <paramref name="repository"/>.
    /// </remarks>
    abstract member Execute : repository: IRepository -> unit
