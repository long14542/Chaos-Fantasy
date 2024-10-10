using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided gameObject has the interface collectibles 
        if (collision.gameObject.TryGetComponent(out InterfaceCollectibles collectible))
        {
            // Call that object Collect method
            collectible.Collect();
        }
    }
}
