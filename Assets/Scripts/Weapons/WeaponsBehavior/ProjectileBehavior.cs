using UnityEngine;


// Base script for projectile behavior
// This script is placed upon the prefab of a weapon that is a projectile
public class ProjectileBehavior : MonoBehaviour
{
    public WeaponData weaponData;
    protected Vector3 direction;
    CharacterHandler playerStats;

    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;
    protected float currentLifetime;
    protected float currentDamagePlus;

    void Awake()
    {
        currentCooldownDuration = weaponData.cooldownDuration;
        currentDamage = weaponData.damage;
        currentPierce = weaponData.pierce;
        currentSpeed = weaponData.speed;
        currentLifetime = weaponData.lifeTime;
        currentDamagePlus = weaponData.damagePlus;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerStats = FindFirstObjectByType<CharacterHandler>();
        // Destroy the game object this script is attached to after a lifeTime amount of time
        Destroy(gameObject, weaponData.lifeTime);
    }

    // Apply player's might to projectiles' damage
    public float MightAppliedDamaged()
    {
        currentDamage += currentDamagePlus;
        return currentDamage *= playerStats.currentMight;
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

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if there is collision with objects tag Enemy
        if (collision.CompareTag("Enemy"))
        {
            // Reference the enemyHandler from the Collider
            EnemyHandler enemy = collision.GetComponent<EnemyHandler>();
            // Use damage increased with might
            int dmg = (int) MightAppliedDamaged();
            enemy.TakeDamage(dmg);
            DecreasePierce();
        }
    }

    // If pierce is 0 then destroy the weapon object
    void DecreasePierce()
    {
        currentPierce -= 1;

        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseDamage()
    {
        currentDamagePlus += 10;
    }
}
