using UnityEngine;
// Spinach increases all damage done by the player
public class Spinach : PassiveItemMother
{
    protected override void ApplyModifier()
    {
        player.currentMight *= 1 + currentMultiplier / 100f;
    }

    protected override void LevelUpItem()
    {
        base.LevelUpItem();

        currentMultiplier += passiveItemData.MultiplierUpNextLevel;
        //Debug.Log($"spinach: lev {currentLevel}, multi {currentMultiplier}");
    }
}
