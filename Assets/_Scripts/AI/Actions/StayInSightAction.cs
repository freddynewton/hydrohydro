using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[CreateAssetMenu(menuName = "AI/Actions/StayInSightAction")]
public class StayInSightAction : ActionAI
{
    public override void use(UtilityAIHandler controller)
    {
        Vector3 dir = (controller.target.transform.position - controller.gameObject.transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(controller.gameObject.transform.position, dir);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject != controller.target.gameObject)
            {
                controller.aiPath.destination = controller.transform.position + dir;
            }
        }
    }
}
