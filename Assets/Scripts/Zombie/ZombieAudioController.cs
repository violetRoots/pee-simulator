using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ZombieAudioController : MonoBehaviour
{
    [MinMaxSlider(0, 10)]
    [SerializeField] 
    private Vector2 sfxIntervalBounds;

    [SerializeField] private GameObject sourcePoint;

    private bool _isSoundPlaying = true;

    private void Awake()
    {
        StartCoroutine(AudioProcess());
    }

    private IEnumerator AudioProcess()
    {
        while (_isSoundPlaying)
        {
            var type = Random.value < 0.5f ? SfxType.ZombieMoan1 : SfxType.ZombieMoan2;
            sourcePoint.Play3DSound(type, maxDistance: 50);

            yield return new WaitForSeconds(Random.Range(sfxIntervalBounds.x, sfxIntervalBounds.y));
        }
    }
}
