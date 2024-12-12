using UnityEngine;
// Wings increases player's movement speed
public class Wings : PassiveItem
{
    protected override void ApplyModifier()
    {
        owner.currentMoveSpeed = owner.characterData.MoveSpeed * (1 + currentMultiplier / 100f);
    }

    public override bool LevelUp()
    {
        if (!base.LevelUp()) return false;

        currentMultiplier += passiveItemData.multiplierUpNextLevel;
        ApplyModifier();
        return true;
    }
}
