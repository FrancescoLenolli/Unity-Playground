using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{
    public Material baseMaterial = null;
    public Material highlightedMaterial = null;

    public void Interact()
    {
        Debug.Log($"Interacting with {transform.name}");
    }

    public void FocusOff()
    {
        GetComponent<MeshRenderer>().material = baseMaterial;
    }

    public void FocusOn()
    {
        GetComponent<MeshRenderer>().material = highlightedMaterial;
    }
}
