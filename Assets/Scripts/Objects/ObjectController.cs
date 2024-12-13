using System.Collections.Generic;
using UnityEngine;

// ObjectController pre-initiates all the objects that the game need
public class ObjectController : MonoBehaviour
{
    public List<EnemyHandler> enemyPrefabs;
    public DamagePopUp popUpPrefab;

    void Awake()
    {
        // Clear the pool when enter a new Gameplay Scene
        ObjectPools.ClearPools();

        for (int i = 0; i < enemyPrefabs.Count; i++)
            ObjectPools.SetupPool(enemyPrefabs[i], 50, enemyPrefabs[i].enemyData.name);
        
        ObjectPools.SetupPool(popUpPrefab, 10, "DamagePopUp");
    }
}
