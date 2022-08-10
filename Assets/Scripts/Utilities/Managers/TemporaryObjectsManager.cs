using UnityEngine;

/// <summary>
/// Provide utilities for temporary objects
/// </summary>
public class TemporaryObjectsManager : GameSingleton<TemporaryObjectsManager>
{
    #region Variables
    /// <summary>
    /// Transform for temporary children to use as parent
    /// </summary>
    public Transform TemporaryChildren { get; private set; }
    #endregion


    #region Unity Methods
    private void Start()
    {
        TemporaryChildren = transform;
    }
    #endregion
}


