using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Manager/SoundManager/Data")]
public class DataSoundManager : SerializedScriptableObject
{
    [SerializeField] Dictionary<MusicNames, AudioClip> musicClips;
    [SerializeField] Dictionary<EffectNames, AudioClip> effectClips;

    public AudioClip GetMusicClip(MusicNames musicName)
    {
        return musicClips[musicName];
    }
    public AudioClip GetEffectClip(EffectNames effectName)
    {
        return effectClips[effectName];
    }
}

public enum MusicNames
{
    Player1Song, Player2Song, EditMapSong, IntroductionSong, WinSong, DefeatSong
}

public enum EffectNames
{
    Error, MoveBetweenGrid, MoveBetweenSubMenus, MoveBetweenMenus,
    Exit, SelectUnit, SelectOption
}
