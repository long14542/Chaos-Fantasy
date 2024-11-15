using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    EnemyMovement movement;
    EnemySpawner spawner;
    DropRateManager drop;
    public string enemyName;

    public float currentSpeed;
    public float currentDamage;
    public float currentHealth;

    private CircleCollider2D collide;

    // Use Awake because it is called before Start
    void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        spawner = FindFirstObjectByType<EnemySpawner>();
        drop = GetComponent<DropRateManager>();
        currentDamage = enemyData.Damage;
        currentSpeed = enemyData.Speed;
        currentHealth = enemyData.MaxHealth;
        collide = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        // Check for nearby colliders within collider radius
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, collide.radius);

        foreach (Collider2D collider in nearbyObjects)
        {
            // Ignore self-collisions
            if (collider.gameObject == this.gameObject) continue;

            // Check if the collider is an enemy
            if (collider.CompareTag("Enemy"))
            {
                HandleOverlap(collider, 5f);
            }
            else if (collider.CompareTag("Player"))
            {
                HandleOverlap(collider, 10f);
                CharacterHandler player = collider.GetComponent<CharacterHandler>();
                player.TakeDamage(currentDamage); // Damage the player if close
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        movement.Knockback(5f, 0.2f);

        DamagePopUp.Create(transform.position, dmg);

        // Deactivate object if current health is lower or equal to 0
        if (currentHealth <= 0)
        {
            drop.DropPickUp();
            ObjectPools.EnqueueObject(this, enemyName);
            spawner.enemiesAlive -= 1;
        }
    }

    void HandleOverlap(Collider2D other, float force)
    {
        Vector2 direction = (Vector2)(transform.position - other.transform.position);
        transform.position += (Vector3)(force * Time.deltaTime * direction.normalized);
    }

}
