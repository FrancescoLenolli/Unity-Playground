using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform lookAtTarget = null;
    public float smoothness = 1.0f;
    public float rotationSpeed = 1.0f;

    private Vector3 cameraOffset;
    private Vector3 cameraPosition;
    private Vector3 inputValue;
    private Quaternion rotationAngle;

    private void Start()
    {
        cameraOffset = transform.position - lookAtTarget.position;
    }

    private void FixedUpdate()
    {
        Quaternion horizontalRotation = Quaternion.AngleAxis(inputValue.x * rotationSpeed, Vector3.up);
        Quaternion verticalRotation = Quaternion.AngleAxis(inputValue.y * rotationSpeed, Vector3.right);

        rotationAngle = horizontalRotation * verticalRotation;

        cameraOffset = rotationAngle * cameraOffset;

        cameraPosition = lookAtTarget.position + cameraOffset;

        transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothness * Time.fixedDeltaTime);
        transform.LookAt(lookAtTarget);
    }

    public void SetInputValue(Vector3 inputValue)
    {
        this.inputValue = Vector3.Normalize(inputValue);
    }
}
