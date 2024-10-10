using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private CharacterHandler player;
    private CircleCollider2D magnetCollector;
    public float pullSpeed;

    void Start()
    {
        player = FindFirstObjectByType<CharacterHandler>();
        magnetCollector = GetComponent<CircleCollider2D>();

        magnetCollector.radius = player.currentMagnet;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided gameObject has the interface collectibles 
        if (collision.gameObject.TryGetComponent(out InterfaceCollectibles collectible))
        {
            // Pulling the collectible toward the player position
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            // Find the vector direction to the player by subtracting the current player position 
            //and the collision position then normalized it
            Vector2 forceDirection = (transform.position - collision.transform.position).normalized;
            // Apply force
            rb.AddForce(forceDirection * pullSpeed);


            // Call that object Collect method
            collectible.Collect();
        }
    }
}
