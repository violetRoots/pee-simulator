using CMF;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private float normalSpeed = 7.0f;
    [SerializeField] private float runSpeed = 10.0f;
    [SerializeField] private float injuredSpeed = 4.0f;
    
    [SerializeField] private AdvancedWalkerController walker;

    [HideInInspector]
    [SerializeField]
    private CharacterProvider _characterProvider;
    private CharacterStateController _stateController;

    private QuestsManager _questsManager;

    Vector3 _previousPos;

    private IDisposable _stateSubscription;

#if UNITY_EDITOR
    private void OnValidate()
    {
        _characterProvider = GetComponent<CharacterProvider>();
    }
#endif

    private void Awake()
    {
        _questsManager = GameManager.Instance.QuestsManager;

        _stateController = _characterProvider.StateController;

        SetNormalSpeed();

        StartCoroutine(RunQuestProgressUpdate());
    }

    private IEnumerator RunQuestProgressUpdate()
    {
        _previousPos = transform.position;

        while (!_questsManager.IsQuestFinished(QuestConfig.QuestType.Run))
        {
            var charOffset = (transform.position - _previousPos).magnitude;

            _questsManager.ChangeProgressQuest(QuestConfig.QuestType.Run, charOffset);

            _previousPos = transform.position;

            yield return new WaitForSeconds(0.5f);
        }        
    }

    private void OnEnable()
    {
        _stateSubscription = _stateController.state.Subscribe(OnCharacterStateChanged);
    }

    private void OnDisable()
    {
        _stateSubscription?.Dispose();
        _stateSubscription = null;
    }

    private void OnCharacterStateChanged(CharacterStateController.CharacterState newState)
    {
        if(newState == CharacterStateController.CharacterState.Normal)
        {
            SetSpeed(normalSpeed);
        }
        else if(newState == CharacterStateController.CharacterState.Run)
        {
            SetSpeed(runSpeed);
        }
        else if (newState == CharacterStateController.CharacterState.Injured)
        {
            SetSpeed(injuredSpeed);
        }
    }

    public void SetNormalSpeed()
    {
        SetSpeed(normalSpeed);
    }

    public void SetInjuredSpeed()
    {
        SetSpeed(injuredSpeed);
    }

    public void SetSpeed(float speed)
    {
        walker.movementSpeed = speed;
    }
}
