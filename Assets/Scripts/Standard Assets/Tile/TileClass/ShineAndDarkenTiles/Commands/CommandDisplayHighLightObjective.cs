using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandDisplayHighLightObjective : ICommand
{
    private TileRefactor _tileSelected;
    public CommandDisplayHighLightObjective(int count)
    {
        var allUnitsObjective = WorldScriptableObjects.GetInstance().currentTilesWhereEnemyAre.reference;
        _tileSelected = allUnitsObjective[count];
    }
    public void Execute()
    {
        var _instancePutHighLight = new HighLightTilesRefactor();
        _instancePutHighLight.PutHighLightTile(_tileSelected, TypeHighLightTile.HighLightPath);
        
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
