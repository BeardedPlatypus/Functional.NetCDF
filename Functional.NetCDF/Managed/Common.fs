namespace BeardedPlatypus.Functional.NetCDF.Managed

open BeardedPlatypus.Functional.NetCDF.Native.NetCDF

/// <summary>
/// The <see cref="Common"/> module defines some common internal types
/// and functions.
/// </summary>
module internal Common =
    /// <summary>
    /// <see cref="NcID"/> defines the id of a NetCDF file.
    /// </summary>
    [<Struct>]
    type NcID = | NcID of int
                member this.ToInt () = match this with | NcID i -> i

    /// <summary>
    /// <see cref="VarID"/> defines the id of a NetCDF file variable.
    /// </summary>
    [<Struct>]
    type VarID = | VarID of int 
                 member this.ToInt () = match this with | VarID i -> i

    /// <summary>
    /// <see cref="AttributeTypeInformation"/> defines the size and type
    /// information of an attribute.
    /// </summary>
    [<Struct>]
    type AttributeTypeInformation = 
        { T: NCType
          Size: int
        }
