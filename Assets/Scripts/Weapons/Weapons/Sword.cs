using UnityEngine;
using System.Collections.Generic;

public class Sword : Weapon
{
    private Transform swordTransform;
    private Quaternion originalRotation;
    private float attackAngle = -25f;
    private bool isAttacking = false;
    private CharacterHandler characterHandler;
    private PolygonCollider2D swordCollider;
    private new WeaponData weaponData;

    protected override void Start()
    {
        base.Start(); // Gọi phương thức Start của lớp Weapon

        // Tạo object Sword từ prefab của weaponData và gán nó cho player
        GameObject swordObject = Instantiate(weaponData.prefab, owner.transform);
        swordTransform = swordObject.transform;
        originalRotation = swordTransform.rotation; // Lưu lại góc xoay ban đầu

        // Tìm và liên kết với CharacterHandler
        characterHandler = owner.GetComponent<CharacterHandler>();

        // Thiết lập PolygonCollider2D
        swordCollider = swordObject.GetComponent<PolygonCollider2D>();
        if (swordCollider == null)
        {
            Debug.LogError("Sword object không có PolygonCollider2D.");
        }
        else
        {
            swordCollider.isTrigger = true; // Đảm bảo collider hoạt động như một trigger
        }
    }

    protected override void Update()
    {
        base.Update(); // Gọi Update từ lớp Weapon để xử lý cooldown
        FollowPlayer(); // Di chuyển theo người chơi
    }

    private void FollowPlayer()
    {
        // Đặt vị trí của thanh kiếm theo người chơi
        swordTransform.position = owner.transform.position;
    }

    protected override void Attack()
    {
        if (!isAttacking)
        {
            base.Attack(); // Gọi Attack từ lớp cha để reset cooldown
            StartCoroutine(PerformAttack()); // Gọi coroutine để thực hiện tấn công
        }
    }

    private System.Collections.IEnumerator PerformAttack()
    {
        isAttacking = true;

        // Xoay kiếm đến góc tấn công
        swordTransform.rotation = Quaternion.Euler(0, 0, attackAngle);

        // Giữ nguyên vị trí tấn công trong một khoảng thời gian ngắn
        yield return new WaitForSeconds(0.2f);

        // Trả kiếm về vị trí xoay ban đầu
        swordTransform.rotation = originalRotation;
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking)
        {
            EnemyHandler enemy = collision.GetComponent<EnemyHandler>();
            if (enemy != null)
            {
                enemy.TakeDamage((int)weaponData.damage); // Gây sát thương đến kẻ thù
                Debug.Log($"Sword attack! Causing {weaponData.damage} damage to {enemy.enemyName}.");
            }
        }
    }
}
