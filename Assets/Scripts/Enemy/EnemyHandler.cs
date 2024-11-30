using System.Collections.Generic;
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
        
        // Track processed pairs to avoid redundant checks
        // HashSet is used instead of List because it avoid duplicates
        // Which is good because we don't want to add the same pair
        HashSet<(Collider2D, Collider2D)> processedPairs = new HashSet<(Collider2D, Collider2D)>();

        foreach (Collider2D obj1 in nearbyObjects)
        {
            foreach (Collider2D obj2 in nearbyObjects)
            {
                // Skip self-collision and already processed pairs
                if (obj1 == obj2 || processedPairs.Contains((obj1, obj2)) || processedPairs.Contains((obj2, obj1)))
                    continue;

                // Add the pair to the processed set
                processedPairs.Add((obj1, obj2));

                // Resolve overlap between obj1 and obj2
                HandleOverlap(obj1, obj2);
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

    void HandleOverlap(Collider2D obj1, Collider2D obj2)
    {
        Vector2 direction = (Vector2)(obj1.transform.position - obj2.transform.position);
        float distance = direction.magnitude;

        float combinedRadius = obj1.bounds.extents.x + obj2.bounds.extents.x; // Assuming circular objects

        // Calculate the overlap amount
        float overlap = combinedRadius - distance;

        if (overlap > 0) // Resolve overlap if necessary
        {
            // Split the resolution equally between the two objects
            Vector3 halfOverlap = (overlap / 2) * direction.normalized;

            obj1.transform.position += halfOverlap;
            obj2.transform.position -= halfOverlap;
        }
    }



}
