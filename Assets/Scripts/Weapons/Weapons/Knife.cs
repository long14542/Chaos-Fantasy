using UnityEngine;

public class Knife : WeaponsMother
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
        GameObject knife = Instantiate(weaponData.Prefab);
        knife.transform.position = this.transform.position;

        // Reference KnifeBehavior and check direction
        knife.GetComponent<KnifeBehavior>().CheckDirection(pm.ShootDir);
    }

    protected override void LevelUpWeapon()
    {
        base.LevelUpWeapon();

        // Find every knife in the game and update their damage
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Knife");
        foreach (var obj in objs)
        {
            obj.GetComponent<KnifeBehavior>().IncreaseDamage();
        }
    }
}
