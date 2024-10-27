using System.Collections.Generic;
using UnityEngine;

// ObjectController pre-initiates all the objects that the game need
public class ObjectController : MonoBehaviour
{
    public List<EnemyHandler> enemyPrefabs;

    void Awake()
    {
        for (int i = 0; i < enemyPrefabs.Count; i++)
            ObjectPools.SetupPool(enemyPrefabs[i], 50, enemyPrefabs[i].name);
    }
}
