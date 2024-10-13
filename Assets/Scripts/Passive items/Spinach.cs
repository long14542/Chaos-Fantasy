// Spinach increases all damage done by the player
public class Spinach : PassiveItemMother
{
    protected override void ApplyModifier()
    {
        player.currentMight *= 1 + passiveItemData.Multiplier / 100f;
    }
}
