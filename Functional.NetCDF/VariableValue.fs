namespace BeardedPlatypus.Functional.NetCDF

/// <summary>
/// <see cref="VariableValue"> implements <see cref="IVariableValue"/> interface.
/// </summary>
[<Sealed>]
type internal VariableValue<'T> (values: 'T[], shape: int[]) =
    interface IVariableValue<'T> with
        override this.Values with get() = values |> Array.toSeq 
        override this.Shape with get() = shape |> Array.toSeq

