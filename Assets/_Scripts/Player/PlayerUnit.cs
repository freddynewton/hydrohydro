using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{

    public override void Start()
    {
        base.Start();
        Physics2D.IgnoreLayerCollision(10, 9);
    }

    public override void Update()
    {
        base.Update();
    }
}
