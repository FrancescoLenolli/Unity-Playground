using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Vector3 inputValue = Vector3.zero;

        characterMovement.HandleMovement(out inputValue);
        characterAnimator.HandleAnimation(inputValue);
    }

    private void SetUp()
    {
        characterMovement = GetComponent<CharacterMovement>();
        characterAnimator = GetComponent<CharacterAnimator>();
        CharacterController controller = GetComponent<CharacterController>();
        Animator animator = GetComponentInChildren<Animator>();

        if (!characterMovement)
        {
            characterMovement = gameObject.AddComponent<CharacterMovement>();
        }

        if (!characterAnimator)
        {
            characterAnimator = gameObject.AddComponent<CharacterAnimator>();
        }

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
