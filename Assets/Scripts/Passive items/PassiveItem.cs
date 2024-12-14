
// Mother class of all passive items
public class PassiveItem : Item
{
    public PassiveItemData passiveItemData;

    public float currentMultiplier;

    public virtual void Initialize(PassiveItemData data)
    {
        base.Initialize(data);

        currentMultiplier = data.multiplier;
    }

    protected virtual void ApplyModifier()
    {
       // Base method for child classes to apply boost values
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Initialize(passiveItemData);
        ApplyModifier();
    }

}
