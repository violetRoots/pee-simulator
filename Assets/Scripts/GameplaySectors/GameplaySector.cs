using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySector : MonoBehaviour
{
    public BoilerSystem BoilerSystem => boilerSystem;
    [SerializeField] private BoilerSystem boilerSystem;

    public PeeAutomateSystem AutomateSystem => automateSystem;
    [SerializeField] private PeeAutomateSystem automateSystem;

    [SerializeField] private Collider sectorCollider;

    public bool Contains(Vector3 pos)
    {
        return sectorCollider.bounds.Contains(pos);
    }
}
