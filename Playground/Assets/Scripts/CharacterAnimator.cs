using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public float maxAnimationValue = 2.0f;

    private Animator animator;
    private int velocityXHash;
    private float velocityX;
    private float velocityZ;

    public void SetUp(Animator animator)
    {
        this.animator = animator;
        velocityXHash = Animator.StringToHash("VelocityX");
        velocityX = 0.0f;
        velocityZ = 0.0f;
    }

    public void HandleAnimation(float inputValue, bool isRunning)
    {
        velocityX = Mathf.Clamp(inputValue, 0.0f, isRunning ? maxAnimationValue : maxAnimationValue * 0.5f);

        animator.SetFloat(velocityXHash, velocityX);
    }
}
