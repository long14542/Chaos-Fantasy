using UnityEngine;
// Wings increases player's movement speed
public class Wings : PassiveItemMother
{
    protected override void ApplyModifier()
    {
        player.currentMoveSpeed *= 1 + currentMultiplier / 100f;
    }

    protected override void LevelUpItem()
    {
        base.LevelUpItem();

        currentMultiplier += passiveItemData.MultiplierUpNextLevel;
        //Debug.Log($"wings: lev {currentLevel}, multi {currentMultiplier}");
    }
}
