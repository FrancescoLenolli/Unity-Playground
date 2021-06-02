using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    public HandCollider handCollider = null;
    public float speed = 1.0f;
    public float runSpeedMultiplier = 2.0f;
    [Range(0.1f, 1f)]
    public float maxBackwardsSpeedValue = -0.3f;
    public float attackCooldown = 0.5f;
    public float jumpForce = 1.0f;
    public float jumpTime = 1.0f;

    private new Rigidbody rigidbody;
    private Vector3 moveInputValue;
    private bool isRunningPressed;
    private bool isInAttackMode;
    private bool isJumping;
    private float attackCooldownTime;
    private float jumpTimeValue;

    private Action<bool> onAttackModePressed;
    private Action onAttackPressed;

    public Action<bool> OnAttackModePressed { get => onAttackModePressed; set => onAttackModePressed = value; }
    public Action OnAttackPressed { get => onAttackPressed; set => onAttackPressed = value; }

    public void SetUp(Rigidbody rigidbody)
    {
        this.rigidbody = rigidbody;
        moveInputValue = Vector2.zero;
        isRunningPressed = false;
        isInAttackMode = false;
        attackCooldownTime = attackCooldown;
        jumpTimeValue = 0.0f;

        handCollider.OnEnemyCollision += OnEnemyHit;
    }

    public void HandleMovement(out float inputValue, out bool isRunning)
    {
        bool canRun = isRunningPressed && IsGrounded();
        moveInputValue.z = Mathf.Clamp(moveInputValue.z, -maxBackwardsSpeedValue, 1.0f);
        Vector3 velocity = moveInputValue * Time.deltaTime * (canRun ? speed * runSpeedMultiplier : speed);
        Vector3 newPosition = transform.position + velocity;

        rigidbody.MovePosition(newPosition);

        HandleJump();
        HandleAttack();

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

    private void SetHandColliderStatus(bool enabled)
    {
        handCollider.Collider.enabled = enabled;
    }

    private void OnEnemyHit(Enemy enemy)
    {
        Debug.Log(enemy.name);
    }

    private void HandleJump()
    {
        if (jumpTimeValue > 0.0f)
        {
            Jump();
            jumpTimeValue -= Time.fixedDeltaTime;
        }
    }

    private void HandleAttack()
    {
        attackCooldownTime = Mathf.Clamp(attackCooldownTime - Time.deltaTime, 0.0f, attackCooldown);
        if (attackCooldownTime <= 0 && handCollider.Collider.enabled)
            SetHandColliderStatus(false);
    }

    private void Jump()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        RaycastHit hitInfo;
        float offset = 0.1f;
        Vector3 startPoint = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        Vector3 direction = Vector3.down;

        Physics.Raycast(startPoint, direction, out hitInfo, 0.1f + offset);
        return hitInfo.transform != null;
    }

    //---------- PlayerInput methods ----------//

    private void OnMovement(InputValue value)
    {
        Vector2 inputValue = value.Get<Vector2>();
        moveInputValue = new Vector3(inputValue.x, 0, inputValue.y);
    }

    private void OnRun(InputValue value)
    {
        isRunningPressed = value.isPressed;
    }

    private void OnAttackMode()
    {
        isInAttackMode = !isInAttackMode;
        onAttackModePressed?.Invoke(isInAttackMode);
    }

    private void OnAttack()
    {
        if (!isInAttackMode)
            return;

        if (attackCooldownTime <= 0.0f)
        {
            onAttackPressed?.Invoke();
            attackCooldownTime = attackCooldown;
            SetHandColliderStatus(true);
        }
    }

    private void OnJump(InputValue value)
    {
        isJumping = value.isPressed;
        jumpTimeValue = isJumping && IsGrounded() ? jumpTime : -1.0f;
    }
}
