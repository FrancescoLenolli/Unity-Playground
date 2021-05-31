using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float runSpeedMultiplier = 2.0f;
    [Range(-1, -0.1f)]
    public float maxBackwardsSpeedValue = -0.3f;

    private CharacterController controller;
    private Vector3 moveInputValue;
    private bool isRunning;

    public void SetUp(CharacterController controller)
    {
        this.controller = controller;
        moveInputValue = Vector2.zero;
        isRunning = false;
    }

    public void HandleMovement(out Vector3 inputValue)
    {
        isRunning = moveInputValue.z > 0;
        moveInputValue.z = Mathf.Clamp(moveInputValue.z, maxBackwardsSpeedValue, 1.0f);

        Vector3 velocity = moveInputValue * Time.deltaTime * (isRunning ? speed * runSpeedMultiplier : speed);
        controller.Move(velocity);

        inputValue = moveInputValue;
    }

    private void OnMovement(InputValue value)
    {
        Vector2 inputValue = value.Get<Vector2>();
        moveInputValue = new Vector3(inputValue.x, 0, inputValue.y);
    }

    private void OnRun(InputValue value)
    {
        isRunning = value.isPressed;
    }
}
