using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target = null;
    public float rotationSpeed = 10.0f;
    public Vector2 rotationLimit = Vector2.zero;

    private Vector3 offset;
    private Vector3 newPosition;
    private Vector3 rotationInputValue;

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        HandlePosition();
        HandleRotation();
    }

    public void SetRotationInput(Vector3 rotationInputValue)
    {
        rotationInputValue.x = -rotationInputValue.x;
        rotationInputValue.z = 0.0f;

        this.rotationInputValue = rotationInputValue;
    }

    private void HandleRotation()
    {
        Vector3 rotationVector = rotationSpeed * Time.fixedDeltaTime * rotationInputValue;

        transform.Rotate(rotationVector);
        float rotationX = ClampAngle(transform.rotation.eulerAngles.x, rotationLimit.x, rotationLimit.y);

        transform.rotation = Quaternion.Euler(rotationX, transform.rotation.eulerAngles.y, 0.0f);
    }

    private void HandlePosition()
    {
        newPosition = target.position + offset;
        transform.position = newPosition;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle > 180)
        {
            angle -= 360;
        }
        angle = Mathf.Clamp(angle, min, max);

        return angle;
    }
}
