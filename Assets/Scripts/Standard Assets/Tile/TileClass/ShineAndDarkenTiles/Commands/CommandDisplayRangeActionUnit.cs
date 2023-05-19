using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandDisplayRangeActionUnit : ICommand
{
    public void Execute()
    {
        var calculatorUnitRangeAction = new CalculatorRangeUnitUI();
        var instancePutHighLight = new HighLightTilesRefactor();
        var highlightTiles = calculatorUnitRangeAction.Calculate();
        WorldScriptableObjects.GetInstance().tilesRangeAction.reference = highlightTiles.ToList();

        foreach (var tile in highlightTiles)
        {
            instancePutHighLight.PutHighLightTile(tile, TypeHighLightTile.HighLightActionRange);
        }
        
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
