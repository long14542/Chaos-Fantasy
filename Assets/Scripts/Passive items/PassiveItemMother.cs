using UnityEngine;

// Mother class of all passive items
public class PassiveItemMother : MonoBehaviour
{
    protected CharacterHandler player;
    public PassiveItemScriptableObject passiveItemData;

    protected virtual void ApplyModifier()
    {
       // Base method for child classes to apply boost values
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<CharacterHandler>();
        ApplyModifier();
    }

}
