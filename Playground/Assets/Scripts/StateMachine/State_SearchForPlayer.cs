using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_SearchForPlayer : State
{
    public override void UpdateState()
    {
        if (!Owner.TargetPlayer)
            Owner.TargetPlayer = FindObjectOfType<PlayerControl>();

        if (!Owner.TargetPlayer)
            return;

        float distance = CharacterUtilities.SqrDistance(Owner.transform, Owner.TargetPlayer.transform);
        float sqrDetectionDistance = Owner.detectionDistance * Owner.detectionDistance;
        if (distance <= sqrDetectionDistance)
        {
            StateMachine.SwitchState(typeof(State_LookAtPlayer));
        }
    }
}
