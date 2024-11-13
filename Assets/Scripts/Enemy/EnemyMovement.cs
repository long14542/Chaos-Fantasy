using UnityEngine;

public class EnemyMovement : MonoBehaviour
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
        if (knockbackDuration > 0) // if is getting knockbacked
        {
            transform.position += knockbackVelocity * Time.deltaTime;
            knockbackDuration -= Time.deltaTime;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentSpeed * Time.deltaTime);
        }
    }

    public void Knockback(float force, float duration)
    {
        // Get the direction
        Vector3 direction = transform.position - player.transform.position;

        knockbackVelocity = direction.normalized * force;
        knockbackDuration = duration;
    }
}
