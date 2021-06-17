using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_LookAtPlayer : State
{
    public override void UpdateState()
    {
        Owner.transform.LookAt(Owner.TargetPlayer.transform);
    }
}
