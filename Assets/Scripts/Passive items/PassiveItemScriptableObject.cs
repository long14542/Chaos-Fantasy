using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItemScriptableObject", menuName = "ScriptableObjects/Passive Item")]
public class PassiveItemScriptableObject : ScriptableObject
{
    [SerializeField]
    private float multiplier;
    public float Multiplier { get => multiplier; private set => multiplier = value; }

    [SerializeField]
    private int level;
    public int Level { get => level; private set => level = value; }

    [SerializeField]
    private int multiplierUpNextLevel;
    public int MultiplierUpNextLevel { get => multiplierUpNextLevel; private set => multiplierUpNextLevel = value; }

    [SerializeField]
    private int maxLevel;
    public int MaxLevel { get => maxLevel; private set => maxLevel = value; }
}
