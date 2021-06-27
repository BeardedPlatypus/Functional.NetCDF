namespace BeardedPlatypus.Functional.NetCDF

/// <summary>
/// <see cref="AttributeValue"> implements <see cref="IAttributeValue"/> interface.
/// </summary>
[<Sealed>]
type internal AttributeValue<'T> (values: 'T[], shape: int[]) =
    interface IAttributeValue<'T> with
        override this.Values with get() = values |> Array.toSeq 
        override this.Shape with get() = shape |> Array.toSeq

