using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator am;
    PlayerMovement pm;
    SpriteRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is moving then set the Move parameter in Animator to true else set to false
        if (pm.moveDir.x != 0 || pm.moveDir.y != 0)
        {
            am.SetBool("Move", true);
            SetSpriteDirection();
        }
        else
        {
            am.SetBool("Move", false);
            SetSpriteDirection();
        }
    }

    void SetSpriteDirection()
    {
        // If the player last horizontal movement was left then flip the animation
        if (pm.lastHorizontal.x < 0)
        {
            render.flipX = true;
        }
        else
        {
            render.flipX = false;
        }
    }
}
