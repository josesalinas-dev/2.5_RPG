using System.Collections.Generic;
using UnityEngine;
using RPGInterfaces;

/// <summary>
/// A generic, reusable object pooling system.
/// Reduces garbage collection and CPU spikes caused by frequent Instantiate/Destroy calls.
/// </summary>
public class ObjectPooler : MonoBehaviour, IObjectPooler
{
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        ServiceLocator.RegisterService<IObjectPooler>(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.UnregisterService<IObjectPooler>();
    }

    /// <summary>
    /// Spawns an object from the pool. If no inactive objects are available, instantiates a new one.
    /// </summary>
    public GameObject SpawnFromPool(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (prefab == null) return null;

        string poolKey = prefab.name;

        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<GameObject>());
        }

        GameObject objectToSpawn = null;

        // Try to find an inactive object in the pool
        if (poolDictionary[poolKey].Count > 0)
        {
            objectToSpawn = poolDictionary[poolKey].Dequeue();
        }

        // If no inactive object is available, instantiate a new one
        if (objectToSpawn == null)
        {
            objectToSpawn = Instantiate(prefab);
            objectToSpawn.name = prefab.name; // Keep the same name as the key
        }

        // Set its properties
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        if (parent != null)
        {
            objectToSpawn.transform.SetParent(parent);
        }
        else
        {
            objectToSpawn.transform.SetParent(null);
        }
        
        objectToSpawn.SetActive(true);

        return objectToSpawn;
    }

    /// <summary>
    /// Returns an object to its corresponding pool queue to be reused later.
    /// </summary>
    public void ReturnToPool(GameObject objectToReturn)
    {
        if (objectToReturn == null) return;

        objectToReturn.SetActive(false);
        objectToReturn.transform.SetParent(this.transform); // Optional: Parent to pooler to keep hierarchy clean

        string poolKey = objectToReturn.name;

        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<GameObject>());
        }

        poolDictionary[poolKey].Enqueue(objectToReturn);
    }
}
