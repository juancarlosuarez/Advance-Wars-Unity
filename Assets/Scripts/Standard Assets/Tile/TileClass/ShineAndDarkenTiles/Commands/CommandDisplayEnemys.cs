using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandDisplayEnemys : ICommand
{
    public void Execute()
    {
        var _instancePutHighLight = new HighLightTilesRefactor();
        var _unitsHighLight = WorldScriptableObjects.GetInstance().currentTilesWhereEnemyAre.reference;

        foreach (var tile in _unitsHighLight) _instancePutHighLight.PutHighLightTile(tile, TypeHighLightTile.HighLightAttack);
        
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
