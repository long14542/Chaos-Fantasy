using UnityEngine;

// Base class for weaponData and itemData
public abstract class ItemData : ScriptableObject
{
    public Sprite icon;
    public int maxLevel;
    public string itemName;
}
