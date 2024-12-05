using Common.Localisation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiToiletManTalkPanel : MonoBehaviour
{
    [SerializeField] private float showTimeout = 1.0f;
    [SerializeField] private float showCooldown = 0.2f;

    [SerializeField] private GameObject talkContentContainer;
    [SerializeField] private TranslatedTextMeshPro phraseText;

    private float _lastShowTime;

    private void Awake()
    {
        Hide();
    }

    public bool CanShow()
    {
        return _lastShowTime == default || Time.time - _lastShowTime > showTimeout + showCooldown;
    }

    public void Show(string key)
    {
        StopAllCoroutines();

        phraseText.SetKey(key);

        talkContentContainer.SetActive(true);

        _lastShowTime = Time.time;

        StartCoroutine(ShowProcess());
    }

    private IEnumerator ShowProcess()
    {
        yield return new WaitForSeconds(showTimeout);

        Hide();
    }

    private void Hide()
    {
        talkContentContainer.SetActive(false);
    }
}
