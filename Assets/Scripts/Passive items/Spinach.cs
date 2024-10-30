using UnityEngine;
// Spinach increases all damage done by the player
public class Spinach : PassiveItem
{
    protected override void ApplyModifier()
    {
        owner.currentMight *= 1 + currentMultiplier / 100f;
    }

    public override void LevelUp()
    {
        base.LevelUp();

        currentMultiplier += passiveItemData.multiplierUpNextLevel;
        //Debug.Log($"spinach: lev {currentLevel}, multi {currentMultiplier}");
    }
}
