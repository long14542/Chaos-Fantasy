using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> chunks;
    public GameObject player;
    public float checkRadius;
    public LayerMask terrainMask;
    public GameObject currentChunk;

    private Vector3 freeChunkPosition;
    private PlayerMovement pm;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxDistance; // MUST BE GREATER THAN THE LENGHT AND WIDTH OF THE CHUNK
    private float currentDistance;
    private float OptimizerCooldown;
    public float OptimizerCooldownDuration;
    // Start is called before the first frame update
    void Start()
    {
        // reference PlayerMovement script to access PlayerMovement methods and properties
        pm = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckfreeChunk();
        OptimizeChunk();
    }

    void CheckfreeChunk()
    {
        if (!currentChunk)
        {
            return;
        }

        // Check if the player move right
        if (pm.moveDir.x > 0 && pm.moveDir.y == 0)
        {
            // Check if the currentChunk's RightChunkSpawnPoint is not colliding with anything in the terrainMask
            // the same for other operations but with different spawn point
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("RightChunkSpawnPoint").position, checkRadius, terrainMask))
            {
                // Update freeChunkPosition
                freeChunkPosition = currentChunk.transform.Find("RightChunkSpawnPoint").position;
                SpawnChunk();
            }
        }

        // Check if the player move left
        if (pm.moveDir.x < 0 && pm.moveDir.y == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("LeftChunkSpawnPoint").position, checkRadius, terrainMask))
            {
                freeChunkPosition = currentChunk.transform.Find("LeftChunkSpawnPoint").position;
                SpawnChunk();
            }
        }

        // Check if the player move up
        if (pm.moveDir.x == 0 && pm.moveDir.y > 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("UpChunkSpawnPoint").position, checkRadius, terrainMask))
            {
                freeChunkPosition = currentChunk.transform.Find("UpChunkSpawnPoint").position;
                SpawnChunk();
            }
        }
        
        // Check if the player move down
        if (pm.moveDir.x == 0 && pm.moveDir.y < 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("DownChunkSpawnPoint").position, checkRadius, terrainMask))
            {
                freeChunkPosition = currentChunk.transform.Find("DownChunkSpawnPoint").position;
                SpawnChunk();
            }
        }

        // Check if the player move right up
        if (pm.moveDir.x > 0 && pm.moveDir.y > 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("RightUpChunkSpawnPoint").position, checkRadius, terrainMask))
            {
                freeChunkPosition = currentChunk.transform.Find("RightUpChunkSpawnPoint").position;
                SpawnChunk();
            }
        }

        // Check if the player move right down
        if (pm.moveDir.x > 0 && pm.moveDir.y < 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("RightDownChunkSpawnPoint").position, checkRadius, terrainMask))
            {
                freeChunkPosition = currentChunk.transform.Find("RightDownChunkSpawnPoint").position;
                SpawnChunk();
            }
        }

        // Check if the player move left up
        if (pm.moveDir.x < 0 && pm.moveDir.y > 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("LeftUpChunkSpawnPoint").position, checkRadius, terrainMask))
            {
                freeChunkPosition = currentChunk.transform.Find("LeftUpChunkSpawnPoint").position;
                SpawnChunk();
            }
        }

        // Check if the player move left down
        if (pm.moveDir.x < 0 && pm.moveDir.y < 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("LeftDownChunkSpawnPoint").position, checkRadius, terrainMask))
            {
                freeChunkPosition = currentChunk.transform.Find("LeftDownChunkSpawnPoint").position;
                SpawnChunk();
            }
        }
    }
    void SpawnChunk()
    {
        // Randomize chunk and spawn it
        int rand = Random.Range(0, chunks.Count);
        latestChunk = Instantiate(chunks[rand], freeChunkPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    // If the chunk is far enough from the player then disable it, else enable it
    void OptimizeChunk()
    {
        // We don't want to check every frame so we set a cooldown before we can 
        OptimizerCooldown -= Time.deltaTime;
        if (OptimizerCooldown <= 0)
        {
            OptimizerCooldown = OptimizerCooldownDuration;
        }
        else
        {
            return;
        }
        foreach (GameObject chunk in spawnedChunks)
        {
            currentDistance = Vector3.Distance(player.transform.position, chunk.transform.position);

            if (currentDistance > maxDistance)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
