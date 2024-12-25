using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItemData", menuName = "ScriptableObjects/Passive Item")]
public class PassiveItemData : ItemData
{
    public GameObject prefab;
    public float multiplier;
    public float multiplierUpNextLevel;
    public List<string> Descriptions;
}
