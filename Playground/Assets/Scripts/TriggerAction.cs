using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerAction : StateMachineBehaviour
{
    public UnityEvent OnAction;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        OnAction?.Invoke();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        OnAction?.Invoke();
    }
}
