using UnityEngine;

public class Pickup : MonoBehaviour
{

    protected CharacterHandler target;
    protected float speed;

    [Header("Bonues")]
    public int expGranted;
    
    public virtual bool Collect(CharacterHandler target, float speed)
    {
        if (!this.target)
        {
            this.target = target;
            this.speed = speed;
            return true;
        }
        return false;
    }

    protected virtual void Update()
    {
        // If there is target for the pick-up to move toward
        if (target)
        {
            // Move toward that target
            Vector2 distance = target.transform.position - transform.position;
            // Check the distance between the pick-up and the target, destroy pick-up if they touch each other
            // Using square instead of square root because it is faster
            // Delta time is accounting for the lag because we are running this every frame
            if (distance.sqrMagnitude > speed * speed * Time.deltaTime)
            {
                transform.position += speed * Time.deltaTime * (Vector3)distance.normalized;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    // Now this will be used for subsequent types of pick-ups like exp, health
    protected virtual void OnDestroy()
    {
        if (!target) return;
        if (expGranted != 0) target.IncreaseExp(expGranted);
    }

}
