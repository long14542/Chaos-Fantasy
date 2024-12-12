using UnityEngine;
// Spinach increases all damage done by the player
public class Haste : PassiveItem
{
    protected override void ApplyModifier()
    {
        owner.currentCooldownReduction = owner.characterData.CooldownReduction + currentMultiplier;
        
        owner.ApplyWeaponBuffs();
    }

    public override bool LevelUp()
    {
        if (!base.LevelUp()) return false;
        Debug.Log("hers");
        currentMultiplier += passiveItemData.multiplierUpNextLevel;
        ApplyModifier();
        return true;
    }
}