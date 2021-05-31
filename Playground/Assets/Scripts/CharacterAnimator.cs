using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;
    private int velocityXHash;
    private int velocityZHash;
    private float velocityX;
    private float velocityZ;

    public void SetUp(Animator animator)
    {
        this.animator = animator;
        velocityXHash = Animator.StringToHash("VelocityX");
        velocityZHash = Animator.StringToHash("VelocityZ");
        velocityX = 0.0f;
        velocityZ = 0.0f;
    }

    public void HandleAnimation(Vector3 inputValue)
    {
        velocityX = Mathf.Clamp(inputValue.x, -1.0f, 1.0f);
        velocityZ = Mathf.Clamp(inputValue.z, -1.0f, 1.0f);

        animator.SetFloat(velocityXHash, velocityX);
        animator.SetFloat(velocityZHash, velocityZ);
    }
}
