// Wings increases player's movement speed
public class Wings : PassiveItemMother
{
    protected override void ApplyModifier()
    {
        player.currentMoveSpeed *= 1 + passiveItemData.Multiplier / 100f;
    }
}
