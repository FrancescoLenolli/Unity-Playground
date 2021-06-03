using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterAnimator))]
public class PlayerControl : MonoBehaviour
{
    private CharacterMovement characterMovement;
    private CharacterAnimator characterAnimator;

    private void Awake()
    {
        SetUp();
    }

    private void FixedUpdate()
    {
        float inputValue = 0.0f;
        bool isRunning = false;

        characterMovement.HandleMovement(out inputValue, out isRunning);
        characterMovement.HandleRotation();
        characterAnimator.HandleAnimation(inputValue, isRunning);
    }

    private void SetUp()
    {
        characterMovement = GetComponent<CharacterMovement>();
        characterAnimator = GetComponent<CharacterAnimator>();
        Rigidbody rb = GetComponent<Rigidbody>();
        Animator animator = GetComponentInChildren<Animator>();

        if (!rb)
        {
            Debug.LogWarning($"{gameObject.name} missing Rigidbody component!");
        }
        else
        {
            characterMovement.SetUp(rb);
        }

        if (!animator)
        {
            Debug.LogWarning($"{gameObject.name} model missing Animator component!");
        }
        else
        {
            characterAnimator.SetUp(animator);
        }

        characterMovement.OnAttackModePressed += characterAnimator.SetFightingStanceAnimation;
        characterMovement.OnAttackPressed += characterAnimator.AttackAnimation;
        characterMovement.OnJumpPressed += characterAnimator.JumpAnimation;
    }
}
