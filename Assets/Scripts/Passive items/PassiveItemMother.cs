using UnityEngine;

// Mother class of all passive items
public class PassiveItemMother : MonoBehaviour
{
    protected CharacterHandler player;
    public PassiveItemScriptableObject passiveItemData;

    public int currentLevel;
    public float currentMultiplier;

    protected virtual void ApplyModifier()
    {
       // Base method for child classes to apply boost values
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<CharacterHandler>();
        currentLevel = passiveItemData.Level;
        currentMultiplier = passiveItemData.Multiplier;
        ApplyModifier();
    }

    protected virtual void LevelUpItem()
    {
        if (currentLevel >= passiveItemData.MaxLevel)
        {
            return;
        }
        currentLevel += 1;
    }

}
