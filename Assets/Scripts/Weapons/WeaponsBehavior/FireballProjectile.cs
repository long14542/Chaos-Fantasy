using UnityEngine;

public class FireballProjectile : Projectile
{
    private bool isHit;
    private Animator ani;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ani = GetComponent<Animator>();
        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeathAnimation();
        // Fireball chỉ di chuyển nếu chưa va chạm
        if (!isHit)
        {
            transform.position += currentSpeed * Time.deltaTime * direction;
        }
    }

    public override void DecreasePierce()
    {
        currentPierce -= 1;

        if (currentPierce <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isHit = true; // Đánh dấu trạng thái đã va chạm
        ani.SetBool("isHit", true); // Chuyển animation chết

        // Gây sát thương lan (AOE)
        DealAreaDamage();
    }

    void DealAreaDamage()
    {
        float baseRadius = 2.5f; // Bán kính cơ bản
        float explosionRadius = baseRadius * transform.localScale.x; // Tỷ lệ bán kính với kích thước quả cầu lửa

        int baseDamage = (int)MightAppliedDamaged(); // Sát thương cơ bản

        // Tìm tất cả các kẻ địch trong phạm vi AOE
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, LayerMask.GetMask("Enemy"));

        foreach (var enemyCollider in hitEnemies)
        {
            EnemyHandler enemy = enemyCollider.GetComponent<EnemyHandler>();
            if (enemy != null)
            {
                enemy.TakeDamage(baseDamage); // Gây sát thương
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // Vẽ bán kính sát thương tỷ lệ với kích thước quả cầu
        float baseRadius = 2.5f;
        float explosionRadius = baseRadius * transform.localScale.x; // Tỷ lệ bán kính với kích thước
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }


    void CheckDeathAnimation()
    {
        // Kiểm tra nếu hoạt ảnh chết đã hoàn tất
        AnimatorStateInfo stateInfo = ani.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("fireballhit") && !ani.IsInTransition(0))
        {
            Destroy(gameObject);
        }
    }
}
