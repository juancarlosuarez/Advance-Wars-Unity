using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommandChangeScene : ICommand
{
    private string _nameScene;
    public static event Action ChangeScene;
    public CommandChangeScene(string nameScene)
    {
        _nameScene = nameScene;
    }
    public void Execute()
    {
        ChangeScene?.Invoke();
        SceneManager.LoadScene(_nameScene);
        FinishExecute();
    }

    public void FinishExecute()
    {
        Debug.Log("Change Scene to " + _nameScene);
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
