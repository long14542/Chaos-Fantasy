using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Vector2 moveDir;
    [HideInInspector] public Vector2 ShootDir;
    [HideInInspector] public Vector2 lastHorizontal;
    private Rigidbody2D body;
    private CharacterHandler player;
    void Start()
    {
        player = GetComponent<CharacterHandler>();
        body = GetComponent<Rigidbody2D>();
        ShootDir = new Vector2(1, 0);
    }
    void Update()
    {
        InputManagement();
    }
    void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY).normalized;
        if (moveDir.x != 0)
        {
            lastHorizontal.x = moveDir.x;
        }
        if (moveDir.x != 0 || moveDir.y != 0)
        {
            ShootDir = moveDir;
        }
    }
    void Move()
    {
        body.velocity = new Vector2(moveDir.x * player.currentMoveSpeed, moveDir.y * player.currentMoveSpeed);
    }
}