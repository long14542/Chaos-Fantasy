using UnityEngine;

public class Staff : Weapon
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Attack()
    {
        base.Attack();

        // Bắn quả cầu lửa dựa trên level
        switch (currentLevel)
        {
            case 1:
                SpawnFireball(pm.ShootDir, 5f); // Level 1: 1 quả bình thường
                break;

            case 2:
                SpawnFireball(pm.ShootDir, 10f); // Level 2: 1 quả lớn hơn
                SpawnFireball(-pm.ShootDir, 5f); // Thêm quả lệch 15 độ
                break;

            case 3:
                SpawnFireball(pm.ShootDir, 15f); // Level 3: Quả to hơn nữa
                SpawnFireball(Quaternion.Euler(0, 0, 20) * pm.ShootDir, 5f); // Thêm quả lệch 20 độ
                SpawnFireball(Quaternion.Euler(0, 0, -20) * pm.ShootDir, 5f); // Thêm quả lệch -20 độ
                break;

            case 4:
                SpawnFireball(pm.ShootDir, 25f); // Level 4: Quả lớn nhất
                SpawnFireball(Quaternion.Euler(0, 0, 30) * pm.ShootDir, 2f); // Thêm quả lệch 30 độ
                SpawnFireball(Quaternion.Euler(0, 0, -30) * pm.ShootDir, 2f); // Thêm quả lệch -30 độ
                SpawnFireball(Vector2.up, 2f); // Thêm quả lên trên
                SpawnFireball(Vector2.down, 2f); // Thêm quả xuống dưới
                break;
        }
    }

    // Hàm tạo quả cầu lửa
    private void SpawnFireball(Vector2 direction, float size)
    {
        GameObject fireBall = Instantiate(weaponData.prefab);
        fireBall.transform.position = this.transform.position;

        // Thay đổi kích thước quả cầu lửa
        fireBall.transform.localScale = Vector3.one * size;

        // Truyền hướng bắn cho FireballProjectile
        FireballProjectile projectile = fireBall.GetComponent<FireballProjectile>();
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
