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
        collide = GetComponent<CircleCollider2D>();
        currentDamage = enemyData.Damage;
        currentSpeed = enemyData.Speed;
        currentHealth = enemyData.MaxHealth;
    }

    void FixedUpdate()
    {
        // Only check on Objects layer to reduce the number of unncessary cheks
        LayerMask mask = LayerMask.GetMask("Objects");
        // Check for nearby colliders within collider radius
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, collide.radius, mask);

        foreach (Collider2D collider in nearbyObjects)
        {
            // Ignore self-collisions
            if (collider.gameObject == this.gameObject) continue;

            // Check if the collider is an enemy
            if (collider.CompareTag("Enemy"))
            {
                HandleOverlap(collider);
            }
            else if (collider.CompareTag("Player"))
            {
                HandleOverlap(collider);
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

    void HandleOverlap(Collider2D other)
    {
        Vector2 direction = (Vector2)(transform.position - other.transform.position);
        float distance = direction.magnitude;

        // Calculate the overlap size
        float combinedRadius = collide.radius + other.bounds.extents.x; // Assuming circular colliders
        float overlap = combinedRadius - distance;

        // Resolve overlap by moving this object out of the overlap
        transform.position += (Vector3)(overlap * direction.normalized);
    }



}
