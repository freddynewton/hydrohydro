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
}
