using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/FollowTargetAction")]
public class FollowTargetAction : ActionAI
{
    public override void use(UtilityAIHandler controller)
    {
        controller.aiPath.destination = controller.target.transform.position;
        
        //controller.aiPath.FinalizeMovement(controller.target.transform.position, Quaternion.identity);
    }
}
