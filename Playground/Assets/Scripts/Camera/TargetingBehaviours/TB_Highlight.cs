using UnityEngine;

public class TB_Highlight : TargetingBehaviour
{
    [SerializeField]
    private Material highlightMaterial;

    private MeshRenderer targetRenderer;
    private Material defaultMaterial;

    protected override void FocusOn()
    {
        targetRenderer = target.GetComponent<MeshRenderer>();
        defaultMaterial = targetRenderer.material;
        targetRenderer.material = highlightMaterial;
    }

    protected override void FocusOff()
    {
        if (targetRenderer)
        {
            targetRenderer.material = defaultMaterial;
            targetRenderer = null;
        }
    }
}
