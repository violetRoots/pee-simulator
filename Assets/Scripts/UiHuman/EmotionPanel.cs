using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EmotionPanel : MonoBehaviour
{
    [Serializable]
    private class EmotionInfo
    {
        public HumanEmotionController.HumanEmotion emotion;
        public Sprite[] rzhumanSprites;
        public SfxType sfx;
    }

    [SerializeField] private EmotionInfo[] emotionsInfo;

    [SerializeField] private float emotionDuration;

    [SerializeField] private Transform emotionObj;
    [SerializeField] private Image rzhuman;

    private void Awake()
    {
        HideEmotion();
    }

    public void ShowEmotion(HumanEmotionController.HumanEmotion emotion)
    {
        StopAllCoroutines();

        var emotionInfo = emotionsInfo.Where(info => info.emotion == emotion).FirstOrDefault();
        if (emotionInfo == null) return;

        rzhuman.sprite = emotionInfo.rzhumanSprites[UnityEngine.Random.Range(0, emotionInfo.rzhumanSprites.Length)];

        if(emotionInfo.sfx != SfxType.None)
        {
            gameObject.Play3DSound(emotionInfo.sfx);
        }

        emotionObj.gameObject.SetActive(true);

        StartCoroutine(HideEmotionDalayed(emotionDuration));
    }

    private IEnumerator HideEmotionDalayed(float time)
    {
        yield return new WaitForSeconds(time);

        HideEmotion();
    }

    public void HideEmotion()
    {
        emotionObj.gameObject.SetActive(false);
    }
}
