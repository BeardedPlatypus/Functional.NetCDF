namespace BeardedPlatypus.Functional.NetCDF

open System.Collections.Generic
open BeardedPlatypus.Functional.NetCDF.Exceptions

/// <summary>
/// <see cref="Repository"/> implements the <see cref="IRepository"/> with 
/// which to query the different aspects of a NetCDF file. 
/// </summary>
[<Sealed>]
type internal Repository (file: Managed.IFile) = 
    let resolveResult (res: Result<'T, Native.NetCDF.NCReturnCode>): 'T =
        match res with 
        | Result.Ok v     -> v
        | Result.Error ec ->
            let errorInfo = ec |> ErrorCode.convert 
            raise (NetCDFException errorInfo)

    let flattenResult (resArray: Result<'T, Native.NetCDF.NCReturnCode>[]) : Result<'T[], Native.NetCDF.NCReturnCode> =
        let folder ((i: int), (acc: Result<'T[], Native.NetCDF.NCReturnCode>)) (v: Result<'T, Native.NetCDF.NCReturnCode>) =
            ( i + 1,
              match (acc, v) with
              | Result.Ok actuallAcc, Result.Ok actualV -> 
                  actuallAcc.[i] <- actualV
                  Result.Ok actuallAcc
              | Result.Error _, _  -> 
                  acc
              | _, Result.Error rc -> Result.Error rc
            )

        let (_, flattened) = Array.fold folder (0, Result.Ok (Array.zeroCreate resArray.Length)) resArray
        flattened

    let idDict = Dictionary<string, VariableID>()

    let retrieveVariableFromFile (variableName: string) : VariableID =
        let id = file.RetrieveVariableID variableName 
                 |> resolveResult
                 |> VariableID
        idDict.Add (variableName, id)
        id

    let retrieveVariableID (variableName: string): VariableID =
        let (hasValue, value) = idDict.TryGetValue variableName

        if (hasValue) then value 
        else retrieveVariableFromFile variableName

    let retrieveVariableShape (varID: VariableID) : Result<int[], Native.NetCDF.NCReturnCode> =
        let dimIDs = (file.RetrieveNumberDimensions varID.ID)
                     |> Result.bind (fun dimSize -> file.RetrieveDimensionIDs varID.ID dimSize)

        let toSize (ids: Managed.Common.DimID[]) : Result<int[], Native.NetCDF.NCReturnCode> =
            Array.map (fun i -> file.RetrieveDimensionValue i) ids
            |> flattenResult

        dimIDs |>Result.bind toSize

    let retrieveVariableValue (id: VariableID) : Result<IVariableValue<'T>, Native.NetCDF.NCReturnCode> =
        let shape = retrieveVariableShape id
        let size = shape |> Result.map (Array.fold (*) 1) |> resolveResult
        let values = Managed.Variable.Value.Retrieve file id.ID size

        match (shape, values) with 
        | Result.Ok s, Result.Ok v -> Result.Ok (VariableValue(v, s) :> IVariableValue<'T>)
        | Result.Error rc, _ -> Result.Error rc
        | _, Result.Error rc -> Result.Error rc

    let retrieveAttribute (attributeName: string) (id: VariableID) : Result<IAttributeValue<'T>, Native.NetCDF.NCReturnCode> =
        let size = file.RetrieveAttributeInformation id.ID attributeName
                   |> Result.map (fun info -> info.Size)
        let values = size |> Result.bind (fun (s: int) -> Managed.Attribute.Value<'T>.Retrieve file id.ID attributeName s )

        values |> Result.map (fun values -> AttributeValue(values) :> IAttributeValue<'T>)
        

    interface IRepository with
        member this.RetrieveVariableValue (id: VariableID) : IVariableValue<'T> =
            retrieveVariableValue id |> resolveResult
 
        member this.RetrieveVariableValue (name: string) : IVariableValue<'T> =
            retrieveVariableID name 
            |> retrieveVariableValue
            |> resolveResult

        member this.RetrieveVariableAttribute<'T> (id: VariableID, attributeName: string) : IAttributeValue<'T> =
            retrieveAttribute attributeName id
            |> resolveResult
        
        member this.RetrieveVariableAttribute<'T> (variableName: string, attributeName: string) : IAttributeValue<'T> =
            retrieveVariableID variableName
            |> retrieveAttribute attributeName
            |> resolveResult

        member this.RetrieveVariablesWithAttribute (attributeName: string) : seq<VariableID> =
            Seq.empty

        member this.RetrieveVariablesWithAttributeWithValue<'T> (attributeName: string, attributeValue: 'T) : seq<VariableID> =
            Seq.empty

