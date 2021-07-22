using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log($"Interacting with {transform.name}");
    }
}
