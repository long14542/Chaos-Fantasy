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
            isHit = true; // Đánh dấu trạng thái đã va chạm
            ani.SetBool("isHit", true); // Cập nhật giá trị cho Animator để chuyển animation
            currentSpeed = 0; // Dừng chuyển động của fireball
        }
    }
}
