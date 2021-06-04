using UnityEngine;

public class RandomAnimation : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        animator.SetInteger("Punch_Index", Random.Range(0, 3));
    }
}
