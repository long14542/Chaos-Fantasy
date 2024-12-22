using UnityEngine;

public class CrystalHammerProjectile : Projectile
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Di chuyển hitbox ra ngoài một chút để tránh trùng vị trí nhân vật
        transform.position += direction * 0.5f; // Điều chỉnh khoảng cách này nếu cần
    }

    public void SetRange(float rangeMultiplier)
    {
        transform.localScale *= rangeMultiplier; // Điều chỉnh kích thước hitbox
    }
}
