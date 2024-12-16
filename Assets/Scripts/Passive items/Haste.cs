// Haste decreases all weapons cooldown duration
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
        currentMultiplier += passiveItemData.multiplierUpNextLevel;
        ApplyModifier();
        return true;
    }
}