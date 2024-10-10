using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public Vector2 ShootDir;
    [HideInInspector]
    public Vector2 lastHorizontal;

    // Character stats
    private Rigidbody2D body;
    private CharacterHandler player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterHandler>();
        body = GetComponent<Rigidbody2D>();
        // Set the default facing: right
        ShootDir = new Vector2(1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    // FixedUpdate is good for physics calculation since it is updated independently from frame rate
    void FixedUpdate()
    {
        Move();
    }

    // Player's Input System
    void InputManagement()
    {
        // Set X to be Horizontal movement and Y to be Vertical movement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Get ONLY direction of movement by normalizing the Vector's length to 1
        moveDir = new Vector2(moveX, moveY).normalized;

        // Get the last horizontal movement
        if (moveDir.x != 0)
        {
            lastHorizontal.x = moveDir.x;
        }

        // Set the shooting direction to be moving direction if move
        if (moveDir.x != 0 || moveDir.y != 0)
        {
            ShootDir = moveDir;
        }


    }

    void Move()
    {
        // Get the direction then multiply it with moveSpeed to get the velocity
        body.velocity = new Vector2(moveDir.x * player.currentMoveSpeed, moveDir.y * player.currentMoveSpeed);
    }
}
