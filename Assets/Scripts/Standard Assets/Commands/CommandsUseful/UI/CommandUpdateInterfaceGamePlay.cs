using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandUpdateInterfaceGamePlay : ICommand
{
    public static event Action UpdateInterfaceGamePlay;
    public void Execute()
    {
        UpdateInterfaceGamePlay?.Invoke();
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
