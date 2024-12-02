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
    private bool isDead = false; // Trạng thái để kiểm soát khi kẻ địch chết

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
        if (isDead)
        {
            CheckDeathAnimation(); // Kiểm tra khi nào hoạt ảnh chết kết thúc
            return;
        }

        foreach (var collider in Physics2D.OverlapCircleAll(transform.position, collide.radius))
        {
            if (collider.gameObject == gameObject) continue;

            if (collider.CompareTag("Enemy")) HandleOverlap(collider, 4f);
            else if (collider.CompareTag("Player")) HandlePlayerCollision(collider);
        }

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public void TakeDamage(int dmg)
    {
        if (isDead) return;

        currentHealth -= dmg;
        movement.Knockback(5f, 0.2f); // Đẩy lùi khi nhận sát thương
        DamagePopUp.Create(transform.position, dmg);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true; // Đánh dấu kẻ địch đã chết
        currentHealth = 0;

        // Dừng di chuyển
        if (movement != null) movement.enabled = false;

        // Phát hoạt ảnh chết
        if (animator != null)
        {
            animator.SetBool("isDead", true);
        }

        // Rớt đồ
        if (drop != null) drop.DropPickUp();

        // Vô hiệu hóa collider
        if (collide != null) collide.enabled = false;
    }

    private void CheckDeathAnimation()
    {
        if (animator != null)
        {
            // Kiểm tra nếu hoạt ảnh chết đã hoàn tất
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Die") && stateInfo.normalizedTime >= 1f) // Hoạt ảnh "Die" kết thúc
            {
                ReturnToPool();
            }
        }
    }

    private void ReturnToPool()
    {
        ObjectPools.EnqueueObject(this, enemyData.name);
        spawner.enemiesAlive--;

        ResetEnemy();
    }

    private void ResetEnemy()
    {
        isDead = false;
        currentHealth = enemyData.MaxHealth;

        // Kích hoạt lại collider
        if (collide != null) collide.enabled = true;

        // Kích hoạt lại di chuyển
        if (movement != null) movement.enabled = true;

        // Đặt trạng thái animator về mặc định
        if (animator != null) animator.SetBool("isDead", false);

        gameObject.SetActive(false); // Tắt game object
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

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}