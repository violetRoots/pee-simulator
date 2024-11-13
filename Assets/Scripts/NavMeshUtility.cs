using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public static class NavMeshUtility
{
    private const float Range = 0.25f;
    private const float AdditionalRange = 0.1f;

    public static bool GetNavMeshPoint(Vector3 position, out Vector3 result)
    {
        return GetNavMeshPointReflection(position, Range, out result);
    }

    private static bool GetNavMeshPointReflection(Vector3 position, float range, out Vector3 result)
    {
        if (NavMesh.SamplePosition(position, out NavMeshHit hit, range, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        else
        {
            return GetNavMeshPointReflection(position, range + AdditionalRange, out result);
        }
    }
}
