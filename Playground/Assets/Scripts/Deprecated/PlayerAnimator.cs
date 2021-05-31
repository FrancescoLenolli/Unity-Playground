using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public float acceleration = 0.1f;
    public float deceleration = 0.1f;

    private Animator animator;
    private int isWalkingHash;
    private int isRunningHash;
    private int velocityHash;
    private int velocityXHash;
    private int velocityZHash;
    private float velocityX = 0.0f;
    private float velocityZ = 0.0f;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        if (animator)
        {
            isWalkingHash = Animator.StringToHash("isWalking");
            isRunningHash = Animator.StringToHash("isRunning");
            velocityHash = Animator.StringToHash("Velocity");
            velocityXHash = Animator.StringToHash("VelocityX");
            velocityZHash = Animator.StringToHash("VelocityZ");
        }
    }

    void Update()
    {
        TwoDimensionalBlendAnimations();
    }

    /// <summary>
    /// Animation switch using Booleans
    /// </summary>
    private void BoolAnimations()
    {
        bool walkKeyPressed = Input.GetKey(KeyCode.W);
        bool runKeyPressed = Input.GetKey(KeyCode.LeftShift);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if (!isWalking && walkKeyPressed)
            animator.SetBool(isWalkingHash, true);

        if (isWalking && !walkKeyPressed)
            animator.SetBool(isWalkingHash, false);

        if (!isRunning && walkKeyPressed && runKeyPressed)
            animator.SetBool(isRunningHash, true);

        if (isRunning && (!walkKeyPressed || !runKeyPressed))
            animator.SetBool(isRunningHash, false);
    }

    /// <summary>
    /// Animation switch using Float and BlendTree
    /// </summary>
    private void BlendFloatAnimations()
    {
        bool walkKeyPressed = Input.GetKey(KeyCode.W);

        if(walkKeyPressed)
        {
            velocityX = Mathf.Clamp(velocityX += Time.deltaTime * acceleration, 0.0f, 1.0f);
        }
        else
        {
            velocityX = Mathf.Clamp(velocityX -= Time.deltaTime * deceleration, 0.0f, 1.0f);
        }

        animator.SetFloat(velocityHash, velocityX);
    }

    /// <summary>
    /// Animation switch using Float
    /// </summary>
    private void TwoDimensionalBlendAnimations()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);

        float tolerance = runPressed ? 2.0f : 0.5f;

        if(forwardPressed)
        {
            velocityZ = Mathf.Clamp(velocityZ += Time.deltaTime * acceleration, 0.0f, tolerance);
        }
        else
        {
            velocityZ = Mathf.Clamp(velocityZ -= Time.deltaTime * deceleration, 0.0f, tolerance);
        }

        if(leftPressed)
        {
            velocityX = Mathf.Clamp(velocityX -= Time.deltaTime * acceleration, -tolerance, tolerance);
        }
        else if(!leftPressed && velocityX < 0.0f)
        {
            velocityX = Mathf.Clamp(velocityX += Time.deltaTime * deceleration, -tolerance, 0.0f);
        }


        if(rightPressed)
        {
            velocityX = Mathf.Clamp(velocityX += Time.deltaTime * acceleration, -tolerance, tolerance);
        }
        else if(!rightPressed && velocityX > 0.0f)
        {
            velocityX = Mathf.Clamp(velocityX -= Time.deltaTime * deceleration, 0.0f, tolerance);
        }

        animator.SetFloat(velocityXHash, velocityX);
        animator.SetFloat(velocityZHash, velocityZ);
    }
}
