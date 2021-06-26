namespace BeardedPlatypus.Functional.NetCDF

open BeardedPlatypus.Functional.NetCDF.Managed.Common

/// <summary>
/// <see cref="VariableID"/> provides a tag to identify variables from within a
/// <see cref="IRepository"/> without retrieving all data. This is necessary to 
/// query further attributes of said variable.
/// </summary>
type public VariableID =
    struct 
        val internal ID: VarID
        internal new(id: VarID) = { ID =id }
    end 
