using UnityEngine;

public class EnemyMovement : MonoBehaviour, IKnockback
{
    private EnemyHandler enemy;
    GameObject player;

    Vector3 knockbackVelocity;
    float knockbackDuration;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<EnemyHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (knockbackDuration > 0) // if enemy is getting knocked back
        {
            transform.position += knockbackVelocity * Time.deltaTime;
            knockbackDuration -= Time.deltaTime;
        }
        else
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.GetCurrentSpeed() * Time.deltaTime);

            // Xoay theo hướng trục X khi di chuyển
            if (direction.x > 0) // Di chuyển sang phải
            {
                transform.localScale = new Vector3(1, 1, 1); // Xoay mặt đối tượng sang phải
            }
            else if (direction.x < 0) // Di chuyển sang trái
            {
                transform.localScale = new Vector3(-1, 1, 1); // Xoay mặt đối tượng sang trái
            }
        }
    }

    public void Knockback(float force, float duration)
    {
        // Get the direction of knockback
        Vector3 direction = transform.position - player.transform.position;

        knockbackVelocity = direction.normalized * force;
        knockbackDuration = duration;
    }
}
