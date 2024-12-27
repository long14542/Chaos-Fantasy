using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    public int baseKnifeCount = 2;        // Số dao cơ bản
    public float rotationSpeed = 150f;   // Tốc độ quay mặc định
    public float orbitRadius = 2f;     // Bán kính quay mặc định
    public float defaultSizeFactor = 1f; // Hệ số kích thước mặc định
    public float largeSizeFactor = 1.5f;  // Hệ số kích thước lớn hơn

    private static List<GameObject> activeKnives = new List<GameObject>(); // Danh sách dao hiện tại

    protected override void Start()
    {
        base.Start();
        //UpdateKnives();
    }

    protected override void Attack()
    {
        // Logic tấn công nếu cần
        cooldown = currentCooldownDuration + weaponData.lifeTime;
        foreach (var knife in activeKnives)
        {
            Destroy(knife); // Hủy đối tượng dao
        }
        activeKnives.Clear(); // Dọn danh sách dao
        
        int knifeCount = baseKnifeCount;
        float angleStep;
        float sizeFactor = defaultSizeFactor; // Hệ số kích thước mặc định

        switch (currentLevel)
        {
            case 1: // Level 1: 2 dao, kích thước mặc định
                knifeCount = 2;
                orbitRadius = 1.5f;
                rotationSpeed = 100f;
                sizeFactor = defaultSizeFactor;
                break;

            case 2: // Level 2: 2 dao, quay nhanh hơn kích thước mặc định
                knifeCount = 2;
                orbitRadius = 1.5f;
                rotationSpeed = 130f;
                sizeFactor = defaultSizeFactor;
                break;

            case 3: // Level 3: 3 dao, kích thước lớn hơn
                knifeCount = 3;
                orbitRadius = 1.5f;
                rotationSpeed = 130f; // Tăng tốc độ quay
                sizeFactor = largeSizeFactor;
                break;

            case 4: // Level 4: 3 dao, kích thước mặc định
                knifeCount = 3;
                orbitRadius = 1.5f;
                rotationSpeed = 150f;
                sizeFactor = largeSizeFactor;
                break;

            case 5: // Level 5: 4 dao, kích thước lớn hơn
                knifeCount = 4;
                orbitRadius = 1.5f;
                rotationSpeed = 200f; // Tăng tốc độ quay
                sizeFactor = largeSizeFactor;
                break;

            default:
                break;
        }

        // Tính góc chia đều dao
        angleStep = 360f / knifeCount;

        // Tạo dao
        SpawnKnives(knifeCount, angleStep, sizeFactor);
    }

    private void UpdateKnives()
    {
        // Xóa tất cả dao hiện tại
        foreach (var knife in activeKnives)
        {
            Destroy(knife); // Hủy đối tượng dao
        }
        activeKnives.Clear(); // Dọn danh sách dao

        // Thay đổi thuộc tính dao dựa trên cấp độ
        int knifeCount = baseKnifeCount;
        float angleStep;
        float sizeFactor = defaultSizeFactor; // Hệ số kích thước mặc định

        switch (currentLevel)
        {
            case 1: // Level 1: 2 dao, kích thước mặc định
                knifeCount = 2;
                orbitRadius = 1.5f;
                rotationSpeed = 100f;
                sizeFactor = defaultSizeFactor;
                break;

            case 2: // Level 2: 2 dao, quay nhanh hơn kích thước mặc định
                knifeCount = 2;
                orbitRadius = 1.5f;
                rotationSpeed = 130f;
                sizeFactor = defaultSizeFactor;
                break;

            case 3: // Level 3: 3 dao, kích thước lớn hơn
                knifeCount = 3;
                orbitRadius = 1.5f;
                rotationSpeed = 130f; // Tăng tốc độ quay
                sizeFactor = largeSizeFactor;
                break;

            case 4: // Level 4: 3 dao, kích thước mặc định
                knifeCount = 3;
                orbitRadius = 1.5f;
                rotationSpeed = 150f;
                sizeFactor = largeSizeFactor;
                break;

            case 5: // Level 5: 4 dao, kích thước lớn hơn
                knifeCount = 4;
                orbitRadius = 1.5f;
                rotationSpeed = 200f; // Tăng tốc độ quay
                sizeFactor = largeSizeFactor;
                break;

            default:
                break;
        }

        // Tính góc chia đều dao
        angleStep = 360f / knifeCount;

        // Tạo dao
        SpawnKnives(knifeCount, angleStep, sizeFactor);
    }

    private void SpawnKnives(int knifeCount, float angleStep, float sizeFactor)
    {
        for (int i = 0; i < knifeCount; i++)
        {
            float angle = i * angleStep;
            Vector3 spawnPosition = pm.transform.position +
                                    new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * orbitRadius;
            GameObject knife = Instantiate(weaponData.prefab, spawnPosition, Quaternion.Euler(0, angle, 0));

            // Đặt kích thước dao bằng cách nhân Vector3.one với hệ số kích thước
            knife.transform.localScale = Vector3.one * sizeFactor;

            KnifeProjectile projectile = knife.GetComponent<KnifeProjectile>();
            if (projectile != null)
            {
                projectile.SetOrbit(pm.transform, angle, orbitRadius, rotationSpeed);
            }

            activeKnives.Add(knife); // Thêm dao vào danh sách quản lý
        }
    }

    public override bool LevelUp()
    {
        if (!base.LevelUp()) return false;

        // Cập nhật dao theo cấp độ
        //UpdateKnives();
        Attack();
        return true;
    }
}
