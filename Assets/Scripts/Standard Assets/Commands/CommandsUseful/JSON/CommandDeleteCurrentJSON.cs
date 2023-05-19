using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CommandDeleteCurrentJSON : ICommand
{
    private string _pathJSONTile;
    private string _pathJSONUnit;
    private string _pathJSONMapData;
    
    
    public void Execute()
    {
        GetJSONReferences();
        RemoveJSON();
        FinishExecute();
    }

    private void GetJSONReferences()
    {
        _pathJSONTile = Application.dataPath + Path.AltDirectorySeparatorChar +
                        "StreamingAssets/JSON/MapsLoad/DesignMap/DataTile/SaveDataTile" + 4 + ".json";
        _pathJSONUnit = Application.dataPath + Path.AltDirectorySeparatorChar +
                        "StreamingAssets/JSON/MapsLoad/DesignMap/DataUnit/SaveDataUnit" + 4 + ".json";
        _pathJSONMapData = Application.dataPath + Path.AltDirectorySeparatorChar +
                           "StreamingAssets/JSON/MapsLoad/DesignMap/DataMap/SaveDataMap" + 4 + ".json";
    }
    private void RemoveJSON()
    {
        File.Delete(_pathJSONTile);
        File.Delete(_pathJSONUnit);
        File.Delete(_pathJSONMapData);
    }
    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
        
}
