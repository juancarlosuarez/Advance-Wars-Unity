using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSaveMenuEdit : IStartOption
{
    public static event Action SaveMenuEdit;
    public void Trigger()
    {
        var currentMapData = WorldScriptableObjects.GetInstance()._currentMapDisplayData;
        var idMap = 2;
        
        SaveMenuEdit?.Invoke();
        SaveMap._sharedInstance.StartSaveMap(currentMapData, idMap);
    }
}
