using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Inventory system to manage the items and weapons that the player currently have
public class Inventory : MonoBehaviour
{
    public List<WeaponsMother> weaponSlots = new(6);
    public List<PassiveItemMother> itemSlots = new(6);

    public List<Image> weaponImages = new(6);
    public List<Image> itemImages = new(6);

    public void AddWeapon(int id, WeaponsMother weapon)
    {
        weaponSlots[id] = weapon;
        weaponImages[id].sprite = weapon.weaponData.Icon;
    }

    public void AddItem(int id, PassiveItemMother item)
    {
        itemSlots[id] = item;
        itemImages[id].sprite = item.passiveItemData.Icon;
    }

}
