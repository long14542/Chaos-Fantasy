using UnityEngine;

public class CrystalHammer : Weapon
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();

        float rangeMultiplier = 1f;
        switch (currentLevel)
        {
            case 1:
                rangeMultiplier = 1f;
                SpawnImpact(rangeMultiplier, pm.ShootDir); // Đánh theo hướng bắn
                break;
            case 2:
                rangeMultiplier = 1.3f;
                SpawnImpact(rangeMultiplier, pm.ShootDir);
                break;
            case 3:
                rangeMultiplier = 1.3f;
                SpawnImpact(rangeMultiplier, pm.ShootDir);
                SpawnImpact(rangeMultiplier, -pm.ShootDir); // Đánh 2 hướng
                break;
            case 4:
                rangeMultiplier = 1.3f;
                SpawnImpact(rangeMultiplier, Vector2.up);
                SpawnImpact(rangeMultiplier, Vector2.down);
                SpawnImpact(rangeMultiplier, Vector2.left);
                SpawnImpact(rangeMultiplier, Vector2.right);
                break;
            case 5:
                rangeMultiplier = 1.6f;
                SpawnImpact(rangeMultiplier, Vector2.up);
                SpawnImpact(rangeMultiplier, Vector2.down);
                SpawnImpact(rangeMultiplier, Vector2.left);
                SpawnImpact(rangeMultiplier, Vector2.right);
                break;
        }
    }

    private void SpawnImpact(float rangeMultiplier, Vector2 direction)
    {
        GameObject impact = Instantiate(weaponData.prefab);
        CrystalHammerProjectile projectile = impact.GetComponent<CrystalHammerProjectile>();

        if (projectile != null)
        {
            projectile.CheckDirection(direction);
            projectile.SetRange(rangeMultiplier);
        }

        // Di chuyển hitbox ra ngoài nhân vật theo hướng đánh
        impact.transform.position = this.transform.position + (Vector3)direction * 1f; // Điều chỉnh khoảng cách
    }

    public override bool LevelUp()
    {
        if (!base.LevelUp()) return false;
        return true;
    }
}
