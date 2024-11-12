using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    EnemySpawner spawner;
    DropRateManager drop;
    public string enemyName;

    public float currentSpeed;
    public float currentDamage;
    public float currentHealth;

    // Use Awake because it is called before Start
    void Awake()
    {
        spawner = FindFirstObjectByType<EnemySpawner>();
        drop = GetComponent<DropRateManager>();
        currentDamage = enemyData.Damage;
        currentSpeed = enemyData.Speed;
        currentHealth = enemyData.MaxHealth;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        DamagePopUp.Create(transform.position, dmg);

        // Deactivate object if current health is lower or equal to 0
        if (currentHealth <= 0)
        {
            drop.DropPickUp();
            ObjectPools.EnqueueObject(this, enemyName);
            spawner.enemiesAlive -= 1;
        }
    }

    // Collision with the player
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterHandler player = collision.gameObject.GetComponent<CharacterHandler>();
            player.TakeDamage(currentDamage); // Use currentDamage in case of future damage buffs
        }
    }


}
