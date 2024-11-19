using UnityEngine;
public class Item : MonoBehaviour
{
    public int currentLevel, maxLevel;
    public string itemName;
    protected CharacterHandler owner;
    public virtual void Initialize(ItemData data)
    {
        currentLevel = 1;
        int maxLevel1 = data.maxLevel;
        maxLevel = maxLevel1;
        itemName = data.itemName;
        CharacterHandler characterHandler = FindFirstObjectByType<CharacterHandler>();
        owner = characterHandler;
    }
    public virtual bool CanLevelUp()
    {
        return currentLevel < maxLevel;
    }

    public virtual void LevelUp()
    {
        if (CanLevelUp())
        {
            currentLevel += 1;
        }
    }
}