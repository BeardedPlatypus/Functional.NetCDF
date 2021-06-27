namespace BeardedPlatypus.Functional.NetCDF.Managed

open BeardedPlatypus.Functional.NetCDF.Native.NetCDF

/// <summary>
/// The <see cref="Variable"/> module provides several convenience methods
/// related to the IO of variables.
/// </summary>
module internal Variable = 
    /// <summary>
    /// <see cref="IRetriever"/> defines the interface with which to retrieve variables of type 'T.
    /// </summary>
    [<Interface>]
    type internal IRetriever<'T> = 
        /// <summary>
        /// Retrieve the array of values of type 'T of size <paramref name="valueSize"/> 
        /// from the variable associated with <paramref name="varID"/> in the specified 
        /// <paramref name="file"/>.
        /// </summary>
        /// <param name="file">The file to retrieve the values from</param>
        /// <param name="varID">The id of the variable of which to retrieve the values.</param>
        /// <param name="valueSize">The size of the values array</param>
        /// <returns>
        /// If succesfull, the array containing the requested values; otherwise a return code.
        /// </returns> 
        abstract member Retrieve : file: IFile -> varID: Common.VarID -> valueSize: int -> Result<'T[], NCReturnCode>

    /// <summary>
    /// <see cref="Retriever{T}"/> provides a default implementetation of <see cref="IRetriever"/>
    /// which always returns an NC2_ERR.
    /// </summary>
    [<Sealed>]
    type internal Retriever<'T> () =
        interface IRetriever<'T> with 
            member this.Retrieve (file: IFile) (varID: Common.VarID) (valueSize: int) : Result<'T[], NCReturnCode> =
                Result.Error NCReturnCode.NC2_ERR

    /// <summary>
    /// <see cref="Retriever"/> implements the <see cref="IRetriever{T}"/> interface
    /// for various values which are available within the NetCDF file format.
    /// </summary>
    [<Sealed>]
    type internal Retriever () =
        interface IRetriever<double> with 
            member this.Retrieve (file: IFile) (varID: Common.VarID) (valueSize: int) : Result<double[], NCReturnCode> =
                file.RetrieveVariableValueDouble varID valueSize
          
        interface IRetriever<float32> with 
            member this.Retrieve (file: IFile) (varID: Common.VarID) (valueSize: int) : Result<float32[], NCReturnCode> =
                file.RetrieveVariableValueFloat varID valueSize

        interface IRetriever<int32> with 
            member this.Retrieve (file: IFile) (varID: Common.VarID) (valueSize: int) : Result<int32[], NCReturnCode> =
                file.RetrieveVariableValueInt varID valueSize

        interface IRetriever<int64> with 
            member this.Retrieve (file: IFile) (varID: Common.VarID) (valueSize: int) : Result<int64[], NCReturnCode> =
                file.RetrieveVariableValueLong varID valueSize

        interface IRetriever<byte> with 
            member this.Retrieve (file: IFile) (varID: Common.VarID) (valueSize: int) : Result<byte[], NCReturnCode> =
                file.RetrieveVariableValueChar varID valueSize

        interface IRetriever<string> with 
            member this.Retrieve (file: IFile) (varID: Common.VarID) (valueSize: int) : Result<string[], NCReturnCode> =
                (file.RetrieveVariableValueString varID valueSize) |> (Result.map Array.singleton)

        interface IRetriever<uint32> with 
            member this.Retrieve (file: IFile) (varID: Common.VarID) (valueSize: int) : Result<uint32[], NCReturnCode> =
                file.RetrieveVariableValueUInt varID valueSize

        interface IRetriever<uint64> with 
            member this.Retrieve (file: IFile) (varID: Common.VarID) (valueSize: int) : Result<uint64[], NCReturnCode> =
                file.RetrieveVariableValueULong varID valueSize

    let private retriever = Retriever()

    /// <summary>
    /// The retriever with which to retrieve variable values.
    /// </summary>
    let internal Value<'T> : IRetriever<'T> = 
        match box retriever with 
        | :? IRetriever<'T> as r -> r
        | _                      -> Retriever<'T>() :> IRetriever<'T>
        
