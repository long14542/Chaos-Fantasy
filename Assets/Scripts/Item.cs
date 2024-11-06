using UnityEngine;

// Mother class for all types of items, i.e: weapon, passive item
public class Item : MonoBehaviour
{
    public int currentLevel = 1, maxLevel = 1;
    public string itemName;

    protected CharacterHandler owner;
    
    // DO NOT try to change ItemData values, this applies to the methods that override this method
    // ScriptableObjects' data are meant to be read only
    public virtual void Initialize(ItemData data)
    {
        maxLevel = data.maxLevel;
        itemName = data.itemName;
        owner = FindFirstObjectByType<CharacterHandler>();
    }

    // Check if the item has already reached the max level
    public virtual bool CanLevelUp()
    {
        return currentLevel < maxLevel;
    }

    public virtual void LevelUp()
    {
        if (CanLevelUp()) currentLevel += 1;
    }
}
