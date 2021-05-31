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

    private void Update()
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
        CharacterController controller = GetComponent<CharacterController>();
        Animator animator = GetComponentInChildren<Animator>();

        if (!controller)
        {
            Debug.LogWarning($"{gameObject.name} missing CharacterController component!");
        }
        else
        {
            characterMovement.SetUp(controller);
        }

        if (!animator)
        {
            Debug.LogWarning($"{gameObject.name} model missing Animator component!");
        }
        else
        {
            characterAnimator.SetUp(animator);
        }
    }
}
