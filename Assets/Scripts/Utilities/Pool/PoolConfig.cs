using System;
using UnityEngine;

[Serializable]
public class PoolConfig
{
    #region Fields
    public string Name;
    [SerializeField]
    [RequireInterface(typeof(IPoolObject))]
    [Tooltip("Requires a reference to the IPoolObject component itself, rather than its GameObject!")]
    private UnityEngine.Object poolObject = default;
    [Range(1, 100)]
    public int Size = 10;
    public Transform SpawnParent = default;

    // NOTE: This is not serialized
    public IPoolObject PoolObject => poolObject as IPoolObject;
    #endregion
}

