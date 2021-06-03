using System.Collections;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public float maxAnimationValue = 2.0f;

    private Animator animator;
    private int velocityXHash;
    private int attackTriggerHash;
    private int jumpTriggerHash;
    private int fightingLayerIndex;
    private float velocityX;

    public void SetUp(Animator animator)
    {
        this.animator = animator;
        fightingLayerIndex = animator.GetLayerIndex("Fighting");
        velocityXHash = Animator.StringToHash("VelocityX");
        attackTriggerHash = Animator.StringToHash("Attack");
        jumpTriggerHash = Animator.StringToHash("Jump");
        velocityX = 0.0f;
    }

    public void HandleAnimation(float inputValue, bool isRunning)
    {
        velocityX = Mathf.Clamp(isRunning ? inputValue * 2 : inputValue, 0.0f, isRunning ? maxAnimationValue : maxAnimationValue * 0.5f);

        animator.SetFloat(velocityXHash, velocityX);
    }

    public void SetFightingStanceAnimation(bool isFighting)
    {
        animator.SetLayerWeight(fightingLayerIndex, isFighting ? 1 : 0);
    }   
    
    public void AttackAnimation()
    {
        animator.SetTrigger(attackTriggerHash);
    }

    public void JumpAnimation()
    {
        animator.SetTrigger(jumpTriggerHash);
    }
}
