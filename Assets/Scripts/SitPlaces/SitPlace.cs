using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitPlace : MonoBehaviour
{
    [SerializeField] private Transform interactPoint;
    [SerializeField] private Transform sitPoint;

    public Transform GetInteractPoint()
    {
        return interactPoint;
    }

    public Transform GetSitPoint()
    {
        return sitPoint;
    }
}
