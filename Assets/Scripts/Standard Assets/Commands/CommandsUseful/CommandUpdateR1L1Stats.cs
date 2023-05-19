using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandUpdateR1L1Stats : ICommand
{
    public static event Action Update;
    public void Execute()
    {
        Update?.Invoke();
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
