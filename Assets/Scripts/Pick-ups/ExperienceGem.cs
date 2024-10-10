using UnityEngine;

public class ExperienceGem : MonoBehaviour, InterfaceCollectibles
{
    public int expGranted;
    public void Collect()
    {
        CharacterHandler player = FindFirstObjectByType<CharacterHandler>();
        player.IncreaseExp(expGranted);
    }

    // Destroy the object once it touches the player
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
