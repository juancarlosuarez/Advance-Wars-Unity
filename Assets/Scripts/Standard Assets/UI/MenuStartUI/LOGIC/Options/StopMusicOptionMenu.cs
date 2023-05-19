using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusicOptionMenu : IStartOption
{
    public void Trigger()
    {
        SoundManager._sharedInstance.StopPlayMusic();
    }
}
