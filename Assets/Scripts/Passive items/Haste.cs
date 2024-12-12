using UnityEngine;
// Spinach increases all damage done by the player
public class Haste : PassiveItem
{
    protected override void ApplyModifier()
    {
        owner.currentCooldownReduction = owner.characterData.CooldownReduction + currentMultiplier;
        Debug.Log("update owner cooldown reduction, " + owner.currentCooldownReduction + ", curren multi: " + currentMultiplier);
        
        owner.ApplyWeaponBuffs();
    }

    public override void LevelUp()
    {
        base.LevelUp();

        currentMultiplier += passiveItemData.multiplierUpNextLevel;
        ApplyModifier();
    }
}