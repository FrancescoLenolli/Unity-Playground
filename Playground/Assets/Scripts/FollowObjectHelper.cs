using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FollowObjectHelper : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;

    private void Awake()
    {
        target = GetComponent<FollowObject>().target;
    }

    private void OnEnable()
    {
        if (target)
            offset = transform.position - target.position;
    }

    private void Update()
    {
        if(target)
        transform.position = target.position + offset;
    }
}
