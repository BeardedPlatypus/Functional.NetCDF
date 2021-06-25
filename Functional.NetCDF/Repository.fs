namespace BeardedPlatypus.Functional.NetCDF

/// <summary>
/// <see cref="Repository"/> implements the <see cref="IRepository"/> with 
/// which to query the different aspects of a NetCDF file. 
/// </summary>
[<Sealed>]
type internal Repository (file: Managed.IFile) = 
    interface IRepository with
        member this.Placeholder (): unit = ()
            
