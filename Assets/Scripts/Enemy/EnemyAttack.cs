using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackDamage = 10f; // Sát thương tấn công
    public float attackRange = 0.5f; // Khoảng cách tấn công
    public float attackCooldown = 1f; // Thời gian hồi giữa các lần tấn công
    private float lastAttackTime;

    private GameObject player;
    private EnemyHandler enemyHandler;
    private Animator animator;
    private bool isAttacking;

    void Start()
    {
        // Lấy tham chiếu đến người chơi
        player = GameObject.FindGameObjectWithTag("Player");
        enemyHandler = GetComponent<EnemyHandler>();
        animator = GetComponent<Animator>(); // Gắn Animator từ component

        if (animator == null)
        {
            Debug.LogError("Animator not found on Enemy!");
        }
    }

    void Update()
    {
        // Nếu kẻ địch đã chết hoặc không tìm thấy người chơi, bỏ qua logic tấn công
        if (enemyHandler == null || enemyHandler.currentHealth <= 0 || player == null)
        {
            // Đảm bảo tắt trạng thái "isAttacking" nếu enemy đã chết
            if (animator != null && animator.GetBool("isAttacking"))
            {
                animator.SetBool("isAttacking", false);
            }

            // Nếu kẻ địch chết, set isDead = true để kích hoạt animation chết
            if (enemyHandler.currentHealth <= 0 && animator != null)
            {
                animator.SetBool("isDead", true);
            }

            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // Nếu khoảng cách trong tầm tấn công và thời gian hồi đã hết
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack(); // Thực hiện tấn công
            lastAttackTime = Time.time; // Cập nhật thời gian tấn công cuối
        }
        else
        {
            // Tắt trạng thái tấn công khi không tấn công
            if (animator != null)
            {
                animator.SetBool("isAttacking", false);
            }
        }
    }

    private void Attack()
    {
        // Gây sát thương cho người chơi nếu trong tầm
        CharacterHandler playerHandler = player.GetComponent<CharacterHandler>();
        if (playerHandler != null)
        {
            playerHandler.TakeDamage(attackDamage);
        }

        // Kích hoạt trạng thái "isAttacking" để phát animation tấn công
        if (animator != null)
        {
            animator.SetBool("isAttacking", true);
        }
    }
}
