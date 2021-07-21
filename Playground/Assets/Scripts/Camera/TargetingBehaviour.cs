using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingBehaviour
{
    private Material highlightMaterial;
    private Transform lastTarget;
    private Transform currentTarget;
    private MeshRenderer targetRenderer;
    private Material defaultMaterial;

    public Transform CurrentTarget { get => currentTarget; }

    public TargetingBehaviour(Material highlightMaterial)
    {
        this.highlightMaterial = highlightMaterial;
    }

    public void FocusOn(IInteractable interactiveObject)
    {
        if (interactiveObject != null)
        {
            targetRenderer = currentTarget.GetComponent<MeshRenderer>();
            defaultMaterial = targetRenderer.material;
            targetRenderer.material = highlightMaterial;
        }
    }

    public void FocusOff()
    {
        if (targetRenderer)
        {
            targetRenderer.material = defaultMaterial;
            targetRenderer = null;
        }

        currentTarget = null;
    }

    public void SetTarget(Transform target)
    {
        lastTarget = currentTarget;
        currentTarget = target;
    }

    public void ResetLastTarget()
    {
        lastTarget = null;
    }

    public bool IsNewTarget(Transform target)
    {
        return lastTarget != target;
    }
}
