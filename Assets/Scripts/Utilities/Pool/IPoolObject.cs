using System;
using UnityEngine;

public interface IPoolObject
{
    #region Fields
    /// <summary>
    /// Game object of poolable object
    /// </summary>
    GameObject GameObject { get; }
    #endregion


    #region Custom Methods
    /// <summary>
    /// Called when an object is first created (setup handler)
    /// </summary>
    void OnCreate();

    /// <summary>
    /// Called each time an object is spawned (reset handler)
    /// </summary>
    void OnSpawn();

    // NOTE: Implementing classes may contain an "OnSpawnInit" method with additional parameters

    /// <summary>
    /// Called when an object is reclaimed by the pool (cleanup handler)
    /// </summary>
    void OnReclaim();
    #endregion
}

