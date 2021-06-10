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

        //float maxValue = 50.0f;
        //float minValue = 1.0f;

        transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothness * Time.fixedDeltaTime);
        transform.LookAt(lookAtTarget);

        //float currentVerticalRotation = Mathf.Clamp(transform.rotation.eulerAngles.x, minValue, maxValue);
        //Vector3 newRotation = new Vector3(currentVerticalRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        //transform.rotation = Quaternion.Euler(newRotation);
    }

    public void SetInputValue(Vector3 inputValue)
    {
        this.inputValue = Vector3.Normalize(inputValue);
    }
}
