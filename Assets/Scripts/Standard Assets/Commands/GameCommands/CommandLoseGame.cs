using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLoseGame : ICommandTask
{
    public static event Action GetUI;

    private float _timeWaitForPlayer = 3;
    private float _currentTime = 0;
    public IEnumerator Execute()
    {
        SoundManager._sharedInstance.PlayMusic(MusicNames.DefeatSong);
        PlayerController.sharedInstance.StopControls();
        InterfacesManager.sharedInstance.CloseGamePlayInterfaces();
        GetUI?.Invoke();
        StartChronometer();

        while (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            yield return null;
        }
        new ExitMapOptionMenu().Trigger();
        FinishExecute();
        yield return null;
    }

    private void StartChronometer() => _currentTime = _timeWaitForPlayer;
    
    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
