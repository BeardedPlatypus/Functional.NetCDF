namespace BeardedPlatypus.Functional.NetCDF.Managed

open BeardedPlatypus.Functional.NetCDF.Native.NetCDF

/// <summary>
/// The <see cref="Attribute"/> module provides several convenience methods
/// related to the IO of attributes.
/// </summary>
module internal Attribute = 
    /// <summary>
    /// <see cref="IRetriever"/> defines the interface with which to retrieve attributes of type 'T.
    /// </summary>
    [<Interface>]
    type internal IRetriever<'T> = 
        /// <summary>
        /// Retrieve the array of values of type 'T of size <paramref name="valueSize"/> 
        /// from the attribute <paramref name="attributeName"/> of the variable associated with 
        /// <paramref name="varID"/> in the specified <paramref name="file"/>.
        /// </summary>
        /// <param name="file">The file to retrieve the values from</param>
        /// <param name="varID">The id of the variable of which to retrieve the values.</param>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <param name="valueSize">The size of the values array</param>
        /// <returns>
        /// If succesfull, the array containing the requested values; otherwise a return code.
        /// </returns> 
        abstract member Retrieve : file: IFile -> varID: Common.VarID -> attributeName : string -> valueSize: int -> Result<'T[], NCReturnCode>

    /// <summary>
    /// <see cref="Retriever{T}"/> provides a default implementetation of <see cref="IRetriever"/>
    /// which always returns an NC2_ERR.
    /// </summary>
    [<Sealed>]
    type internal Retriever<'T> () =
        interface IRetriever<'T> with 
            member this.Retrieve (file: IFile) (varID: Common.VarID) (attributeName: string) (valueSize: int) : Result<'T[], NCReturnCode> =
                Result.Error NCReturnCode.NC2_ERR

    /// <summary>
    /// <see cref="Retriever"/> implements the <see cref="IRetriever{T}"/> interface
    /// for various values which are available within the NetCDF file format.
    /// </summary>
    [<Sealed>]
    type internal Retriever () =
        interface IRetriever<double> with 
            member this.Retrieve (file: IFile) (varID: Common.VarID) (attributeName: string) (valueSize: int) : Result<double[], NCReturnCode> =
                file.RetrieveAttributeValueDouble varID attributeName valueSize
          
        interface IRetriever<float32> with 
            member this.Retrieve (file: IFile) (varID: Common.VarID) (attributeName: string) (valueSize: int) : Result<float32[], NCReturnCode> =
                file.RetrieveAttributeValueFloat varID attributeName valueSize

        interface IRetriever<int32> with 
            member this.Retrieve (file: IFile) (varID: Common.VarID) (attributeName: string) (valueSize: int) : Result<int32[], NCReturnCode> =
                file.RetrieveAttributeValueInt varID attributeName valueSize

        interface IRetriever<int64> with 
            member this.Retrieve (file: IFile) (varID: Common.VarID) (attributeName: string) (valueSize: int) : Result<int64[], NCReturnCode> =
                file.RetrieveAttributeValueLong varID attributeName valueSize

        interface IRetriever<string> with 
            member this.Retrieve (file: IFile) (varID: Common.VarID) (attributeName: string) (valueSize: int) : Result<string[], NCReturnCode> =
                // There seems to be an inconsistency between .NET5 and Unity, where an additional character is read for .NET5 but not Unity
                // Currently this is fixed with this compiler directive.
                #if NETCDF_TRIM_STRINGS
                let size = valueSize - 1
                #else 
                let size = valueSize
                #endif
                (file.RetrieveAttributeValueText varID attributeName size) |> (Result.map Array.singleton)

        interface IRetriever<uint32> with 
            member this.Retrieve (file: IFile) (varID: Common.VarID) (attributeName: string) (valueSize: int) : Result<uint32[], NCReturnCode> =
                file.RetrieveAttributeValueUInt varID attributeName valueSize

        interface IRetriever<uint64> with 
            member this.Retrieve (file: IFile) (varID: Common.VarID) (attributeName: string) (valueSize: int) : Result<uint64[], NCReturnCode> =
                file.RetrieveAttributeValueULong varID attributeName valueSize

    let private retriever = Retriever()

    /// <summary>
    /// The retriever with which to retrieve variable values.
    /// </summary>
    let internal Value<'T> : IRetriever<'T> = 
        match box retriever with 
        | :? IRetriever<'T> as r -> r
        | _                      -> Retriever<'T>() :> IRetriever<'T>
        
