using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandTest : ICommand
{
    public void Execute()
    {
        Debug.Log("PRUEBAS XDDDDDDDDDDDD MATAME POR FA");
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
