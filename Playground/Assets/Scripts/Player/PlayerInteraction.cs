using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable targetedObject;

    public void SetTargetedObject(IInteractable newObject)
    {
        if (targetedObject != newObject)
            targetedObject = newObject;
    }

    public void Interact()
    {
        if (targetedObject != null)
            targetedObject.Interact();
    }
}
