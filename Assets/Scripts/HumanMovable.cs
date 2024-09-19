using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMovable : MonoBehaviour
{
    [Serializable]
    public class WayPointInfo
    {
        public Transform point;
        public float standTime = 1.0f;
    }

    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private WayPointInfo[] wayPoints;

    [Space]
    [SerializeField] private AnimationClip idleAnimationClip;
    [SerializeField] private AnimationClip walkAnimationClip;
    [SerializeField] private Animation humanAnimation;

    [Space]
    [SerializeField] private Transform target;

    private int _targetWayPointIndex;

    private void Start()
    {
        if (wayPoints.Length > 0)
        {
            StartCoroutine(MoveToTarget(wayPoints[0]));
        }
    }

    private IEnumerator MoveToTarget(WayPointInfo wayPointInfo)
    {
        var newRotation = target.rotation.eulerAngles;
        newRotation.y = Quaternion.LookRotation(wayPointInfo.point.position - transform.position).eulerAngles.y;
        target.rotation = Quaternion.Euler(newRotation);

        humanAnimation.clip = walkAnimationClip;

        while (Vector3.Distance(target.position, wayPointInfo.point.position) > 0.01f)
        {
            target.position = Vector3.MoveTowards(transform.position, wayPointInfo.point.position, moveSpeed * Time.deltaTime);

            yield return null;
        }

        humanAnimation.clip = idleAnimationClip;

        yield return new WaitForSeconds(wayPointInfo.standTime);

        StartCoroutine(MoveToTarget(GetTargetWayPointInfo()));
    }

    private WayPointInfo GetTargetWayPointInfo()
    {
        _targetWayPointIndex = (int)Mathf.Repeat(_targetWayPointIndex + 1, wayPoints.Length);

        return wayPoints[_targetWayPointIndex];
    }
}
