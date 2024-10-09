using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    
    private float currentSpeed;
    private float currentDamage;
    private float currentHealth;

    // Use Awake because it is called before Start
    void Awake()
    {
        currentDamage = enemyData.Damage;
        currentSpeed = enemyData.Speed;
        currentHealth = enemyData.MaxHealth;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        // Destroy object if current health is lower or equal to 0
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
