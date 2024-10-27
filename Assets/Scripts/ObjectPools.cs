using System.Collections.Generic;
using UnityEngine;

// Pools of objects to pre-instantiate the objects before runtime
// This removes the need for creating and destroying objects while the game is running
// VERY IMPORTANT for games with many objects
public static class ObjectPools
{
    // A dictionary contains all object pool types. Ex: enemies, projectiles,....
    public static Dictionary<string, Queue<Component>> poolDictionary = new();

    // Set up an object pool of a specific type of object
    public static void SetupPool<T>(T ItemPrefab, int poolSize, string entry) where T : Component
    {
        // Create an entry for the pool 
        poolDictionary.Add(entry, new Queue<Component>());

        // Initiate the pool
        for (int i = 0; i < poolSize; i++)
        {
            T poolInstance = Object.Instantiate(ItemPrefab);
            poolInstance.gameObject.SetActive(false);
            poolDictionary[entry].Enqueue(poolInstance);
        }
    }

    // Put the objects of type T back into the queue
    public static void EnqueueObject<T>(T item, string name) where T : Component
    {
        // If the object is already inactive then return
        if (!item.gameObject.activeSelf)
        {
            return;
        }

        item.transform.position = Vector2.zero;
        poolDictionary[name].Enqueue(item);
        item.gameObject.SetActive(false);
    }

    // Push the objects of type T out of the queue to run in the game
    public static T DequeueObject<T>(string name) where T : Component
    {
        return (T)poolDictionary[name].Dequeue();
    }
}
