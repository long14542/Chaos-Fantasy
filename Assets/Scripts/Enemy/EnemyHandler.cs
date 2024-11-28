using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    private EnemyMovement movement;
    private EnemySpawner spawner;
    private DropRateManager drop;
    private Animator animator;

    private float currentSpeed, currentDamage, currentHealth;
    private CircleCollider2D collide;

    void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        spawner = FindObjectOfType<EnemySpawner>();
        drop = GetComponent<DropRateManager>();
        collide = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();

        currentDamage = enemyData.Damage;
        currentSpeed = enemyData.Speed;
        currentHealth = enemyData.MaxHealth;
    }

    void Update()
    {
        foreach (var collider in Physics2D.OverlapCircleAll(transform.position, collide.radius))
        {
            if (collider.gameObject == gameObject) continue;

            if (collider.CompareTag("Enemy")) HandleOverlap(collider, 4f);
            else if (collider.CompareTag("Player")) HandlePlayerCollision(collider);
        }

        if (currentHealth <= 0 && !animator.GetBool("isDead"))
        {
            animator.SetBool("isDead", true);
            drop.DropPickUp();
            ObjectPools.EnqueueObject(this, enemyData.name);
            spawner.enemiesAlive--;
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        movement.Knockback(5f, 0.2f);
        DamagePopUp.Create(transform.position, dmg);

        if (currentHealth <= 0) animator.SetBool("isDead", true);
    }

    private void HandleOverlap(Collider2D other, float force)
    {
        Vector2 direction = (Vector2)(transform.position - other.transform.position);
        transform.position += (Vector3)(force * Time.deltaTime * direction.normalized);
    }

    private void HandlePlayerCollision(Collider2D collider)
    {
        collider.GetComponent<CharacterHandler>().TakeDamage(currentDamage);
        HandleOverlap(collider, 10f);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    // Getter for currentSpeed
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
