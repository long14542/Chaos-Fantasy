using UnityEngine;
// Wings increases player's movement speed
public class Wings : PassiveItem
{
    private PlayerMovement movement;

    protected override void Start()
    {
        movement = FindFirstObjectByType<PlayerMovement>();
        base.Start();
    }
    protected override void ApplyModifier()
    {
        movement.currentMoveSpeed = owner.characterData.MoveSpeed * (1 + currentMultiplier / 100f);
    }

    public override bool LevelUp()
    {
        if (!base.LevelUp()) return false;

        currentMultiplier += passiveItemData.multiplierUpNextLevel;
        ApplyModifier();
        return true;
    }
}
