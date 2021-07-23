using System;
using UnityEngine;

public class RaycastTargeting : MonoBehaviour
{
    public TargetingBehaviour targetingBehaviour;
    public float range = 5.0f;
    public bool enableTargeting = false;
    public bool debug = false;

    private float offset = 0.1f;
    private Vector3 startPoint;
    private Vector3 direction;
    private Action<IInteractable> onTargeting;

    public Action<IInteractable> OnTargeting { get => onTargeting; set => onTargeting = value; }

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

        if (targetingBehaviour.IsNewTarget(target))
        {
            onTargeting?.Invoke(targetingBehaviour.GetValidTarget(target));
        }
    }

    private Transform RaycastTarget(Vector3 startPoint, Vector3 direction, float maxDistance, bool debug)
    {
        if (debug)
            Debug.DrawRay(startPoint, direction * maxDistance, Color.red);

        Physics.Raycast(startPoint, direction, out RaycastHit hitInfo, maxDistance, 1);

        return hitInfo.transform;
    }
}
