using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_Raycast : TargetingMethod
{
    public float range = 5.0f;
    public bool debug = false;

    private float offset = 0.1f;
    private Vector3 startPoint;
    private Vector3 direction;

    public override Transform GetTarget()
    {
        startPoint = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        direction = transform.forward;

        return RaycastTarget(startPoint, direction, range + offset, debug);
    }

    private Transform RaycastTarget(Vector3 startPoint, Vector3 direction, float maxDistance, bool debug)
    {
        if (debug)
            Debug.DrawRay(startPoint, direction * maxDistance, Color.red);

        Physics.Raycast(startPoint, direction, out RaycastHit hitInfo, maxDistance, 1);

        return hitInfo.transform;
    }
}

