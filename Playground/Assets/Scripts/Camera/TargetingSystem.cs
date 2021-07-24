using System;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public bool enableTargeting = false;

    private TargetingBehaviour targetingBehaviour;
    private TargetingMethod targetingMethod;
    private Transform target;
    private Action<IInteractable> onTargeting;

    public Action<IInteractable> OnTargeting { get => onTargeting; set => onTargeting = value; }

    private void Awake()
    {
        targetingBehaviour = GetComponent<TargetingBehaviour>();
        targetingMethod = GetComponent<TargetingMethod>();

        if (!targetingBehaviour)
            Debug.LogError($"Targeting Behaviour missing in {gameObject.name}!");
        if(!targetingMethod)
            Debug.LogError($"Targeting Method missing in {gameObject.name}!");
    }

    private void Update()
    {
        FindTarget();
    }

    private void FindTarget()
    {
        if (!targetingBehaviour || !targetingMethod)
            return;

        target = targetingMethod.GetTarget();

        if (targetingBehaviour.IsNewTarget(target))
        {
            onTargeting?.Invoke(targetingBehaviour.GetValidTarget(target));
        }
    }
}
