using UnityEngine;

public class ExperienceGem : MonoBehaviour, InterfaceCollectibles
{
    public int expGranted;
    public void Collect()
    {
        CharacterHandler player = FindFirstObjectByType<CharacterHandler>();
        player.IncreaseExp(expGranted);
        Destroy(gameObject);
    }

}
