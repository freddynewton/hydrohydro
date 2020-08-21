using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowDroneUnit : EnemyUnit
{
    public GameObject shadow;

    public override void DoDamage(GameObject bulletObj, Bullet bulletSettings)
    {
        base.DoDamage(bulletObj, bulletSettings);
    }

    public override void death(GameObject bullet)
    {
        base.death(bullet);
        shadow.transform.localScale = new Vector3(0.6f, 0.6f, 1);
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }
}
