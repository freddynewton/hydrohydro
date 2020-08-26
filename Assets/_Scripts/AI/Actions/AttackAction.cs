using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/AttackAction")]
public class AttackAction : ActionAI
{
    public override void use(UtilityAIHandler controller)
    {
        controller.unit.weaponScript.shootEnemy(controller.unit);
    }
}
