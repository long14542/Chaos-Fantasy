using UnityEngine;

public class KnifeBehavior : ProjectileBehavior
{
    private Knife kn;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        kn = FindObjectOfType<Knife>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += kn.speed * Time.deltaTime * direction;
    }
}
