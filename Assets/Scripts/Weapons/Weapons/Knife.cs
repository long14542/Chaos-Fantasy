using UnityEngine;

public class Knife : Weapon
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();

        switch (currentLevel)
        {
            case 1: // Level 1: Ném 1 dao
                SpawnKnife(pm.ShootDir);
                break;

            case 2: // Level 2: Ném 2 dao theo góc chéo
                SpawnKnife(pm.ShootDir);
                SpawnKnife(Quaternion.Euler(0, 0, 30) * pm.ShootDir); // Dao lệch 30 độ
                SpawnKnife(Quaternion.Euler(0, 0, -30) * pm.ShootDir); // Dao lệch -30 độ
                break;

            case 3: // Level 3: Ném 4 dao 4 hướng
                SpawnKnife(Vector2.up);
                SpawnKnife(Vector2.down);
                SpawnKnife(Vector2.left);
                SpawnKnife(Vector2.right);
                break;
        }
    }
    private void SpawnKnife(Vector2 direction)
    {
        GameObject knife = Instantiate(weaponData.prefab);
        knife.transform.position = this.transform.position;

        // Truyền hướng bắn cho projectile
        KnifeProjectile projectile = knife.GetComponent<KnifeProjectile>();
        if (projectile != null)
        {
            projectile.CheckDirection(direction);
        }
    }


    public override bool LevelUp()
    {
        if (!base.LevelUp()) return false;
        return true;
    }
}
