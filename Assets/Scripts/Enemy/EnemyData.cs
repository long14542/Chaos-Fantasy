using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]
public class EnemyData : ScriptableObject
{
    // Base stats
    public string e_name;

    public string deathAnimName;
    
    [SerializeField]
    private float speed;
    public float Speed { get => speed; private set => speed = value; }

    [SerializeField]
    private float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    private float damage;
    public float Damage { get => damage; private set => damage = value; }
}
