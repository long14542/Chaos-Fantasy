using UnityEngine;

// Base weapon script
public class WeaponsMother : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    private float cooldown;

    public int currentLevel;
    protected PlayerMovement pm;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        cooldown = weaponData.CooldownDuration;
        currentLevel = weaponData.Level;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0f)
        {
            Attack();
        }
    }

    protected virtual void LevelUpWeapon()
    {
        if (currentLevel >= weaponData.MaxLevel)
        {
            return;
        }
        currentLevel += 1;
        Debug.Log("Leveled up: " + currentLevel);
    }

    protected virtual void Attack()
    {
        cooldown = weaponData.CooldownDuration;
    }
}
