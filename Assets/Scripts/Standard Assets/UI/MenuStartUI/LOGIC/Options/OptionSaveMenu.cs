using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSaveMenu : IStartOption
{
    public void Trigger()
    {
        var currentMapData = WorldScriptableObjects.GetInstance()._currentMapDisplayData;
        var idMap = WorldScriptableObjects.GetInstance().idMap._id;
        
        SaveMap._sharedInstance.StartSaveMap(currentMapData, idMap);
        PlayerController.sharedInstance.ChangeControlToGamePlay();
    }
}
