using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAction : ActionAI
{
    public override void use(UtilityAIHandler controller)
    {
        Vector3 dir = (controller.target.transform.position - controller.gameObject.transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(controller.gameObject.transform.position, dir);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject == controller.target.gameObject)
            {
                controller.aiPath.destination = findHidePoint(controller);
            }
        }
    }

    private Vector2 findHidePoint(UtilityAIHandler controller)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(controller.gameObject.transform.position, 2f);

        Vector2 pos = Vector2.zero;
        int foundCol = -1;

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.layer == 8)
            {
                if (foundCol == -1)
                {
                    foundCol = i;
                } else if (Vector2.Distance(controller.gameObject.transform.position, hitColliders[i].gameObject.transform.position) <
                    Vector2.Distance(controller.gameObject.transform.position, hitColliders[foundCol].gameObject.transform.position))
                {
                    foundCol = i;
                }
            }
        }



        return pos;
    }
}
