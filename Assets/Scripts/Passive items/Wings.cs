using UnityEngine;
// Wings increases player's movement speed
public class Wings : PassiveItem
{
    protected override void ApplyModifier()
    {
        owner.currentMoveSpeed *= 1 + currentMultiplier / 100f;
    }

    public override void LevelUp()
    {
        base.LevelUp();

        currentMultiplier += passiveItemData.multiplierUpNextLevel;
        //Debug.Log($"wings: lev {currentLevel}, multi {currentMultiplier}");
    }
}
