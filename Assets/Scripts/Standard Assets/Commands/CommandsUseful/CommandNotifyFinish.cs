using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandNotifyFinish : ICommand
{
    public static event Action FinishNotification;
    //This command is very dangerous, always had a control about this event, if not probably will be a mess.
    public void Execute()
    {
        FinishExecute();
    }

    public void FinishExecute()
    {
        FinishNotification?.Invoke();
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
