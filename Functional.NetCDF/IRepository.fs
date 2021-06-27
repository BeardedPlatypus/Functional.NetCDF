namespace BeardedPlatypus.Functional.NetCDF

/// <summary>
/// <see cref="IRepository"/> defines the interface with which to 
/// query the different aspects of a NetCDF file. 
/// </summary>
[<Interface>]
type public IRepository = 
    /// <summary>
    /// Retrieve the variable values associated with the variable id, <paramref cref="id"/>
    /// </summary>
    /// <param name="id">The id of the variable.</param>
    /// <returns>
    /// The <see cref="IVariableResult"/> corresponding with <paramref name="id"/>.
    /// </returns>
    /// <exception cref="Exceptions.NetCDFException"> 
    /// Thrown when any exception occurs during retrieving the variable values.
    /// </exception>
    abstract member RetrieveVariableValue<'T> : id: VariableID -> IVariableValue<'T>

    /// <summary>
    /// Retrieve the variable values associated with the variable name, <paramref cref="name"/>
    /// </summary>
    /// <param name="name">The name of the variable.</param>
    /// <returns>
    /// The <see cref="IVariableResult"/> corresponding with <paramref name="name"/>.
    /// </returns>
    /// <exception cref="Exceptions.NetCDFException"> 
    /// Thrown when any exception occurs during retrieving the variable values.
    /// </exception>
    abstract member RetrieveVariableValue<'T> : name: string -> IVariableValue<'T>

    /// <summary>
    /// Try and retrieve the variable values associated with the variable id, <paramref cref="id"/>
    /// </summary>
    /// <param name="id">The id of the variable.</param>
    /// <returns>
    /// True if a <see cref="IVariableResult"/> corresponding with 
    /// <paramref name="id"/> can be retrieved; false otherwise.
    /// If a variable value could be stored it is stored in the out variable. 
    /// </returns>
    /// <remarks>
    /// If an error occurs, false will be returned.
    /// </remarks>
    // TODO
    // abstract member TryRetrieveVariableValue<'T> : id: VariableID -> (bool * IVariableValue<'T>)

    /// <summary>
    /// Try and retrieve the variable values associated with the variable name, <paramref cref="name"/>
    /// </summary>
    /// <param name="name">The name of the variable.</param>
    /// <returns>
    /// True if a <see cref="IVariableResult"/> corresponding with 
    /// <paramref name="name"/> can be retrieved; false otherwise.
    /// If a variable value could be stored it is stored in the out variable. 
    /// </returns>
    /// <remarks>
    /// If an error occurs, false will be returned.
    /// </remarks>
    // TODO
    // abstract member TryRetrieveVariableValue<'T> : name: string -> (bool * IVariableValue<'T>)

    /// <summary>
    /// Retrieve the dimension names associated with <paramref name="id"/>. 
    /// </summary>
    /// <param name="id">The id of the variable</param>
    /// <returns>
    /// The sequence of dimension names.
    /// </returns>
    /// <exception cref="Exceptions.NetCDFException"> 
    /// Thrown when any exception occurs during retrieving the variable values.
    /// </exception>
    /// <remarks>
    /// The order corresponds with the <see cref="IvariableValue.Shape"/>. 
    /// </remarks>
    // TODO
    // abstract member RetrieveDimensionNames : id: VariableID -> seq<string>

    /// <summary>
    /// Retrieve the dimension names associated with <paramref name="name"/>. 
    /// </summary>
    /// <param name="id">The name of the variable</param>
    /// <returns>
    /// The sequence of dimension names.
    /// </returns>
    /// <exception cref="Exceptions.NetCDFException"> 
    /// Thrown when any exception occurs during retrieving the variable values.
    /// </exception>
    /// <remarks>
    /// The order corresponds with the <see cref="IvariableValue.Shape"/>. 
    /// </remarks>
    // TODO
    // abstract member RetrieveDimensionNames : name: string -> seq<string>

    /// <summary>
    /// Retrieve the attribute with the specified <paramref name="attributeName"/> from
    /// the variable associated with the specified <paramref name="variableID"/>.
    /// </summary>
    /// <param name="VariableID">The variable id</param>
    /// <param name="attributeName">The attribute name</param>
    /// <returns>
    /// The attribute value associated with <paramref name="attributeName"/> from the 
    /// variable associated with <paramref name="variableID"/>.
    /// </returns>
    /// <exception cref="Exceptions.NetCDFException"> 
    /// Thrown when any exception occurs during retrieving the variable values.
    /// </exception>
    abstract member RetrieveVariableAttribute<'T> : VariableID * string -> IAttributeValue<'T>

    /// <summary>
    /// Retrieve the attribute with the specified <paramref name="attributeName"/> from
    /// the variable associated with the specified <paramref name="variableName"/>.
    /// </summary>
    /// <param name="VariableName">The variable name</param>
    /// <param name="attributeName">The attribute name</param>
    /// <returns>
    /// The attribute value associated with <paramref name="attributeName"/> from the 
    /// variable associated with <paramref name="variableName"/>.
    /// </returns>
    /// <exception cref="Exceptions.NetCDFException"> 
    /// Thrown when any exception occurs during retrieving the variable values.
    /// </exception>
    abstract member RetrieveVariableAttribute<'T> : string * string -> IAttributeValue<'T>

    /// <summary>
    /// Try and retrieve the attribute with the specified <paramref name="attributeName"/> from
    /// the variable associated with the specified <paramref name="variableID"/>.
    /// </summary>
    /// <param name="VariableID">The variable id</param>
    /// <param name="attributeName">The attribute name</param>
    /// <returns>
    /// True if the attribute value associated with <paramref name="attributeName"/> 
    /// can be retrieved from the  variable associated with <paramref name="variableID"/>.
    /// The value is returned in the out variable.
    /// </returns>
    /// <remarks>
    /// If an error occurs, false will be returned.
    /// </remarks>
    // TODO
    // abstract member TryRetrieveAttribute<'T> : VariableID * string -> (bool * 'T)

    /// <summary>
    /// Try and retrieve the attribute with the specified <paramref name="attributeName"/> from
    /// the variable associated with the specified <paramref name="variableName"/>.
    /// </summary>
    /// <param name="VariableName">The variable name</param>
    /// <param name="attributeName">The attribute name</param>
    /// <returns>
    /// True if the attribute value associated with <paramref name="attributeName"/> 
    /// can be retrieved from the  variable associated with <paramref name="variableName"/>.
    /// The value is returned in the out variable.
    /// </returns>
    /// <remarks>
    /// If an error occurs, false will be return.
    /// </remarks>
    // TODO
    // abstract member TryRetrieveAttribute<'T> : string * string -> (bool * 'T)

    /// <summary>
    /// Retrieve all variables that have an attribute specified with 
    /// <paramref name="attributeName"/>.
    /// </summary>
    /// <param name="attributeName">The name of the attribute that should be associated with the variable</param>
    /// <returns>
    /// The sequence of VariableIDs which have the specified <pararmrefm name="attributeName"/>
    /// </returns>
    /// <exception cref="Exceptions.NetCDFException"> 
    /// Thrown when any exception occurs during retrieving the variable values.
    /// </exception>
    abstract member RetrieveVariablesWithAttribute : attributeName: string -> seq<VariableID>

    /// <summary>
    /// Retrieve all variables that have an attribute specified with 
    /// <paramref name="attributeName"/> which has the value equal to <paramref name="attributeValue"/>.
    /// </summary>
    /// <param name="attributeName">The name of the attribute that should be associated with the variable</param>
    /// <param name="attributeValue"The value that variables should contain in the specified attribute name </param>
    /// <returns>
    /// The sequence of VariableIDs which have the specified <pararmrefm name="attributeName"/>
    /// </returns>
    /// <exception cref="Exceptions.NetCDFException"> 
    /// Thrown when any exception occurs during retrieving the variable values.
    /// </exception>
    abstract member RetrieveVariablesWithAttributeWithValue<'T when 'T : equality> : string * 'T -> seq<VariableID> 

