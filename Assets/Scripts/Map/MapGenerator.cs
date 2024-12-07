using System;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> chunks;
    public GameObject player;
    public float checkRadius;
    public LayerMask terrainMask;
    public GameObject currentChunk;

    private Vector3 playerLastPosition;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxDistance; // MUST BE GREATER THAN THE LENGTH AND WIDTH OF THE CHUNK
    private float currentDistance;
    private float OptimizerCooldown;
    public float OptimizerCooldownDuration;

    void Awake()
    {
        // Load map data
        chunks = MapSelector.LoadMap();
        MapSelector.instance.DestroySingleton();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerLastPosition = player.transform.position;
        // Spawn the first chunk before checking
        SpawnChunk(playerLastPosition);
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

        // Check the player's moving direction by substracting the current position to the last position
        Vector3 moveDir = player.transform.position - playerLastPosition;
        playerLastPosition = player.transform.position;

        // Get the direction name, that is also the name of the spawn point
        string directionName = GetDirectionName(moveDir);

        CheckAndSpawnChunk(directionName);

        // Check and spawn adjacent chunks
        if (directionName == "Right Up")
        {
            CheckAndSpawnChunk("Right");
            CheckAndSpawnChunk("Up");
        }
        else if (directionName == "Right Down")
        {
            CheckAndSpawnChunk("Right");
            CheckAndSpawnChunk("Down");
        }
        else if (directionName == "Left Up")
        {
            CheckAndSpawnChunk("Left");
            CheckAndSpawnChunk("Up");
        }
        else if (directionName == "Left Down")
        {
            CheckAndSpawnChunk("Left");
            CheckAndSpawnChunk("Down");
        }
        else if (directionName == "Left")
        {
            CheckAndSpawnChunk("Left Up");
            CheckAndSpawnChunk("Left Down");
        }
        else if (directionName == "Right")
        {
            CheckAndSpawnChunk("Right Up");
            CheckAndSpawnChunk("Right Down");
        }
        else if (directionName == "Down")
        {
            CheckAndSpawnChunk("Left Down");
            CheckAndSpawnChunk("Right Down");
        }
        else if (directionName == "Up")
        {
            CheckAndSpawnChunk("Left Up");
            CheckAndSpawnChunk("Right Up");
        }

    }

    void CheckAndSpawnChunk(string direction)
    {
        // Check if the current chunk directionName's spawn point is not colliding with anything in the terrainMask
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find(direction).position, checkRadius, terrainMask))
        {
            // Spawn chunk at that spawn point position
            SpawnChunk(currentChunk.transform.Find(direction).position);
        }
    }

    string GetDirectionName(Vector3 direction)
    {
        direction = direction.normalized;

        // Check if the player is moving horizontally more than vertically
        if (Math.Abs(direction.x) > Math.Abs(direction.y))
        {
            // If the player moving up
            if (direction.y > 0.5f)
            {
                return direction.x > 0 ? "Right Up" : "Left Up";
            }
            // If the player moving down
            else if (direction.y < -0.5f)
            {
                return direction.x > 0 ? "Right Down" : "Left Down";
            }
            // If the player not moving up or down
            else
            {
                return direction.x > 0 ? "Right" : "Left";
            }
        }
        // If the player move vertically more than horizontally
        else
        {
            // If the player moving right
            if (direction.x > 0.5f)
            {
                return direction.y > 0 ? "Right Up" : "Right Down";
            }
            // If the player moving left
            else if (direction.x < -0.5f)
            {
                return direction.y > 0 ? "Left Up" : "Left Down";
            }
            // If the player not moving right or left
            else
            {
                return direction.y > 0 ? "Up" : "Down";
            }
        }
    }

    void SpawnChunk(Vector3 spawnPosition)
    {
        // Randomize chunk and spawn it
        int rand = UnityEngine.Random.Range(0, chunks.Count);
        latestChunk = Instantiate(chunks[rand], spawnPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    // If the chunk is far enough from the player then disable it, else enable it
    void OptimizeChunk()
    {
        // We don't want to check every frame so we set a cooldown before we can optimize
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
