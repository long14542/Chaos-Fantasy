using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    MapGenerator mg;
    public GameObject targetChunk;

    // Start is called before the first frame update
    void Start()
    {
        // reference MapGenerator script to access MapGenerator methods and properties
        mg = FindObjectOfType<MapGenerator>();
    }

    // Check which chunk is the player in and set it to currentChunk
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mg.currentChunk = targetChunk;
        }
    }

    // If the player leave it then set the chunk to null
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (mg.currentChunk == targetChunk)
            {
                mg.currentChunk = null;
            }
        }
    }
}
