using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackDamage = 10f; // Sát thương tấn công
    public float attackRange = 0.5f; // Khoảng cách tấn công
    public float attackCooldown = 1.1f; // Thời gian hồi giữa các lần tấn công
    private float lastAttackTime;

    private GameObject player;
    private EnemyHandler enemyHandler;
    private Animator animator;

    void Start()
    {
        // Tìm Player
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found! Ensure Player has the correct tag.");
        }
        else
        {
            Debug.Log("Player found successfully.");
        }

        // Lấy EnemyHandler
        enemyHandler = GetComponent<EnemyHandler>();
        if (enemyHandler == null)
        {
            Debug.LogError("EnemyHandler not found on this object.");
        }
        else
        {
            Debug.Log("EnemyHandler found successfully.");
        }

        // Lấy Animator
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found on Enemy.");
        }
        else
        {
            Debug.Log("Animator found successfully.");
        }
    }

    void Update()
    {
        // Debug trạng thái kẻ địch
        if (enemyHandler == null || enemyHandler.currentHealth <= 0)
        {
            if (enemyHandler == null)
            {
                Debug.Log("EnemyHandler is null. Skipping attack logic.");
            }
            else if (enemyHandler.currentHealth <= 0)
            {
                Debug.Log("Enemy is dead. Skipping attack logic.");
            }
            return;
        }

        if (player == null)
        {
            Debug.Log("Player is null. Skipping attack logic.");
            return;
        }

        // Tính khoảng cách tới Player
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log($"Distance to Player: {distanceToPlayer}");

        // Nếu khoảng cách trong tầm tấn công và cooldown đã sẵn sàng
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Debug.Log("Enemy is within attack range and attack cooldown is ready.");
            Attack();
            lastAttackTime = Time.time; // Cập nhật thời gian tấn công cuối
        }
        else
        {
            if (distanceToPlayer > attackRange)
            {
                Debug.Log("Enemy is out of attack range.");
            }
            if (Time.time < lastAttackTime + attackCooldown)
            {
                Debug.Log("Attack cooldown not ready yet.");
            }
        }

        // Reset trạng thái tấn công
        if (animator != null && animator.GetBool("isAttacking") && Time.time >= lastAttackTime + 0.5f)
        {
            Debug.Log("Resetting attack animation.");
            animator.SetBool("isAttacking", false);
        }
    }

    private void Attack()
    {
        // Gây sát thương cho người chơi
        CharacterHandler playerHandler = player.GetComponent<CharacterHandler>();
        if (playerHandler != null)
        {
            playerHandler.TakeDamage(attackDamage);
            Debug.Log($"Player takes {attackDamage} damage from Enemy.");

            // Kích hoạt animation tấn công
            if (animator != null)
            {
                Debug.Log("Triggering attack animation.");
                animator.SetBool("isAttacking", true);
            }
            else
            {
                Debug.LogError("Animator is null. Cannot trigger attack animation.");
            }
        }
        else
        {
            Debug.LogError("CharacterHandler not found on Player.");
        }
    }

    public void DealDamageToPlayer() // Gắn vào Animation Event
    {
        if (player != null)
        {
            CharacterHandler playerHandler = player.GetComponent<CharacterHandler>();
            if (playerHandler != null)
            {
                playerHandler.TakeDamage(attackDamage);
                Debug.Log("Player takes damage via Animation Event.");
            }
            else
            {
                Debug.LogError("CharacterHandler not found on Player.");
            }
        }
        else
        {
            Debug.LogError("Player is null in DealDamageToPlayer().");
        }
    }
}
