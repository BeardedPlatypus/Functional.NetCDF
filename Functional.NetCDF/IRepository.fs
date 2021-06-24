namespace BeardedPlatypus.Functional.NetCDF

/// <summary>
/// <see cref="IRepository"/> defines the interface with which to 
/// query the different aspects of a NetCDF file. 
/// </summary>
[<Interface>]
type public IRepository = 
    abstract member RetrieveVariable<'T> : name: string -> 'T

