using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputDetection : MonoBehaviour
{
    private Transform cameraTarget;
    private Vector2 rawMoveInputValue;
    private Vector3 movementValue;
    private Vector3 cameraRotationValue;
    private bool isRunningPressed;
    private bool isInAttackMode;

    private Action<bool> onAttackModePressed;
    private Action onAttackPressed;
    private Action onJumpPressed;
    private Action<Vector3> onRotatingCamera;

    public Action<bool> OnAttackModePressed { get => onAttackModePressed; set => onAttackModePressed = value; }
    public Action OnAttackPressed { get => onAttackPressed; set => onAttackPressed = value; }
    public Action OnJumpPressed { get => onJumpPressed; set => onJumpPressed = value; }
    public Action<Vector3> OnRotatingCamera { get => onRotatingCamera; set => onRotatingCamera = value; }

    public void SetUp(Transform cameraTarget)
    {
        this.cameraTarget = cameraTarget;
        movementValue = Vector3.zero;
        cameraRotationValue = Vector3.zero;
        isRunningPressed = false;
        isInAttackMode = false;
    }

    public void HandleInput(out Vector3 movementValue, out bool isRunningPressed)
    {
        if (this.movementValue == Vector3.zero)
            this.isRunningPressed = false;

        movementValue = this.movementValue;;
        isRunningPressed = this.isRunningPressed;
    }

    private void OnMovement(InputValue value)
    {
        rawMoveInputValue = value.Get<Vector2>();
        UpdateMovement();
    }

    private void OnRotateCamera(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        cameraRotationValue = new Vector3(input.y, input.x, 0);

        onRotatingCamera?.Invoke(cameraRotationValue);
        UpdateMovement();
    }

    private void OnRun()
    {
        isRunningPressed = !isRunningPressed;
    }

    private void OnAttackMode()
    {
        isInAttackMode = !isInAttackMode;
        onAttackModePressed?.Invoke(isInAttackMode);
    }

    private void OnAttack()
    {
        onAttackPressed?.Invoke();
    }

    private void OnJump()
    {
        onJumpPressed?.Invoke();
    }

    private void UpdateMovement() 
    {
        Vector3 newMoveInputValue = new Vector3(rawMoveInputValue.x, 0, rawMoveInputValue.y);

        if (cameraTarget)
        {
            newMoveInputValue = rawMoveInputValue.y * cameraTarget.transform.forward + rawMoveInputValue.x * cameraTarget.transform.right;
            newMoveInputValue = new Vector3(newMoveInputValue.x, 0.0f, newMoveInputValue.z);
        }

        movementValue = newMoveInputValue;
    }
}
