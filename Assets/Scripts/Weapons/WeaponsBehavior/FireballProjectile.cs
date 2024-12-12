using System;
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

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu va chạm với quái (Enemy)
        if (collision.CompareTag("Enemy"))
        {
            EnemyHandler enemy = collision.GetComponent<EnemyHandler>();
            // Use damage increased with might
            currentDamage = weaponData.damage; // DO NOT EVER DELETE this, it will break the damage number ;v Idk why 
            int dmg = (int)MightAppliedDamaged();
            enemy.TakeDamage(dmg);         
            DecreasePierce();
        }
    }

    protected override void DecreasePierce()
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
        ani.SetBool("isHit", true); // Cập nhật giá trị cho Animator để chuyển animation
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
