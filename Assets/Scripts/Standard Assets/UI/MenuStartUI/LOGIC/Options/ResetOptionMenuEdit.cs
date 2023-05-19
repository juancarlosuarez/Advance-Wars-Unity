using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOptionMenuEdit : IStartOption
{
    public static event Action ResetMap;
    public void Trigger()
    {
        Debug.Log("ResetOption");
        var dataMapReset = WorldScriptableObjects.GetInstance()._resetMapData;
        var sizeX = dataMapReset.width;
        var sizeY = dataMapReset.height;
        var nameMap = dataMapReset.mapName;
        
        GenerateMapWithoutJson._sharedInstance.GenerateMap(true, sizeX, sizeY, nameMap);
        ResetMap?.Invoke();
    }
}
