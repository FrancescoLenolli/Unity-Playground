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

    private CharacterController controller;
    private Vector3 moveInputValue;
    private bool isRunningPressed;
    private bool isInAttackMode;
    private float attackCooldownTime;

    private Action<bool> onAttackModePressed;
    private Action onAttackPressed;

    public Action<bool> OnAttackModePressed { get => onAttackModePressed; set => onAttackModePressed = value; }
    public Action OnAttackPressed { get => onAttackPressed; set => onAttackPressed = value; }

    public void SetUp(CharacterController controller)
    {
        this.controller = controller;
        moveInputValue = Vector2.zero;
        isRunningPressed = false;
        isInAttackMode = false;
        attackCooldownTime = attackCooldown;

        handCollider.OnEnemyCollision += OnEnemyHit;
    }

    public void HandleMovement(out float inputValue, out bool isRunning)
    {
        moveInputValue.z = Mathf.Clamp(moveInputValue.z, -maxBackwardsSpeedValue, 1.0f);
        Vector3 velocity = moveInputValue * Time.deltaTime * (isRunningPressed ? speed * runSpeedMultiplier : speed);

        controller.Move(velocity);

        attackCooldownTime = Mathf.Clamp(attackCooldownTime - Time.deltaTime, 0.0f, attackCooldown);
        if (attackCooldownTime <= 0 && handCollider.Collider.enabled)
            SetHandColliderStatus(false);

        float inputX = Mathf.Abs(moveInputValue.x);
        float inputZ = Mathf.Abs(moveInputValue.z);
        inputValue = inputX + inputZ;
        isRunning = isRunningPressed;
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

    private void OnEnemyHit(Enemy enemy)
    {
        Debug.Log(enemy.name);
    }
}
