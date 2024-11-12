using UnityEngine;
// Wings increases player's movement speed
public class Wings : PassiveItem
{
    protected override void ApplyModifier()
    {
        owner.currentMoveSpeed = owner.characterData.MoveSpeed * (1 + currentMultiplier / 100f);
    }

    public override void LevelUp()
    {
        base.LevelUp();

        currentMultiplier += passiveItemData.multiplierUpNextLevel;
        ApplyModifier();
        //Debug.Log($"wings: lev {currentLevel}, multi {currentMultiplier}");
    }
}
