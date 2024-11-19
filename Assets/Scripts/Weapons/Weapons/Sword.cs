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

        GameObject slash = Instantiate(weaponData.prefab);
        slash.GetComponent<SwordProjectile>().CheckDirection(pm.ShootDir);

        slash.transform.position = this.transform.position;
    }

    public override void LevelUp()
    {
        base.LevelUp();
    }
}