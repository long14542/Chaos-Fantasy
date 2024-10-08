using UnityEngine;

// Base weapon script
public class WeaponsMother : MonoBehaviour
{
    public GameObject prefab;
    public float speed;
    public float cooldownDuration;
    private float cooldown;
    public int pierce;

    protected PlayerMovement pm;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        cooldown = cooldownDuration;
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
        cooldown = cooldownDuration;
    }
}
