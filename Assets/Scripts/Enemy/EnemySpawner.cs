using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // A wave holds the information of what groups of enemies, spawn interval, counts
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        public int waveQuota; // Minimum number of enemies to spawn in the wave
        public int totalSpawnCount; // Number of spawned enemies in the wave
        public float spawnInterval;
    }

    // A group of enemies
    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount; // Mininum of enemies to spawn in the group
        public int spawnCount; // Number of spawned enemies of this type in the wave
    }

    public List<Wave> waves;
    public int currentWave;

    private Transform player;

    [Header("Spawn Settings")]
    float spawnTimer;
    public int enemiesAlive;
    public int maxEnemies; // Maximum number of enemies alive
    public bool maxEnemiesReached = false; // To know whether the maximum number of enemies has been reached or not
    public float waveInterval; // Wave interval between each wave

    [Header("Spawn Position")]
    public List<Transform> spawnPoints;

    void Start()
    {
        player = FindFirstObjectByType<CharacterHandler>().transform;
        CalculateWaveQuota();
    }

    void Update()
    {
        // Check to see if it's time to start the next wave
        if (currentWave < waves.Count && waves[currentWave].totalSpawnCount == waves[currentWave].waveQuota)
        {
            StartCoroutine(StartNextWave());
        }

        spawnTimer += Time.deltaTime;
        // Enemy spawn after a certain amount of time
        if (spawnTimer >= waves[currentWave].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator StartNextWave()
    {
        // Wait for waveInterval seconds before starting a new wave
        yield return new WaitForSeconds(waveInterval);

        // If there are more waves to start then move on to the new wave and calculate its quota
        if (currentWave < waves.Count - 1)
        {
            currentWave += 1;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        // Calculate the number of enemies need to spawn in the wave
        foreach (EnemyGroup group in waves[currentWave].enemyGroups)
        {
            currentWaveQuota += group.enemyCount;
        }
        waves[currentWave].waveQuota = currentWaveQuota;
    }


    // This spawns enemies
    // Stop spawnning if maxEnemies is reached
    // Only spawn in a single wave until the next wave start
    void SpawnEnemies()
    {
        // Check if the number of spawned enemies in a wave has reached its quota and max number of enemies is not reached
        if (waves[currentWave].totalSpawnCount < waves[currentWave].waveQuota && !maxEnemiesReached)
        {
            // For each group, check if the number of spawned enemies in a group has reached its quota
            foreach (EnemyGroup group in waves[currentWave].enemyGroups)
            {
                if (group.spawnCount < group.enemyCount)
                {
                    // Check if the alive enemies has reached its max
                    if (enemiesAlive >= maxEnemies)
                    {
                        // No more spawning
                        maxEnemiesReached = true;
                        return;
                    }

                    // Use object pool to create enemies
                    EnemyHandler instance = ObjectPools.DequeueObject<EnemyHandler>(group.enemyName);
                    instance.gameObject.transform.position = player.position + spawnPoints[Random.Range(0, spawnPoints.Count)].position;
                    instance.gameObject.SetActive(true);

                    //Instantiate(group.enemyPrefab, player.position + spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);

                    group.spawnCount += 1;
                    waves[currentWave].totalSpawnCount += 1;
                    enemiesAlive += 1;
                }
            }
        }

        // If alive enemies is less than max enemies allowed then set maxEnemiesReached to false
        if (enemiesAlive < maxEnemies)
        {
            maxEnemiesReached = false;
        }
    }


}
