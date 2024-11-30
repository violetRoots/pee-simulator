using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixamoAnimationFix : MonoBehaviour
{
    [SerializeField] private Transform bone;
    [SerializeField] private Vector3 boneLocalPosition;
    [SerializeField] private string[] animationNames;

    [SerializeField] private Animator animator;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(animator == null)
            animator = GetComponentInParent<Animator>();
    }
#endif

    private void LateUpdate()
    {
        foreach(var animation in animationNames)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(animation))
            {
                bone.localPosition = boneLocalPosition;
            }
        }
    }
}
