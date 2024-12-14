using UnityEngine;

public class Sword : Weapon
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        // Spawn slashes based on current level
        switch (currentLevel)
        {
            case 1:
                SpawnSlash(pm.ShootDir); // Default single direction
                break;
            case 2:
                // Spawn in both front and back
                SpawnSlash(pm.ShootDir);
                SpawnSlash(-pm.ShootDir);
                break;
            case 3:
                // Spawn in four directions: up, down, left, right
                SpawnSlash(Vector2.up);
                SpawnSlash(Vector2.down);
                SpawnSlash(Vector2.left);
                SpawnSlash(Vector2.right);
                break;
            default:
                Debug.LogWarning("Unhandled level in Sword.Attack");
                break;
        }
    }

    private void SpawnSlash(Vector2 direction)
    {
        direction.Normalize();
        GameObject slash = Instantiate(weaponData.prefab); // Tạo slash
        SwordProjectile projectile = slash.GetComponent<SwordProjectile>();

        if (projectile != null)
        {
            projectile.CheckDirection(direction); // Thiết lập hướng chém
        }
        slash.transform.position = this.transform.position;
    }


    public override bool LevelUp()
    {
        if (!base.LevelUp()) return false;
        return true;
    }
}
