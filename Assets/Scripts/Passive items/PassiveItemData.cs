using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItemData", menuName = "ScriptableObjects/Passive Item")]
public class PassiveItemData : ItemData
{
    public float multiplier;
    public int multiplierUpNextLevel;
}
