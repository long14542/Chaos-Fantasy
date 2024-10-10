using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    
    public float currentSpeed;
    public float currentDamage;
    public float currentHealth;

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

    // Collision with the player
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CharacterHandler player = collision.gameObject.GetComponent<CharacterHandler>();
            player.TakeDamage(currentDamage); // Use currentDamage in case of future damage buffs
        }
    }
}
