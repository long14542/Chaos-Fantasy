using UnityEngine;
using System.Collections;

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

        switch (currentLevel)
        {
            case 1:
                StartCoroutine(SpawnImpactsInLine(1.6f, pm.ShootDir,10)); // Sử dụng hướng người chơi
                break;
            case 2:
                SpawnImpact(1.3f, pm.ShootDir); // Sử dụng hướng người chơi
                break;
            case 3:
                StartCoroutine(SpawnImpactsInLine(1.3f, pm.ShootDir)); // Sử dụng hướng người chơi
                break;
            case 4:
                StartCoroutine(SpawnImpactsInLine(1.3f, pm.ShootDir)); // Sử dụng hướng người chơi
                break;
            case 5:
                StartCoroutine(SpawnImpactsInLine(1.6f, pm.ShootDir, 5)); // Tăng số lượng đòn đánh lên 5 cho cấp 5
                break;
        }
    }

    private void SpawnImpact(float size, Vector2 direction)
    {
        GameObject impact = Instantiate(weaponData.prefab);
        CrystalHammerProjectile projectile = impact.GetComponent<CrystalHammerProjectile>();
        impact.transform.localScale = Vector3.one * size;
        if (projectile != null)
        {
            projectile.CheckDirection(direction);
        }

        // Di chuyển hitbox ra ngoài nhân vật theo hướng đánh
        impact.transform.position = this.transform.position + (Vector3)direction * 0.5f; // Điều chỉnh khoảng cách
    }

    // Coroutine để spawn các đòn đánh theo đường thẳng, dựa trên hướng người chơi
    // Đã thêm tham số `numImpacts` để chỉ định số lượng đòn đánh
    private IEnumerator SpawnImpactsInLine(float size, Vector2 direction, int numImpacts = 4)
    {
        float distanceBetweenAttacks = 1f; // Khoảng cách giữa các đòn tấn công
        Transform previousImpactTransform = null;

        for (int i = 0; i < numImpacts; i++) // Tạo số lượng đòn đánh tùy thuộc vào `numImpacts`
        {
            GameObject impact = Instantiate(weaponData.prefab);
            CrystalHammerProjectile projectile = impact.GetComponent<CrystalHammerProjectile>();
            impact.transform.localScale = Vector3.one * size;

            if (projectile != null)
            {
                projectile.CheckDirection(direction);
            }

            if (i == 0)
            {
                // Đòn đầu tiên xuất phát từ vị trí của nhân vật
                impact.transform.position = this.transform.position + (Vector3)direction * 0.5f;
            }
            else if (previousImpactTransform != null)
            {
                // Đòn tiếp theo dựa trên vị trí của đòn trước đó
                impact.transform.position = previousImpactTransform.position + (Vector3)(direction.normalized * distanceBetweenAttacks);
            }

            previousImpactTransform = impact.transform;

            // Chờ một khoảng thời gian trước khi tạo đòn tiếp theo
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override bool LevelUp()
    {
        if (!base.LevelUp()) return false;
        return true;
    }
}
