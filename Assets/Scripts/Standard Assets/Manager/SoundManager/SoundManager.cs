using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _sharedInstance;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource effectAudioSource;

    [SerializeField] private DataSoundManager dataSound;
    public void PlayEffectSound(EffectNames effectName)
    {
        var clipToPlay = dataSound.GetEffectClip(effectName);
        effectAudioSource.PlayOneShot(clipToPlay);
    }

    public void PlayMusic(MusicNames musicName)
    {
        musicAudioSource.Stop();
        var clipToPlay = dataSound.GetMusicClip(musicName);
        musicAudioSource.PlayOneShot(clipToPlay);
    }

    public void StopPlayMusic() => musicAudioSource.mute = !musicAudioSource.mute;
    private void Awake()
    {
        if (_sharedInstance == null)
        {
            _sharedInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
