﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/AttackAction")]
public class AttackAction : ActionAI
{
    public override void use(UtilityAIHandler controller)
    {
        Vector3 dir = (controller.target.transform.position - controller.gameObject.transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(controller.gameObject.transform.position, dir);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject == controller.target.gameObject)
            {
                controller.unit.weaponScript.shootEnemy(controller.unit);
            }
        }
    }
}
