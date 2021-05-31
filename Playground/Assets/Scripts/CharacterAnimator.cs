using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public Vector2 xValueRange = Vector2.zero;
    public float maxAnimationValue = 2.0f;
    //public Vector2 zValueRange = Vector2.zero;

    private Animator animator;
    private int velocityXHash;
    //private int velocityZHash;
    private float velocityX;
    private float velocityZ;

    public void SetUp(Animator animator)
    {
        this.animator = animator;
        velocityXHash = Animator.StringToHash("VelocityX");
        //velocityZHash = Animator.StringToHash("VelocityZ");
        velocityX = 0.0f;
        velocityZ = 0.0f;
    }

    public void HandleAnimation(float inputValue, bool isRunning)
    {
        velocityX = Mathf.Clamp(inputValue, 0.0f, isRunning ? maxAnimationValue : maxAnimationValue * 0.5f);
        //velocityZ = Mathf.Abs(Mathf.Clamp(isRunning ? inputValue.z * 2 : inputValue.z, zValueRange.x, zValueRange.y));

        animator.SetFloat(velocityXHash, velocityX);
        //animator.SetFloat(velocityZHash, velocityZ);
    }
}
