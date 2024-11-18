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

        // Create knife GameObject and assign its position to the current class which is parented to the Player
        GameObject knife = Instantiate(weaponData.prefab);
        knife.transform.position = this.transform.position;

        // Reference KnifeBehavior and check direction
        knife.GetComponent<KnifeProjectile>().CheckDirection(pm.ShootDir);
    }

    public override void LevelUp()
    {
        base.LevelUp();
    }
}
