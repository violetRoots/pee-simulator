using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class AudioManager : SingletonFromResourcesBase<AudioManager>
{
    [Serializable]
    public class SfxInfo
    {
        public SfxType type;
        public AudioClip clip;
    }

    [SerializeField] private AudioClip[] calmMusics;
    [SerializeField] private AudioClip[] actionMusics;
    [SerializeField] private SfxInfo[] sfxInfos;

    [Space]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private MusicState _musicState;

    private bool _isMusicEnabled = true;

    private void Awake()
    {
        
    }

    public void PlayCalmMusic()
    {
        if (_musicState == MusicState.Calm) return;

        StopPlayMusic();
        _musicState = MusicState.Calm;
        StartCoroutine(PlayListProgress(calmMusics));
    }

    public void PlayActionMusic()
    {
        if (_musicState == MusicState.Action) return;

        StopPlayMusic();
        _musicState = MusicState.Action;
        StartCoroutine(PlayListProgress(actionMusics));
    }

    private void StopPlayMusic()
    {
        _musicState = MusicState.None;
        StopCoroutine(nameof(PlayListProgress));
        musicSource.Stop();
    }

    private IEnumerator PlayListProgress(AudioClip[] clips)
    {
        while (_isMusicEnabled)
        {
            var clip = clips[UnityEngine.Random.Range(0, clips.Length)];
            musicSource.clip = clip;
            musicSource.Play();

            yield return new WaitForSecondsRealtime(clip.length);
        }
    }

    public void PlaySound(SfxType sfxType, float volume = 0.1f)
    {
        PlaySound(sfxType, sfxSource, volume);
    }

    public void PlaySound(SfxType sfxType, AudioSource source, float volume = 0.1f)
    {
        var sfxInfo = sfxInfos.Where(info => info.type == sfxType).FirstOrDefault();

        if (sfxInfo == null) return;

        source.Stop();
        source.volume = volume;
        source.clip = sfxInfo.clip;
        source.Play();
    }

    public static void StaticPlaySound(SfxType sfxType, float volume = 0.1f)
    {
        AudioManager.Instance.PlaySound(sfxType, volume);
    }

    public static void StaticPlaySound(SfxType sfxType, AudioSource source)
    {
        AudioManager.Instance.PlaySound(sfxType, source);
    }
}

public enum MusicState
{
    None = 0,
    Calm = 1,
    Action = 2
}

public enum SfxType
{
    None = 0,
    BottleLoad = 1,
    BottleFail = 2,
    Cats = 3,
    ItemBought = 4,
    NewCustomer = 5,
    NewTask = 6,
    PeopleFight = 7,
    PissLoop1 = 8,
    PissLoop2 = 9,
    Satisfaction = 10,
    Satisfied = 11,
    SkibidiEnter = 12,
    DoorOpened = 13,
    TaskComplete = 14,
    WeaponChange = 15,
    ZombieExplosion = 16,
    ZombieMoan1 = 17,
    ZombieMoan2 = 18,
}
