using UnityEngine;

// Base weapon script
public class WeaponsMother : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    private float cooldown;

    protected PlayerMovement pm;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        cooldown = weaponData.CooldownDuration;
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

    protected virtual void Attack()
    {
        cooldown = weaponData.CooldownDuration;
    }
}
