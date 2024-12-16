
// Spinach increases all damage done by the player
public class Spinach : PassiveItem
{
    protected override void ApplyModifier()
    {
        owner.currentMight = owner.characterData.Might * (1 + currentMultiplier / 100f);
    }

    public override bool LevelUp()
    {
        if (!base.LevelUp()) return false;

        currentMultiplier += passiveItemData.multiplierUpNextLevel;
        ApplyModifier();
        return true;
    }
}
