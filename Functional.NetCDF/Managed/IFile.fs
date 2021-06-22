﻿namespace BeardedPlatypus.Functional.NetCDF.Managed

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
    /// Retrieve the int attribute values of <paramref name="attributeName"/> 
    /// of the variable associated with <paramref name="variableID"/>.
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <param name="attributeName"> The name of the attribute.</param>
    /// <param name="attributeSize"> The size of the attribute.</param>
    /// <returns>
    /// An array containing the attribute values as ints if succesful;
    /// otherwise the error code.
    /// </returns>
    abstract member RetrieveAttributeValueInt: variableID: VarID -> attributeName: string -> attributeSize: int -> Result<int16[], NCReturnCode>

    /// <summary>
    /// Retrieve the long attribute values of <paramref name="attributeName"/> 
    /// of the variable associated with <paramref name="variableID"/>.
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <param name="attributeName"> The name of the attribute.</param>
    /// <param name="attributeSize"> The size of the attribute.</param>
    /// <returns>
    /// An array containing the attribute values as longs if succesful;
    /// otherwise the error code.
    /// </returns>
    abstract member RetrieveAttributeValueLong: variableID: VarID -> attributeName: string -> attributeSize: int -> Result<int32[], NCReturnCode>

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
    abstract member RetrieveAttributeValueLongLong: variableID: VarID -> attributeName: string -> attributeSize: int -> Result<int64[], NCReturnCode>

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
    abstract member RetrieveAttributeValueUInt: variableID: VarID -> attributeName: string -> attributeSize: int -> Result<uint16[], NCReturnCode>

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
    abstract member RetrieveAttributeValueULong: variableID: VarID -> attributeName: string -> attributeSize: int -> Result<uint32[], NCReturnCode>

    /// <summary>
    /// Retrieve the uint64 attribute values of <paramref name="attributeName"/> 
    /// of the variable associated with <paramref name="variableID"/>.
    /// </summary>
    /// <param name="variableID">The id of the variable.</param>
    /// <param name="attributeName"> The name of the attribute.</param>
    /// <param name="attributeSize"> The size of the attribute.</param>
    /// <returns>
    /// An array containing the attribute values as uint64 if succesful;
    /// otherwise the error code.
    /// </returns>
    abstract member RetrieveAttributeValueULongLong: variableID: VarID -> attributeName: string -> attributeSize: int -> Result<uint64[], NCReturnCode>

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