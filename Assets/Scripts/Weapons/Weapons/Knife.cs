using System.Collections;
using UnityEngine;

public class Knife : Weapon
{
    public float doubleShotDelay = 0.005f; // Khoảng thời gian delay giữa 2 lần bắn

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();

        if (currentLevel < 3)
        {
            // Level 1 và 2: chỉ bắn 1 lần
            SpawnKnives();
        }
        else
        {
            // Level 3 trở lên: bắn 2 lần liên tiếp
            StartCoroutine(DoubleShot());
        }
    }

    private IEnumerator DoubleShot()
    {
        // Lần bắn đầu tiên
        SpawnKnives();

        // Chờ delay giữa 2 lần bắn
        yield return new WaitForSeconds(doubleShotDelay);

        // Lần bắn thứ hai
        SpawnKnives();
    }

    private void SpawnKnives()
    {
        switch (currentLevel)
        {
            case 1: // Level 1: Ném 1 dao
                SpawnKnife(pm.ShootDir);
                break;

            case 2: // Level 2: Ném 2 dao theo góc chéo
                SpawnKnife(pm.ShootDir);
                SpawnKnife(-pm.ShootDir);
                break;

            case 3: // Level 3: Ném 3 dao theo góc
                SpawnKnife(pm.ShootDir);
                SpawnKnife(Quaternion.Euler(0, 0, 30) * pm.ShootDir); // Dao lệch 30 độ
                SpawnKnife(Quaternion.Euler(0, 0, -30) * pm.ShootDir); // Dao lệch -30 độ
                break;
            case 4:
                SpawnKnife(pm.ShootDir);
                SpawnKnife(-pm.ShootDir);
                SpawnKnife(Quaternion.Euler(0, 0, 30) * pm.ShootDir); // Dao lệch 30 độ
                SpawnKnife(Quaternion.Euler(0, 0, -30) * pm.ShootDir); // Dao lệch -30 độ
                break;
            case 5: // Level 4: Ném 4 dao 4 hướng
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
