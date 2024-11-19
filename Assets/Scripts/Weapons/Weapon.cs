using UnityEngine;

public class Weapon : Item
{
    public WeaponData weaponData;
    private float cooldown;
    private float currentCooldownDuration;
    protected PlayerMovement pm;
    public virtual void Initialize(WeaponData data)
    {
        base.Initialize(data); // Gọi phương thức Initialize của lớp cha
        currentCooldownDuration = data.cooldownDuration;
    }
    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        Initialize(weaponData);
    }
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
        cooldown = currentCooldownDuration;
    }
}