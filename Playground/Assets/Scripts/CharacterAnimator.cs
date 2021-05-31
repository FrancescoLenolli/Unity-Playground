using System.Collections;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public float maxAnimationValue = 2.0f;

    private Animator animator;
    private int velocityXHash;
    private int fightingLayerIndex;
    private int isAttackingHash;
    private float velocityX;

    public void SetUp(Animator animator)
    {
        this.animator = animator;
        fightingLayerIndex = animator.GetLayerIndex("Fighting");
        velocityXHash = Animator.StringToHash("VelocityX");
        isAttackingHash = Animator.StringToHash("isAttacking");
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
    
    public void AttackAnimation(bool isAttacking)
    {
        animator.SetBool(isAttackingHash, isAttacking);
    }
}
