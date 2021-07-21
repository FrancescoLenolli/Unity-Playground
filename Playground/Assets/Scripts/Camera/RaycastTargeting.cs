using System;
using UnityEngine;

public class RaycastTargeting : MonoBehaviour
{
    public float range = 5.0f;
    public Material highlightMaterial;
    public bool debug = false;

    private float offset = 0.1f;
    private Vector3 startPoint;
    private Vector3 direction;
    private TargetingBehaviour targetingBehaviour;
    private Action<IInteractable> onTargeting;

    public Action<IInteractable> OnTargeting { get => onTargeting; set => onTargeting = value; }

    private void Awake()
    {
        targetingBehaviour = new TargetingBehaviour(highlightMaterial);
    }

    private void Update()
    {
        startPoint = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        direction = transform.forward;
        RaycastForward(startPoint, direction);
    }

    private void RaycastForward(Vector3 startPoint, Vector3 direction)
    {
        if (debug)
            Debug.DrawRay(startPoint, direction * (range + offset), Color.red);

        Physics.Raycast(startPoint, direction, out RaycastHit hitInfo, range + offset, 1);

        if (!hitInfo.transform)
        {
            targetingBehaviour.ResetLastTarget();
            targetingBehaviour.FocusOff();

            onTargeting?.Invoke(null);
            return;
        }

        if (!targetingBehaviour.IsNewTarget(hitInfo.transform))
            return;

        targetingBehaviour.FocusOff();
        targetingBehaviour.SetTarget(hitInfo.transform);
        IInteractable interactiveObject = targetingBehaviour.CurrentTarget.GetComponent<IInteractable>();
        targetingBehaviour.FocusOn(interactiveObject);

        onTargeting?.Invoke(interactiveObject);


    }
}
