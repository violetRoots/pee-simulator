using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimationController : MonoBehaviour
{
    [SerializeField] private string walkStateName;
    [SerializeField] private string idleStateName;
    [SerializeField] private string sitStateName;

    [SerializeField] private Animator animator;

    public void PlayWalkAnimation()
    {
        animator.Play(walkStateName);
    }

    public void PlayIdleAnimation()
    {
        animator.Play(idleStateName);
    }

    public void PlaySitAnimation()
    {
        animator.Play(sitStateName);
    }
}
