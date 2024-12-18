using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioUtility
{
    public static void Play3DSound(this GameObject gameObject, SfxType sfxType, float volume = 1.0f, bool isLoop = false, float maxDistance = 10.0f)
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        
        if(audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1.0f;
            audioSource.volume = volume;
            audioSource.maxDistance = maxDistance;
            audioSource.loop = isLoop;
        }

        AudioManager.StaticPlaySound(sfxType, audioSource);
    }

    public static void Stop3DSound(this GameObject gameObject)
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();

        if (audioSource == null) return;
        audioSource.Stop();
    }
}
