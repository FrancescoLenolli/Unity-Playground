using UnityEngine;

public abstract class TargetingBehaviour : MonoBehaviour
{
    protected Transform target;

    protected abstract void FocusOn();
    protected abstract void FocusOff();

    public IInteractable GetValidTarget(Transform newTarget)
    {
        IInteractable currentInteractiveTarget = target ? target.GetComponent<IInteractable>() : null;
        IInteractable newInteractiveTarget = null;

        if (newTarget)
            newInteractiveTarget = newTarget.GetComponent<IInteractable>();

        if (target && currentInteractiveTarget != null)
            FocusOff();

        SetTarget(newTarget);

        if (target && newInteractiveTarget != null)
            FocusOn();

        return newInteractiveTarget;
    }

    public Transform GetTarget()
    {
        return target;
    }

    public bool IsNewTarget(Transform target)
    {
        return this.target != target;
    }

    private void SetTarget(Transform target)
    {
        this.target = target;
    }
}
