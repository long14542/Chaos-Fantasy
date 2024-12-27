// Recover character's health every second
public class Potato : PassiveItem
{
    protected override void ApplyModifier()
    {
        owner.currentRecovery += currentMultiplier;
    }

    public override bool LevelUp()
    {
        if (!base.LevelUp()) return false;
        currentMultiplier += passiveItemData.multiplierUpNextLevel;
        ApplyModifier();
        return true;
    }
}
