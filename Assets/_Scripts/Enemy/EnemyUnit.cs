using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = Vector2.zero;
    }

    public override void death(GameObject bullet)
    {
        base.death(bullet);
    }

    public override void DoDamage(GameObject bulletObj, Bullet bulletSettings)
    {
        base.DoDamage(bulletObj, bulletSettings);
    }
}
