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
    private Animator animator; // Thêm Animator vào biến
    private bool isDead;

    // Use Awake because it is called before Start
    void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        spawner = FindFirstObjectByType<EnemySpawner>();
        drop = GetComponent<DropRateManager>();
        collide = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>(); // Thêm tham chiếu tới Animator

        currentDamage = enemyData.Damage;
        currentSpeed = enemyData.Speed;
        currentHealth = enemyData.MaxHealth;
    }

    void Update()
    {
        // Kiểm tra các va chạm gần
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, collide.radius);

        foreach (Collider2D collider in nearbyObjects)
        {
            // Bỏ qua chính đối tượng
            if (collider.gameObject == this.gameObject) continue;

            // Kiểm tra nếu là kẻ địch
            if (collider.CompareTag("Enemy"))
            {
                HandleOverlap(collider, 4f);
            }
            else if (collider.CompareTag("Player"))
            {
                HandleOverlap(collider, 10f);
                CharacterHandler player = collider.GetComponent<CharacterHandler>();
                player.TakeDamage(currentDamage); // Gây sát thương cho người chơi nếu gần
            }
        }

        // Kiểm tra nếu kẻ địch chết
        if (currentHealth <= 0)
        {
            // Kích hoạt animation chết
            if (animator != null)
            {
                animator.SetBool("isDead", true);
            }

            // Các hành động khác khi kẻ địch chết
            drop.DropPickUp();
            ObjectPools.EnqueueObject(this, enemyName);
            spawner.enemiesAlive -= 1;
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        movement.Knockback(5f, 0.2f);

        DamagePopUp.Create(transform.position, dmg);

        // Kiểm tra nếu máu <= 0
        if (currentHealth <= 0)
        {
            // Set isDead để kích hoạt animation chết
            if (animator != null)
            {
                animator.SetBool("isDead", true);
            }
        }
    }

    void HandleOverlap(Collider2D other, float force)
    {
        Vector2 direction = (Vector2)(transform.position - other.transform.position);
        transform.position += (Vector3)(force * Time.deltaTime * direction.normalized);
    }
}
