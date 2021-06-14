using System;
using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public HandCollider handCollider = null;
    public float rotateCameraSpeed = 1.0f;
    public float speed = 1.0f;
    public float runSpeedMultiplier = 2.0f;
    [Range(0.1f, 1f)]
    public float maxBackwardsSpeedValue = -0.3f;
    public float attackCooldown = 0.5f;
    public float jumpForce = 1.0f;

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
        bool isGrounded = IsGrounded();
        bool canRun = isRunningPressed && isGrounded;
        moveInputValue = movementValue;

        moveInputValue.z = Mathf.Clamp(moveInputValue.z, -maxBackwardsSpeedValue, 1.0f);
        Vector3 velocity = (canRun ? speed * runSpeedMultiplier : speed) * Time.deltaTime * moveInputValue;
        Vector3 newPosition = transform.position + velocity;

        rigidbody.MovePosition(newPosition);

        HandleJump(isGrounded);

        if (isGrounded)
            rigidbody.useGravity = false;
        else
            rigidbody.useGravity = true;

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
        canJump = IsGrounded();
    }

    public void Attack()
    {
        if (!isInAttackMode)
            return;
        if (attackCooldown > 0.0f)
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
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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

    private bool IsGrounded()
    {
        float offset = 0.1f;
        Vector3 startPoint = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        Vector3 direction = Vector3.down;

        Physics.Raycast(startPoint, direction, out RaycastHit hitInfo, 0.1f + offset);
        return hitInfo.transform != null;
    }

    private bool IsFalling()
    {
        return rigidbody.velocity.y < 0.0f;
    }

    private IEnumerator AttackRoutine()
    {
        onAttackPressed?.Invoke();
        attackCooldownTime = attackCooldown;
        SetHandColliderStatus(true);

        while (attackCooldownTime > 0.0f)
        {
            attackCooldownTime = Mathf.Clamp(attackCooldownTime - Time.deltaTime, 0.0f, attackCooldown);
            yield return null;
        }

        SetHandColliderStatus(false);
        yield return null;
    }
}
