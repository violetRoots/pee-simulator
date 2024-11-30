using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeAutomateSystem : MonoBehaviour
{
    [SerializeField] private Transform automatesContainer;

    public void AddAutomate(PeeAutomate automate)
    {
        automate.transform.SetParent(automatesContainer, true);
    }
}
