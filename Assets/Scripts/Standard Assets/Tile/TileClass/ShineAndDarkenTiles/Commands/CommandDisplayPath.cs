using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CommandDisplayPath : ICommand
{
    private bool _needToUpdatePathHighLight;

    public CommandDisplayPath(bool needToUpdatePathHighLight)
    {
        _needToUpdatePathHighLight = needToUpdatePathHighLight;
    }
    public void Execute()
    {
        var _instancePutHighLight = new HighLightTilesRefactor();
        //var instanceRangeAvailable = new RangeAvailableForWalk();
        var instanceRangeAvailable = new WalkHighLight();
        var tileSelected = WorldScriptableObjects.GetInstance().tileSelected.reference;
        var currentPlayer = WorldScriptableObjects.GetInstance().currentPLayer.reference;
        
        if (_needToUpdatePathHighLight) WorldScriptableObjects.GetInstance().tilesPathHighLight.reference = instanceRangeAvailable.CalculateHighLightTilesForPath(tileSelected, currentPlayer);

        var tilesHighLight = WorldScriptableObjects.GetInstance().tilesPathHighLight.reference;
        foreach (var tile in tilesHighLight)
        {
            _instancePutHighLight.PutHighLightTile(tile, TypeHighLightTile.HighLightPath);
        }
        
        FinishExecute();
    }
    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
        Debug.Log("Finish Execute Display Path");
    }
}
