using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer)),ExecuteInEditMode]
public class RendererId : MonoBehaviour
{
    // Start is called before the first frame update
    public string SortingLayers;
    public int SortingOrderId;
    MeshRenderer m;
    void Start()
    {

    }
    private void OnGUI()
    {
        m = GetComponent<MeshRenderer>();
        m.sortingLayerID = SortingLayer.NameToID(SortingLayers);
        m.sortingOrder = SortingOrderId;
    }
}
