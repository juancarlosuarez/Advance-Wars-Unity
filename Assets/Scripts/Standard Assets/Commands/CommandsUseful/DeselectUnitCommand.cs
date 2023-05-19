using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeselectUnitCommand : ICommand
{
    public void Execute()
    {
        if (WorldScriptableObjects.GetInstance().hadPointerUnitSelected.boolReference is false) return;

        WorldScriptableObjects.GetInstance().hadPointerUnitSelected.boolReference = false;
        WorldScriptableObjects.GetInstance().tilesPathHighLight.reference = new List<TileRefactor>();
        
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
