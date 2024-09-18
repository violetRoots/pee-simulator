using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeAutomate : MonoBehaviour
{
    [SerializeField] private Vector3 defaultOffset;
 
    [SerializeField] private Material greenPlaceMaterial;
    [SerializeField] private Material redPlaceMaterial;

    [SerializeField] private Collider placeMarker;
    [SerializeField] private BaseAutomatePart[] automateParts;

    private MeshRenderer _placeMarkerRenderer;

    private void Awake()
    {
        _placeMarkerRenderer = placeMarker.GetComponent<MeshRenderer>();
    }

    public void Activate()
    {
        foreach (var part in automateParts)
        {
            part.Activate();
        }

        SetPlaceMarkerActive(false);
    }

    public void SetPlaceMarkerActive(bool isActive)
    {
        placeMarker.gameObject.SetActive(isActive);
    }

    public void SetPlaceMarkerMaterial(bool canPlace)
    {
        _placeMarkerRenderer.material = canPlace ? greenPlaceMaterial : redPlaceMaterial;
    }

    public Vector3 GetOffset()
    {
        return defaultOffset;
    }
}
