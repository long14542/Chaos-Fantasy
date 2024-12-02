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
        

        foreach (var collider in Physics2D.OverlapCircleAll(transform.position, collide.radius))
        {
            if (collider.gameObject == gameObject) continue;

            if (collider.CompareTag("Enemy")) HandleOverlap(collider, 4f);
            else if (collider.CompareTag("Player")) HandlePlayerCollision(collider);
        }

        if (currentHealth <= 0 && !isDead)
        {
            Die();
            InitializeFromPool();
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
        isDead = true;
        currentHealth = 0;

        if (movement != null) movement.enabled = false;
        if (drop != null) drop.DropPickUp();
        if (collide != null) collide.enabled = false;

        ObjectPools.EnqueueObject(this, enemyData.name); // Đưa vào pool ngay lập tức
        spawner.enemiesAlive--; // Giảm số lượng kẻ địch còn sống
    }

<<<<<<< HEAD
    private void CheckDeathAnimation()
    {
        if (animator != null)
        {
            // Kiểm tra nếu hoạt ảnh chết đã hoàn tất
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Die") && stateInfo.normalizedTime >= 1f) // Hoạt ảnh "Die" kết thúc
            {
                GetComponent<SpriteRenderer>().enabled = false;
                ReturnToPool();
            }
        }
    }
=======
>>>>>>> 0e0846e64877a9d54ceee72c89f2678a460dffb6



    private void ResetEnemy()
    {
        isDead = false;
        currentHealth = enemyData.MaxHealth;

        if (collide != null) collide.enabled = true;
        if (movement != null) movement.enabled = true;
        if (animator != null) animator.SetBool("isDead", false);
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

    public void InitializeFromPool()
    {
        ResetEnemy();
        gameObject.SetActive(true);
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
