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

    let retrieveVariableValue (id: VariableID) : IVariableValue<'T> =
        match typeof<'T> with 
        | _ -> Exception.raiseDefault ()

    let retrieveDimensionNames (id: VariableID) : seq<string> =
        Seq.empty

    let retrieveAttribute (attributeName: string) (id: VariableID) : 'T =
        match typeof<'T> with 
        | _ -> Exception.raiseDefault ()
        

    interface IRepository with
        member this.RetrieveVariableValue<'T> (id: VariableID) : IVariableValue<'T> =
            retrieveVariableValue id

        member this.RetrieveVariableValue<'T> (name: string) : IVariableValue<'T> =
            retrieveVariableID name 
            |> retrieveVariableValue

        member this.RetrieveDimensionNames (id: VariableID) : seq<string> =
            retrieveDimensionNames id

        member this.RetrieveDimensionNames (name: string) : seq<string> =
            retrieveVariableID name 
            |> retrieveDimensionNames

        member this.RetrieveAttribute<'T> (id: VariableID, attributeName: string) : 'T =
            retrieveAttribute attributeName id
        
        member this.RetrieveAttribute<'T> (variableName: string, attributeName: string) : 'T =
            retrieveVariableID variableName
            |> retrieveAttribute attributeName

        member this.RetrieveVariablesWithAttribute (attributeName: string) : seq<VariableID> =
            Seq.empty

        member this.RetrieveVariablesWithAttributeWithValue<'T> (attributeName: string, attributeValue: 'T) : seq<VariableID> =
            Seq.empty

