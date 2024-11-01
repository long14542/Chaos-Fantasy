using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Inventory system to manage the items and weapons that the player currently have
public class Inventory : MonoBehaviour
{
    public List<Slot> weaponSlots = new(6);
    public List<Slot> passiveItemSlots = new(6);

    // Each slot will hold an item and its icon
    [System.Serializable]
    public class Slot
    {
        public Item item;
        public Image image;

        public void AssignItem(Item assignedItem)
        {
            item = assignedItem;
            if (item is Weapon)
            {
                Weapon weapon = item as Weapon;
                image.sprite = weapon.weaponData.icon;
            }
            else
            {
                PassiveItem passiveItem = item as PassiveItem;
                image.sprite = passiveItem.passiveItemData.icon;
            }
        }
    }

    public void AddWeapon(int id, Weapon weapon)
    {
        weaponSlots[id].AssignItem(weapon);
    }

    public void AddItem(int id, PassiveItem item)
    {
        passiveItemSlots[id].AssignItem(item);
    }

}
