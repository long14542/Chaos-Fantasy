using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    // Base stats
    // SerializedField to expose private members in inspector
    // Properties to access private member variables
    [SerializeField]
    private GameObject prefab; // Member variable
    public GameObject Prefab { get => prefab; private set => prefab = value; } // Property

    [SerializeField]
    private float speed;
    public float Speed { get => speed; private set => speed = value; }

    [SerializeField]
    private float damage;
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    private float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }

    [SerializeField]
    private int pierce;
    public int Pierce { get => pierce; private set => pierce = value; }

    [SerializeField]
    private float lifeTime;
    public float LifeTime { get => lifeTime; private set => lifeTime = value; }

    [SerializeField]
    private int level;
    public int Level { get => level; private set => level = value; }

    // Every time the weapon levels up, its damage is increased by this amount
    [SerializeField]
    private int damageUpNextLevel;
    public int DamageUpNextLevel { get => damageUpNextLevel; private set => damageUpNextLevel = value; }

    [SerializeField]
    private int maxLevel;
    public int MaxLevel { get => maxLevel; private set => maxLevel = value; }
}
