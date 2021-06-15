using System;
using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public HandCollider handCollider = null;
    public MovementValues movementValues = null;

    private new Rigidbody rigidbody;
    private Vector3 moveInputValue;
    private bool isInAttackMode;
    private bool canJump;
    private bool isJumping;
    private float attackCooldownTime;

    private Action<bool> onAttackModePressed;
    private Action onAttackPressed;
    private Action<bool> onJumping;
    private Action<Vector3> onRotateCamera;

    public Action<bool> OnAttackModePressed { get => onAttackModePressed; set => onAttackModePressed = value; }
    public Action OnAttackPressed { get => onAttackPressed; set => onAttackPressed = value; }
    public Action<bool> OnJumping { get => onJumping; set => onJumping = value; }
    public Action<Vector3> OnRotatingCamera { get => onRotateCamera; set => onRotateCamera = value; }

    public void SetUp(Rigidbody rigidbody)
    {
        this.rigidbody = rigidbody;
        moveInputValue = Vector2.zero;
        isInAttackMode = false;
        isJumping = false;
        attackCooldownTime = 0.0f;

        handCollider.OnEnemyCollision += OnEnemyHit;
    }

    public void HandleMovement(Vector3 movementValue, bool isRunningPressed, out float inputValue, out bool isRunning)
    {
        bool isGrounded = CharacterUtilities.IsGrounded(transform);
        bool canRun = isRunningPressed && isGrounded;
        moveInputValue = movementValue;

        Vector3 velocity = (canRun ? movementValues.speed * movementValues.runSpeedMultiplier : movementValues.speed) * Time.fixedDeltaTime * moveInputValue;
        Vector3 newPosition = transform.position + velocity;

        rigidbody.MovePosition(newPosition);

        HandleJump(isGrounded);

        rigidbody.useGravity = !isGrounded;

        float inputX = Mathf.Abs(moveInputValue.x);
        float inputZ = Mathf.Abs(moveInputValue.z);
        inputValue = inputX + inputZ;
        isRunning = canRun;
    }

    public void HandleRotation()
    {
        if (moveInputValue == Vector3.zero)
            return;

        Vector3 lookAtPosition = new Vector3(moveInputValue.x, 0.0f, moveInputValue.z);
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(lookAtPosition);

        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, 1.0f);
    }

    public void SetAttackMode(bool isInAttackMode)
    {
        this.isInAttackMode = isInAttackMode;
    }

    public void SetJump()
    {
        canJump = CharacterUtilities.IsGrounded(transform);
    }

    public void Attack()
    {
        if (!isInAttackMode)
            return;
        if (attackCooldownTime > 0.0f)
            return;

        StartCoroutine(AttackRoutine());
    }

    private void HandleJump(bool isGrounded)
    {
        if (canJump)
        {
            Jump();
        }
        else if (isJumping && isGrounded && rigidbody.useGravity)
        {
            isJumping = false;
            onJumping.Invoke(false);
        }
    }

    private void Jump()
    {
        isJumping = true;
        rigidbody.AddForce(Vector3.up * movementValues.jumpForce, ForceMode.Impulse);
        onJumping?.Invoke(true);
        canJump = false;
    }

    private void SetHandColliderStatus(bool enabled)
    {
        handCollider.Collider.enabled = enabled;
    }

    private void OnEnemyHit(Enemy enemy)
    {
        Debug.Log(enemy.name);
    }

    private IEnumerator AttackRoutine()
    {
        onAttackPressed?.Invoke();
        attackCooldownTime = movementValues.attackCooldown;
        SetHandColliderStatus(true);

        while (attackCooldownTime > 0.0f)
        {
            attackCooldownTime = Mathf.Clamp(attackCooldownTime - Time.deltaTime, 0.0f, movementValues.attackCooldown);
            yield return null;
        }

        SetHandColliderStatus(false);
        yield return null;
    }
}
