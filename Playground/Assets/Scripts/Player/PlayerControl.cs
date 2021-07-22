using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(PlayerInputDetection))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private FollowObject cameraTarget = null;

    private CharacterMovement characterMovement;
    private CharacterAnimator characterAnimator;
    private PlayerInputDetection playerInputDetection;
    private PlayerInteraction playerInteraction;
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
        Rigidbody rb = GetComponent<Rigidbody>();
        Animator animator = GetComponentInChildren<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
        characterAnimator = GetComponent<CharacterAnimator>();
        playerInputDetection = GetComponent<PlayerInputDetection>();
        playerInteraction = GetComponent<PlayerInteraction>();

        playerInputDetection.SetUp(cameraTarget.transform);

        if (rb)
            characterMovement.SetUp(rb);

        if (animator)
            characterAnimator.SetUp(animator);

        if (cameraTarget)
            playerInputDetection.OnRotatingCamera += cameraTarget.SetRotationInput;

        if (playerInteraction)
        {
            if (raycastTargeting)
            {
                raycastTargeting.enableTargeting = true;
                raycastTargeting.OnTargeting += playerInteraction.SetTargetedObject;
            }

            playerInputDetection.OnInteractPressed += playerInteraction.Interact;
        }

        playerInputDetection.OnAttackModePressed += characterMovement.SetAttackMode;
        playerInputDetection.OnAttackModePressed += characterAnimator.SetFightingStanceAnimation;
        playerInputDetection.OnAttackPressed += characterMovement.Attack;
        playerInputDetection.OnJumpPressed += characterMovement.SetJump;
        characterMovement.OnAttackPressed += characterAnimator.AttackAnimation;
        characterMovement.OnJumping += characterAnimator.JumpAnimation;
    }
}
