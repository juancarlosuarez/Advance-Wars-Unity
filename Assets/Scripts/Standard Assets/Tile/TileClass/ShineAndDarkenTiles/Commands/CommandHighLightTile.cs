using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHighLightTile : ICommand
{
    public void Execute()
    {
        var _instancePutHighLight = new HighLightTilesRefactor();
        var _tilesHighLight = WorldScriptableObjects.GetInstance().tilesWhereUnitCanBeDispose.reference;

        foreach (var tile in _tilesHighLight) _instancePutHighLight.PutHighLightTile(tile, TypeHighLightTile.HighLightAttack);
        
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
