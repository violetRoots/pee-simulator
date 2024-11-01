using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

public class HumanEmotionController : MonoBehaviour
{
    public enum HumanEmotion
    {
        None = 0,
        Happy = 1,
        Sad = 2,
        Angry = 3,
    }

    [SerializeField] private EmotionPanel emotionPanel;

    [HideInInspector]
    [SerializeField] 
    private HumanProvider _humanProvider;

    private HumanStateController _stateController;
    private HumanPeeController _peeController;

    private IDisposable _stateSubscription;
    private IDisposable _peeSubscription;

#if UNITY_EDITOR
    private void OnValidate()
    {
        _humanProvider = GetComponent<HumanProvider>();
    }
#endif

    private void Awake()
    {
        _stateController = _humanProvider.StateController;
        _peeController = _humanProvider.PeeController;
    }

    private void OnEnable()
    {
        _stateSubscription = _stateController.state.Subscribe(OnHumanStateChanged);
        _peeSubscription = _peeController.state.Skip(1).Subscribe(OnPeeStateChanged);
    }

    private void OnDisable()
    {
        _stateSubscription?.Dispose();
        _stateSubscription = null;

        _peeSubscription?.Dispose();
        _peeSubscription = null;
    }

    private void OnHumanStateChanged(HumanStateController.HumanState newState)
    {

    }

    private void OnPeeStateChanged(HumanPeeController.PeeState newState)
    {
        Debug.Log(newState.emotion);

        emotionPanel.ShowEmotion(newState.emotion);
    }
}
