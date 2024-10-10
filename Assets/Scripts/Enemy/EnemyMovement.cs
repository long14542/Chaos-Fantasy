using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyHandler enemy;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<EnemyHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentSpeed * Time.deltaTime);
    }
}
