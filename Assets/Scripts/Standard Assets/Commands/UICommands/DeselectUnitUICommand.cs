using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeselectUnitUICommand : ICommand
{
    private HighLightTilesRefactor _instancePutHighLight;
    public DeselectUnitUICommand()
    {
        _instancePutHighLight = new HighLightTilesRefactor();
    }
    public void Execute()
    {
        if (WorldScriptableObjects.GetInstance().hadPointerUnitSelected.boolReference is false) return;
        CommandQueue.GetInstance.AddCommand(new ResetHighLightTilesCommand(WorldScriptableObjects.GetInstance().tilesPathHighLight.reference), false);
        //_instancePutHighLight.ResetHighLightTiles(WorldScriptableObjects.GetInstance().tilesPathHighLight.reference);
        CalculatePathArrow.GetInstance().StopShowArrows();
        CommandQueue.GetInstance.ExecuteCommandImmediately(new DesactiveSelectCommand(), false);
        
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
