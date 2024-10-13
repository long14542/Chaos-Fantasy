using System.Collections.Generic;
using UnityEngine;

// Inventory system to manage the items and weapons that the player currently have
public class Inventory : MonoBehaviour
{
    public Dictionary<string, WeaponsMother> weaponSlots = new(6);
    public Dictionary<string, PassiveItemMother> itemSlots = new(6);

    public void AddWeapon(string name, WeaponsMother weapon)
    {
        weaponSlots[name] = weapon;
    }

    public void AddItem(string name, PassiveItemMother item)
    {
        itemSlots[name] = item;
    }

    //public List<WeaponsMother> weaponSlots = new(6);
    //public int[] weaponLevels = new int[6];
    //public List<PassiveItemMother> itemSlots = new(6);
    //public int[] itemLevels = new int[6];

    //public void AddWeapon(int id, WeaponsMother weapon)
    //{
    //    weaponSlots[id] = weapon;
    //    weaponLevels[id] = weapon.weaponData.Level;
    //}

    //public void AddItem(int id, PassiveItemMother item)
    //{
    //    itemSlots[id] = item;
    //    itemLevels[id] = item.passiveItemData.Level;
    //}

}
