using UnityEngine;

public class TargetingBehaviour : MonoBehaviour
{
    [SerializeField]
    private Material highlightMaterial;

    private Transform target;
    private MeshRenderer targetRenderer;
    private Material defaultMaterial;

    public void FocusOn()
    {
        Debug.Log("Focus on");
        targetRenderer = target.GetComponent<MeshRenderer>();
        defaultMaterial = targetRenderer.material;
        targetRenderer.material = highlightMaterial;
    }

    public void FocusOff()
    {
        if (!target || target.GetComponent<IInteractable>() == null)
            return;

        Debug.Log($"Focus off");
        if (targetRenderer)
        {
            targetRenderer.material = defaultMaterial;
            targetRenderer = null;
        }
    }

    public IInteractable GetValidTarget(Transform newTarget)
    {
        IInteractable interactiveTarget = null;

        if (newTarget)
            interactiveTarget = newTarget.GetComponent<IInteractable>();

        FocusOff();
        SetTarget(newTarget);

        if (target && interactiveTarget != null)
            FocusOn();

        return interactiveTarget;
    }

    public Transform GetTarget()
    {
        return target;
    }

    public void SetTarget(Transform target)
    {
        Debug.Log("Set Target");
        this.target = target;
    }

    public bool IsNewTarget(Transform target)
    {
        return this.target != target;
    }
}
