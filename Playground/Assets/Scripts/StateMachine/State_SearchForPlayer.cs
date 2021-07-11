using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_SearchForPlayer : State
{
    private PlayerControl target;

    public override void UpdateState()
    {
        if (!target)
        {
            target = FindObjectOfType<PlayerControl>();
            StateMachine.SwitchState(typeof(State_Move));
            return;
        }

        float sqrDistance = CharacterUtilities.SqrDistance(Owner.transform, target.transform);
        float sqrDetectionDistance = Owner.detectionDistance * Owner.detectionDistance;
        Owner.TargetPlayer = sqrDistance <= sqrDetectionDistance ? target : null;

        StateMachine.SwitchState(typeof(State_Move));
    }
}
