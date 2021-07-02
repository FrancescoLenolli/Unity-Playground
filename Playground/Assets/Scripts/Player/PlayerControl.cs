using System;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(PlayerInputDetection))]
public class PlayerControl : MonoBehaviour
{
    public FollowObject cameraTarget = null;

    private CharacterMovement characterMovement;
    private CharacterAnimator characterAnimator;
    private PlayerInputDetection playerInputDetection;
    private IInteractable targetedObject;
    private Vector3 movementValue;
    private float inputValue;
    private bool isRunning;

    private void Awake()
    {
        SetUp();
    }

    private void FixedUpdate()
    {
        playerInputDetection.HandleInput(out movementValue, out isRunning);
        characterMovement.HandleMovement(movementValue, isRunning, out inputValue, out isRunning);
        characterMovement.HandleRotation();
        characterAnimator.HandleAnimation(inputValue, isRunning);
    }

    private void SetUp()
    {
        RaycastTargeting raycastTargeting = cameraTarget.GetComponent<RaycastTargeting>();
        characterMovement = GetComponent<CharacterMovement>();
        characterAnimator = GetComponent<CharacterAnimator>();
        playerInputDetection = GetComponent<PlayerInputDetection>();
        Rigidbody rb = GetComponent<Rigidbody>();
        Animator animator = GetComponentInChildren<Animator>();

        playerInputDetection.SetUp(cameraTarget.transform);

        if (rb)
            characterMovement.SetUp(rb);

        if (animator)
            characterAnimator.SetUp(animator);

        if (cameraTarget)
            playerInputDetection.OnRotatingCamera += cameraTarget.SetRotationInput;

        playerInputDetection.OnAttackModePressed += characterMovement.SetAttackMode;
        playerInputDetection.OnAttackModePressed += characterAnimator.SetFightingStanceAnimation;
        playerInputDetection.OnAttackPressed += characterMovement.Attack;
        playerInputDetection.OnJumpPressed += characterMovement.SetJump;
        playerInputDetection.OnInteractPressed += Interact;
        characterMovement.OnAttackPressed += characterAnimator.AttackAnimation;
        characterMovement.OnJumping += characterAnimator.JumpAnimation;
        raycastTargeting.OnTargeting += SetTargetedObject;
    }

    private void SetTargetedObject(IInteractable newObject)
    {
        if (targetedObject == newObject)
            return;

        IInteractable previousObject = targetedObject;
        targetedObject = newObject;

        if (previousObject != null)
            previousObject.OffFocus();
        if (targetedObject != null)
            targetedObject.OnFocus();
    }

    private void Interact()
    {
        //Action onInteract = targetedObject != null ? targetedObject.Interact : new Action(() => { });
        //onInteract?.Invoke();

        if (targetedObject != null)
            targetedObject.Interact();
    }
}
