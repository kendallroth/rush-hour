using System;
using UnityEngine;

/// <summary>
/// Attribute that requires implementation of the provided interface
/// </summary>
public class RequireInterfaceAttribute : PropertyAttribute
{
    #region Fields
    public Type RequiredType { get; private set; }
    #endregion


    #region Custom Methods
    /// <summary>
    /// Requiring implementation of the <see cref="T:RequireInterfaceAttribute"/> interface.
    /// </summary>
    /// <param name="type">Interface type</param>
    public RequireInterfaceAttribute(Type type)
    {
        RequiredType = type;
    }
    #endregion
}
