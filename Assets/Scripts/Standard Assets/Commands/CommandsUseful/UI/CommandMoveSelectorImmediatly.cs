using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMoveSelectorImmediatly : ICommand
{
    private int _idNewTile;
    public CommandMoveSelectorImmediatly(int idNewTile)
    {
        _idNewTile = idNewTile;
    }
    
    public static event Action<int> MoveSelectorImmediatly;
    public void Execute()
    {
        MoveSelectorImmediatly?.Invoke(_idNewTile);
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
