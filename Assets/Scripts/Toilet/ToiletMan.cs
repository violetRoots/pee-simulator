using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletMan : MonoBehaviour
{
    [Header("Head Movement")]
    [SerializeField] private Vector3 characterPosOffset = Vector3.up;
    [SerializeField] private Vector3 boneRotationOffset;
    [SerializeField] private Transform headBone;

    [Header("Talk")]
    [SerializeField] private UiToiletManTalkPanel talkPanel;
    [SerializeField] private string[] phraseKeys;

    private CharacterProvider _characterProvider;

    private void Awake()
    {
        _characterProvider = GameManager.Instance.CharacterProvider;
    }

    private void Update()
    {
        var charPos = _characterProvider.transform.position + characterPosOffset - transform.position;
        headBone.rotation = Quaternion.LookRotation(charPos) * Quaternion.Euler(boneRotationOffset);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.TryGetComponent(out PeeBox peeBox)) return;

        if (!talkPanel.CanShow()) return;

        var randomPhraseKey = phraseKeys[Random.Range(0, phraseKeys.Length)];
        talkPanel.Show(randomPhraseKey);

    }
}
