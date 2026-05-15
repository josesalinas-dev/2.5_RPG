using UnityEngine;

namespace RPGInterfaces
{
    /// <summary>
    /// Interface for the Object Pooling system.
    /// Used to spawn and recycle GameObjects to prevent Garbage Collection spikes.
    /// </summary>
    public interface IObjectPooler
    {
        GameObject SpawnFromPool(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null);
        void ReturnToPool(GameObject objectToReturn);
    }
}
