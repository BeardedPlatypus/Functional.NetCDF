namespace BeardedPlatypus.Functional.NetCDF

/// <summary>
/// <see cref="IAttributeValue"/> defines the result of an attribute value query.
/// </summary>
[<Interface>]
type public IAttributeValue<'T> = 
    /// <summary>
    /// The values as a flattened sequence.
    /// </summary>
    abstract Values : seq<'T> with get
