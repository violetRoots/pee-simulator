using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimationController : MonoBehaviour
{
    [SerializeField] private string walkStateName;
    [SerializeField] private string idleStateName;
    [SerializeField] private string fightStateName;
    [SerializeField] private string sitStateName;


    [HideInInspector]
    [SerializeField]
    private HumanProvider _humanProvider;
    private HumanContentController _contentController;

    private Animator _animator;
    private bool _isFirstAnimation = true;

#if UNITY_EDITOR
    private void OnValidate()
    {
        _humanProvider = GetComponent<HumanProvider>();
    }
#endif

    private void Awake()
    {
        _contentController = _humanProvider.ContentController;        
    }

    private void OnEnable()
    {
        _contentController.ContentSpawned += InitAnimator;
    }

    private void OnDisable()
    {
        _contentController.ContentSpawned -= InitAnimator;
    }

    private void InitAnimator(HumanContent content)
    {
        _animator = content.GetComponent<Animator>();
    }

    public void PlayWalkAnimation()
    {
        PlayAnimation(walkStateName);
    }

    public void PlayIdleAnimation()
    {
        PlayAnimation(idleStateName);
    }

    public void PlayFightAnimation()
    {
        PlayAnimation(fightStateName);
    }

    public void PlaySitAnimation()
    {
        PlayAnimation(sitStateName);
    }

    private void PlayAnimation(string stateName)
    {
        var skipFrames = _isFirstAnimation ? 1 : 0;

        StartCoroutine(PlayAnimationSkipFrames(stateName, skipFrames));
    }

    private IEnumerator PlayAnimationSkipFrames(string stateName, int skipFrames = 0)
    {
        for(int i = 0; i < skipFrames; i++)
        {
            yield return null;
        }

        _animator.Play(stateName);
    }
}
