namespace BeardedPlatypus.Functional.NetCDF.Managed

open BeardedPlatypus.Functional.NetCDF.Native.NetCDF
open BeardedPlatypus.Functional.NetCDF.Managed.Common

/// <summary>
/// <see cref="IFile"/> defines the interface with which to retrieve data
/// from a .nc file. This is a thin wrapper around the Native.NetCDF logic.
/// </summary>
[<Interface>]
type internal IFile =
    inherit System.IDisposable
    /// <summary>
    /// Retrieves the number of variables in this <see cref="IFile"/>
    /// </summary>
    /// <returns>
    /// The number of variables in this <see cref="IFile"/> if succesful; 
    /// otherwise the error code.
    /// </returns>
    abstract member RetrieveNVariables : unit -> Result<int, NCReturnCode>

    /// <summary>
    /// Retrieve the name of the variable defined by the <paramref name="variableID"/>.
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <returns>
    /// The name of the variable defined by <paramref name="variableID"/> if succesful; 
    /// otherwise the error code.
    /// </returns>
    abstract member RetrieveVariableName : variableID: VarID ->  Result<string, NCReturnCode>

    /// <summary>
    /// Retrieves the number of attributes of <paramref name="variableID"/>.
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <returns>
    /// The number of variables in this <see cref="IFile"/>if succesful; 
    /// otherwise the error code.
    /// </returns>
    abstract member RetrieveNAttributes : variableID: VarID -> Result<int, NCReturnCode>

    /// <summary>
    /// Retrieve the name of the attribute at position 
    /// <paramref name="attributeNumber"/> of the variable defined by the 
    /// <paramref name="variableID"/>.
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <param name="attributeNumber"> The number of the attribute.</param>
    /// <returns>
    /// The name of the attribute if succesful; otherwise the error code.
    /// </returns>
    abstract member RetrieveAttributeName : variableID: VarID -> attributeNumber: int -> Result<string, NCReturnCode>

    /// <summary>
    /// Retrieve the attribute information of the attribute 
    /// <paramref name="attributeName"/> of the variable associated with
    /// <paramref name="variableID"/>.
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <param name="attributeName"> The name of the attribute.</param>
    /// <returns>
    /// The attribute information of the attribute if succesful; otherwise the error code.
    /// </returns>
    abstract member RetrieveAttributeInformation : variableID: VarID -> attributeName: string -> Result<AttributeTypeInformation, NCReturnCode>

    /// <summary>
    /// Retrieve the double attribute values of <paramref name="attributeName"/> 
    /// of the variable associated with <paramref name="variableID"/>.
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <param name="attributeName"> The name of the attribute.</param>
    /// <param name="attributeSize"> The size of the attribute.</param>
    /// <returns>
    /// An array containing the attribute values as doubles if succesful;
    /// otherwise the error code.
    /// </returns>
    abstract member RetrieveAttributeValueDouble : variableID: VarID -> attributeName: string -> attributeSize: int -> Result<double[], NCReturnCode>

    /// <summary>
    /// Retrieve the float attribute values of <paramref name="attributeName"/> 
    /// of the variable associated with <paramref name="variableID"/>.
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <param name="attributeName"> The name of the attribute.</param>
    /// <param name="attributeSize"> The size of the attribute.</param>
    /// <returns>
    /// An array containing the attribute values as floats if succesful;
    /// otherwise the error code.
    /// </returns>
    abstract member RetrieveAttributeValueFloat: variableID: VarID -> attributeName: string -> attributeSize: int -> Result<float32[], NCReturnCode>

    /// <summary>
    /// Retrieve the int32 attribute values of <paramref name="attributeName"/> 
    /// of the variable associated with <paramref name="variableID"/>.
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <param name="attributeName"> The name of the attribute.</param>
    /// <param name="attributeSize"> The size of the attribute.</param>
    /// <returns>
    /// An array containing the attribute values as longs if succesful;
    /// otherwise the error code.
    /// </returns>
    abstract member RetrieveAttributeValueInt: variableID: VarID -> attributeName: string -> attributeSize: int -> Result<int32[], NCReturnCode>

    /// <summary>
    /// Retrieve the int64 attribute values of <paramref name="attributeName"/> 
    /// of the variable associated with <paramref name="variableID"/>.
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <param name="attributeName"> The name of the attribute.</param>
    /// <param name="attributeSize"> The size of the attribute.</param>
    /// <returns>
    /// An array containing the attribute values as int64 if succesful;
    /// otherwise the error code.
    /// </returns>
    abstract member RetrieveAttributeValueLong: variableID: VarID -> attributeName: string -> attributeSize: int -> Result<int64[], NCReturnCode>

    /// <summary>
    /// Retrieve the uint attribute values of <paramref name="attributeName"/> 
    /// of the variable associated with <paramref name="variableID"/>.
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <param name="attributeName"> The name of the attribute.</param>
    /// <param name="attributeSize"> The size of the attribute.</param>
    /// <returns>
    /// An array containing the attribute values as uints if succesful;
    /// otherwise the error code.
    /// </returns>
    abstract member RetrieveAttributeValueUInt: variableID: VarID -> attributeName: string -> attributeSize: int -> Result<uint32[], NCReturnCode>

    /// <summary>
    /// Retrieve the ulong attribute values of <paramref name="attributeName"/> 
    /// of the variable associated with <paramref name="variableID"/>.
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <param name="attributeName"> The name of the attribute.</param>
    /// <param name="attributeSize"> The size of the attribute.</param>
    /// <returns>
    /// An array containing the attribute values as ulongs if succesful;
    /// otherwise the error code.
    /// </returns>
    abstract member RetrieveAttributeValueULong: variableID: VarID -> attributeName: string -> attributeSize: int -> Result<uint64[], NCReturnCode>

    /// <summary>
    /// Retrieve the string attribute values of <paramref name="attributeName"/> 
    /// of the variable associated with <paramref name="variableID"/>.
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <param name="attributeName"> The name of the attribute.</param>
    /// <param name="attributeSize"> The size of the string.</param>
    /// <returns>
    /// The attribute value as string if succesful; otherwise the error code.
    /// </returns>
    abstract member RetrieveAttributeValueText: variableID: VarID -> attributeName: string -> attributeSize: int -> Result<string, NCReturnCode>

    /// <summary>
    /// Retrieve the <see cref="VarID"/> associated with provided <paramref name="variableName"/>
    /// </summary>
    /// <param name="variableName">The name of the variable</param>
    /// <returns>
    /// The <see cref="VarID"/> if succesful; otherwise the error code.
    /// </returns>
    abstract member RetrieveVariableID: variableName: string -> Result<VarID, NCReturnCode>

    /// <summary>
    /// Retrieve the number of dimensions of the value associated with <paramref name="variableID"/>.
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <returns>
    /// The number of dimensions if succesful; otherwise the error code.
    /// </returns>
    abstract member RetrieveNumberDimensions: variableID: VarID -> Result<int, NCReturnCode>

    /// <summary>
    /// Retrieve the <see cref="DimID"/> values associated with <paramref name="variableID"/>
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <param name="nDimensions">The number of dimension ids.</param>
    /// <returns>
    /// An array containing the <see cref="DimID"/> associated with 
    /// <paramref name="variableID"/> if succesful; otherwise the error code.
    /// </returns>
    abstract member RetrieveDimensionIDs: variableID: VarID -> nDimensions: int -> Result<DimID[], NCReturnCode>

    /// <summary>
    /// Retrieve the value associated with the <paramref name="dimensionID"/> .
    /// </summary>
    /// <param name="dimensionID">The id of the dimension.</param>
    /// <returns>
    /// The value associated with the <paramref name="dimensionID"/> if succesful; 
    /// otherwise the error code.
    /// </returns>
    abstract member RetrieveDimensionValue: dimensionID: DimID -> Result<int, NCReturnCode>

    /// <summary>
    /// Retrieve the the double values associated with <paramref name="variableID"/>
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <param name="valueSize"> The number of values.</param>
    /// <returns>
    /// An array containing the values as doubles associated with 
    /// <paramref name="variableID"/> if succesful; otherwise the error code.
    /// </returns>
    abstract member RetrieveVariableValueDouble : variableID: VarID -> valueSize: int -> Result<double[], NCReturnCode>

    /// <summary>
    /// Retrieve the the float32 values associated with <paramref name="variableID"/>
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <param name="valueSize"> The number of values.</param>
    /// <returns>
    /// An array containing the values as float32s associated with 
    /// <paramref name="variableID"/> if succesful; otherwise the error code.
    /// </returns>
    abstract member RetrieveVariableValueFloat : variableID: VarID -> valueSize: int -> Result<float32[], NCReturnCode>

    /// <summary>
    /// Retrieve the the int32 values associated with <paramref name="variableID"/>
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <param name="valueSize"> The number of values.</param>
    /// <returns>
    /// An array containing the values as int32s associated with 
    /// <paramref name="variableID"/> if succesful; otherwise the error code.
    /// </returns>
    abstract member RetrieveVariableValueInt : variableID: VarID -> valueSize: int -> Result<int32[], NCReturnCode>

    /// <summary>
    /// Retrieve the the int64 values associated with <paramref name="variableID"/>
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <param name="valueSize"> The number of values.</param>
    /// <returns>
    /// An array containing the values as int64s associated with 
    /// <paramref name="variableID"/> if succesful; otherwise the error code.
    /// </returns>
    abstract member RetrieveVariableValueLong : variableID: VarID -> valueSize: int -> Result<int64[], NCReturnCode>

    /// <summary>
    /// Retrieve the the byte values associated with <paramref name="variableID"/>
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <param name="valueSize"> The number of values.</param>
    /// <returns>
    /// An array containing the values as byte associated with 
    /// <paramref name="variableID"/> if succesful; otherwise the error code.
    /// </returns>
    abstract member RetrieveVariableValueChar : variableID: VarID -> valueSize: int -> Result<byte[], NCReturnCode>
