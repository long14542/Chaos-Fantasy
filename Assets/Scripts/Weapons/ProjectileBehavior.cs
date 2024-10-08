using UnityEngine;


// Base script for projectile behavior
// This script is placed upon the prefab of a weapon that is a projectile
public class ProjectileBehavior : MonoBehaviour
{
    protected Vector3 direction;
    public float lifeTime;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Destroy the game object this script is attached to after a lifeTime amount of time
        Destroy(gameObject, lifeTime);
    }

    public void CheckDirection(Vector3 dir)
    {
        direction = dir;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        // If the direction if left then change the scale accordingly
        if (direction.x < 0 && direction.y == 0)
        {
            scale.x *= -1;
        }
        // Up
        else if (direction.x == 0 && direction.y > 0)
        {
            rotation.z = 90;
        }
        // Down
        else if (direction.x == 0 && direction.y < 0)
        {
            rotation.z = -90;
        }
        // Right up
        else if (direction.x > 0 && direction.y > 0)
        {
            rotation.z = 45;
        }
        // Right down
        else if (direction.x > 0 && direction.y < 0)
        {
            rotation.z = -45;
        }
        // Left down
        else if (direction.x < 0 && direction.y < 0)
        {
            scale.x *= -1;
            rotation.z = 45;
        }
        else if (direction.x < 0 && direction.y > 0)
        {
            scale *= -1;
            rotation.z = -45;
        }

        // Set the scale and rotation of the object
        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
