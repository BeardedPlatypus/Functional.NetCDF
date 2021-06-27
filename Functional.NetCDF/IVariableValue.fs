namespace BeardedPlatypus.Functional.NetCDF

/// <summary>
/// <see cref="IVariableValue"> defines the result of a variable value query.
/// 
/// The value of a variable can have a higher dimension. The <see cref="Shape"/>
/// defines the number of dimensions and their corresponding lengths.
/// The values are stored as a single ordered sequence based on the order of 
/// dimensions defined in the shame.
/// </summary>
[<Interface>]
type public IVariableValue<'T> = 
    /// <summary>
    /// The values as a flattened sequence.
    /// </summary>
    abstract Values : seq<'T> with get

    /// <summary>
    /// The dimensions of the variable values, where each index corresponds
    /// with a dimension of the variable result.
    /// </summary>
    abstract Shape : seq<int> with get
