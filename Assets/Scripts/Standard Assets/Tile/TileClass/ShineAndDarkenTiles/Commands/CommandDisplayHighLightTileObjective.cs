using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandDisplayHighLightTileObjective : ICommand
{
    private TileRefactor _tileSelected;
    public CommandDisplayHighLightTileObjective(int count)
    {
        var allUnitsObjective = WorldScriptableObjects.GetInstance().tilesWhereUnitCanBeDispose.reference;
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
