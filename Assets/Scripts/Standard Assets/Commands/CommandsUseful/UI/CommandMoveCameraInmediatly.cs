using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMoveCameraInmediatly : ICommand
{
    public static event Action<int> SendIDToManagerCamera;
    
    private int _idNewTilePosition;
    public CommandMoveCameraInmediatly(int idNewTilePosition)
    {
        _idNewTilePosition = idNewTilePosition;
    }
    public void Execute()
    {
        SendIDToManagerCamera?.Invoke(_idNewTilePosition);
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
