using UnityEngine;

public class SwordProjectile : Projectile
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        transform.position += direction * -0.1f;
    }
}
