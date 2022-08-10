using System.Collections.Generic;
using UnityEngine;

public class PoolManager : GameSingleton<PoolManager>
{
    #region Fields
    public List<PoolConfig> Pools;
    #endregion

    private Dictionary<string, Queue<IPoolObject>> poolDictionary;


    #region Unity Methods
    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<IPoolObject>>();

        // Create and initialize the pool queues
        Pools.ForEach(p => poolDictionary.Add(p.Name, CreatePool(p)));
    }
    #endregion


    #region Custom Methods
    /// <summary>
    /// Create an object pool
    /// </summary>
    /// <param name="poolConfig">Pool configuration</param>
    /// <returns>Created object pool</returns>
    private Queue<IPoolObject> CreatePool(PoolConfig poolConfig)
    {
        Queue<IPoolObject> poolQueue = new Queue<IPoolObject>();
        Transform spawnParent = poolConfig.SpawnParent
            ? poolConfig.SpawnParent
            : TemporaryObjectsManager.Instance.TemporaryChildren;

        for (int i = 0; i < poolConfig.Size; i++)
        {
            GameObject newObject = Instantiate(poolConfig.PoolObject.GameObject, spawnParent);
            newObject.SetActive(false);
            IPoolObject poolableObject = newObject.GetComponent<IPoolObject>();
            poolableObject.OnCreate();
            poolQueue.Enqueue(poolableObject);
        }

        return poolQueue;
    }

    /// <summary>
    /// Reclaim all objects in a pool
    /// </summary>
    /// <param name="poolKey">Pool key</param>
    public void ReclaimPool(string poolKey)
    {
        if (!poolDictionary.ContainsKey(poolKey)) return;

        Queue<IPoolObject> pool = poolDictionary[poolKey];
        foreach (var poolObject in pool)
        {
            poolObject?.OnReclaim();
        }
    }

    /// <summary>
    /// Get an object from a pool
    /// </summary>
    /// <param name="poolKey">Pool key</param>
    /// <returns>Pool object</returns>
    public GameObject GetFromPool(string poolKey)
    {
        if (!poolDictionary.ContainsKey(poolKey))
        {
            Debug.LogWarning($"Pool with tag {poolKey} doesn't exist!");
            return null;
        }

        Queue<IPoolObject> pool = poolDictionary[poolKey];
        return GetFromPool(pool);
    }

    /// <summary>
    /// Get an object from a pool
    /// </summary>
    /// <param name="pool">Pool queue</param>
    /// <returns>Pool object</returns>
    // NOTE: Disabled public function until the pool is replaced by a "safer" function (queue shouldn't be exposed)
    private GameObject GetFromPool(Queue<IPoolObject> pool)
    {
        // TODO: Possibly add functionality to handle first returning pooled members that have been deactivated
        // TODO: Possibly add PooledObject script to these objects to allow easier reclaiming (via reference to pool)

        IPoolObject poolObject = pool.Dequeue();
        poolObject.GameObject.SetActive(true);

        // Pool object must be enqueued again after being retrieved
        pool.Enqueue(poolObject);

        return poolObject.GameObject;
    }

    /// <summary>
    /// Spawn an object from a pool
    /// </summary>
    /// <param name="poolKey">Pool key</param>
    /// <param name="position">Object position</param>
    /// <param name="rotation">Object rotation</param>
    /// <returns>Spawned pool object</returns>
    public GameObject SpawnFromPool(string poolKey, Vector3 position, Quaternion rotation)
    {
        GameObject spawnedObject = GetFromPool(poolKey);
        return SpawnFromAnywhere(spawnedObject, position, rotation);
    }

    /// <summary>
    /// Spawn an object from a pool
    /// </summary>
    /// <param name="pool">Pool queue</param>
    /// <param name="position">Object position</param>
    /// <param name="rotation">Object rotation</param>
    /// <returns>Spawned pool object</returns>
    // NOTE: Disabled function until the pool is replaced by a "safer" function (queue shouldn't be exposed)
    /*public GameObject SpawnFromPool(Queue<GameObject> pool, Vector3 position, Quaternion rotation)
    {
        GameObject spawnedObject = GetFromPool(pool);
        return SpawnFromAnywhere(spawnedObject, position, rotation);
    }*/

    private GameObject SpawnFromAnywhere(GameObject spawnedObject, Vector3 position, Quaternion rotation)
    {
        if (!spawnedObject) return null;

        spawnedObject.transform.position = position;
        spawnedObject.transform.rotation = rotation;

        // Call the spawn handler for the object to reset internal state
        IPoolObject poolObject = spawnedObject.GetComponent<IPoolObject>();
        poolObject?.OnSpawn();

        return spawnedObject;
    }
    #endregion
}

