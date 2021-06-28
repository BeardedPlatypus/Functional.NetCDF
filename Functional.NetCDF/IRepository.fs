namespace BeardedPlatypus.Functional.NetCDF

/// <summary>
/// <see cref="IRepository"/> defines the interface with which to 
/// query the different aspects of a NetCDF file. 
/// </summary>
[<Interface>]
type public IRepository = 
    /// <summary>
    /// Retrieve the variable values associated with the variable id, <paramref name="id"/>
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
    /// Retrieve the variable values associated with the variable name, <paramref name="name"/>
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
    abstract member RetrieveVariableAttribute<'T> : variableID: VariableID * attributeName: string -> IAttributeValue<'T>

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
    abstract member RetrieveVariableAttribute<'T> : variableName: string * attributeName: string -> IAttributeValue<'T>

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
    /// <param name="attributeValue">The value that variables should contain in the specified attribute name </param>
    /// <returns>
    /// The sequence of VariableIDs which have the specified <pararmrefm name="attributeName"/>
    /// </returns>
    /// <exception cref="Exceptions.NetCDFException"> 
    /// Thrown when any exception occurs during retrieving the variable values.
    /// </exception>
    abstract member RetrieveVariablesWithAttributeWithValue<'T when 'T : equality> : string * 'T -> seq<VariableID> 

