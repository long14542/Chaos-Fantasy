using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItemData", menuName = "ScriptableObjects/Passive Item")]
public class PassiveItemData : ItemData
{
    public GameObject prefab;
    public float multiplier;
    public int multiplierUpNextLevel;
}
