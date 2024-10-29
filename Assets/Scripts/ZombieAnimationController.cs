using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationController : MonoBehaviour
{
    [SerializeField] private string idleStateName = "Idle";
    [SerializeField] private string walkStateName = "Walk";
    [SerializeField] private string attackStateName = "Attack";
    [SerializeField] private string dieStateName = "Die";

    [SerializeField] private Animator _animator;

#if UNITY_EDITOR
    private void OnValidate()
    {
        _animator = GetComponentInChildren<Animator>();
    }
#endif

    public void PlayWalkAnimation()
    {
        _animator.enabled = false;
        _animator.enabled = true;
        _animator.Play(walkStateName);
    }

    public void PlayIdleAnimation()
    {
        _animator.Play(idleStateName);
    }

    public void PlayAttackAnimation()
    {
        _animator.Play(attackStateName);
    }

    public void PlayDieAnimation()
    {
        _animator.Play(dieStateName);
    }
}
