using System;
using UnityEngine;

public class RaycastTargeting : MonoBehaviour
{
    public float range = 5.0f;
    public Material highlightMaterial;
    public bool enableTargeting = false;
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
        if (!enableTargeting)
            return;

        startPoint = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        direction = transform.forward;
        FindTarget(startPoint, direction);
    }

    private void FindTarget(Vector3 startPoint, Vector3 direction)
    {
        Transform target = RaycastTarget(startPoint, direction, range + offset, debug);

        if (!target)
        {
            targetingBehaviour.ResetLastTarget();
            targetingBehaviour.FocusOff();

            onTargeting?.Invoke(null);
            return;
        }

        if (!targetingBehaviour.IsNewTarget(target))
            return;

        targetingBehaviour.FocusOff();
        targetingBehaviour.SetTarget(target);
        IInteractable interactiveObject = targetingBehaviour.CurrentTarget.GetComponent<IInteractable>();
        targetingBehaviour.FocusOn(interactiveObject);

        onTargeting?.Invoke(interactiveObject);


    }

    private Transform RaycastTarget(Vector3 startPoint, Vector3 direction, float maxDistance, bool debug)
    {
        if (debug)
            Debug.DrawRay(startPoint, direction * maxDistance, Color.red);

        Physics.Raycast(startPoint, direction, out RaycastHit hitInfo, maxDistance, 1);

        return hitInfo.transform;
    }
}
