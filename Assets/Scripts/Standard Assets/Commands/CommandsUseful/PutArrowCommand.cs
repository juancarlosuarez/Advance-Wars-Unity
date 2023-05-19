using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutArrowCommand : ICommand
{
    public void Execute()
    {
        if (!WorldScriptableObjects.GetInstance().hadPointerUnitSelected.boolReference)
        {
            FinishExecute();
            return;
        }

        var tilesWhereICanPutMyArrow = WorldScriptableObjects.GetInstance().tilesPathHighLight.reference;
        var tileWhereIWannaPutMyNewArrow = WorldScriptableObjects.GetInstance().tileSelected.reference;
        
        if (tilesWhereICanPutMyArrow.Contains(tileWhereIWannaPutMyNewArrow)) CalculatePathArrow.GetInstance().GetArrowPath();
        else
        {
            Debug.Log("Arrow Outside Limit");
        }
        
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
