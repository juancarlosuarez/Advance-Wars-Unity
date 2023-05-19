using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StartNewMapJSON
{
    private string _pathJSONTile;
    private string _pathJSONUnit;
    private string _pathJSONMap;

    private string _pathJSONTileCurrentMap;
    private string _pathJSONUnitCurrentMap;
    private string _pathJSONMapCurrentMap;
    
    private SaveMap.ListDataTile _dataEachTile;
    private SaveMap.ListUnitDataSave _dataEachUnit;
    private SaveMap.DataMap _dataMap;
    
    public void NewMap(int idSlot)
    {
        GetPathJSON(idSlot);
        LoadDataFromTemplateMap();
        RemovePreviousJSON();
        WriteJSONDocuments();
    }

    private void GetPathJSON(int idSlot)
    {
        _pathJSONTile = Application.dataPath + Path.AltDirectorySeparatorChar +
                        "StreamingAssets/JSON/MapsLoad/DesignMap/DataTile/SaveDataTile" + idSlot + ".json";
        _pathJSONUnit = Application.dataPath + Path.AltDirectorySeparatorChar +
                        "StreamingAssets/JSON/MapsLoad/DesignMap/DataUnit/SaveDataUnit" + idSlot + ".json";
        _pathJSONMap = Application.dataPath + Path.AltDirectorySeparatorChar +
                       "StreamingAssets/JSON/MapsLoad/DesignMap/DataMap/SaveDataMap" + idSlot + ".json";
        
        _pathJSONTileCurrentMap = Application.dataPath + Path.AltDirectorySeparatorChar +
                                  "StreamingAssets/JSON/MapsLoad/DesignMap/DataTile/SaveDataTile" + 4 + ".json";
        _pathJSONUnitCurrentMap = Application.dataPath + Path.AltDirectorySeparatorChar +
                                  "StreamingAssets/JSON/MapsLoad/DesignMap/DataUnit/SaveDataUnit" + 4 + ".json";
        _pathJSONMapCurrentMap = Application.dataPath + Path.AltDirectorySeparatorChar +
                                 "StreamingAssets/JSON/MapsLoad/DesignMap/DataMap/SaveDataMap" + 4 + ".json";
    }

    private void LoadDataFromTemplateMap()
    {
        GetDataTileFromJson();
        GetDataUnitFromJson();
        GetSizeMap();
    }

    private void RemovePreviousJSON()
    {
        RemoveJSONTile();
        RemoveJSONUnit();
        RemoveJSONMap();
    }
    private void WriteJSONDocuments()
    {
        WriteJSONTile();
        WriteJSONUnit();
        WriteJSONDataMap();
    }
    private void GetDataTileFromJson()
    {
        using StreamReader reader = new StreamReader(_pathJSONTile);
        
        string json = reader.ReadToEnd();

        _dataEachTile = JsonUtility.FromJson<SaveMap.ListDataTile>(json);
        reader.Close();
    }
    private void GetDataUnitFromJson()
    {
        using StreamReader reader = new StreamReader(_pathJSONUnit);
        
        string json = reader.ReadToEnd();

        _dataEachUnit = JsonUtility.FromJson<SaveMap.ListUnitDataSave>(json);
        reader.Close();
    }

    private void GetSizeMap()
    {
        using StreamReader reader = new StreamReader(_pathJSONMap);
        string json = reader.ReadToEnd();

        _dataMap = JsonUtility.FromJson<SaveMap.DataMap>(json);
        reader.Close();
    }

    private void RemoveJSONTile()
    {
        if (File.Exists(_pathJSONTileCurrentMap)) File.Delete(_pathJSONTileCurrentMap);
    }

    private void RemoveJSONUnit()
    {
        if (File.Exists(_pathJSONUnitCurrentMap)) File.Delete(_pathJSONUnitCurrentMap);
    }

    private void RemoveJSONMap()
    {
        if (File.Exists(_pathJSONUnitCurrentMap)) File.Delete(_pathJSONMapCurrentMap);
    }
    private void WriteJSONTile()
    {
        Debug.Log("Saving Data at " + _pathJSONTileCurrentMap);
        string kson = JsonUtility.ToJson(_dataEachTile);
        
        using StreamWriter writer = new StreamWriter(_pathJSONTileCurrentMap);
        writer.Write(kson);
        writer.Close();
    }
    private void WriteJSONUnit()
    {
        Debug.Log("Saving Data at " + _pathJSONUnitCurrentMap);
        string kson = JsonUtility.ToJson(_dataEachUnit);
        
        using StreamWriter writer = new StreamWriter(_pathJSONUnitCurrentMap);
        writer.Write(kson);
        writer.Close();
    }

    private void WriteJSONDataMap()
    {
        Debug.Log("Saving Data at " + _pathJSONMapCurrentMap);
        string json = JsonUtility.ToJson(_dataMap);

        using StreamWriter writer = new StreamWriter(_pathJSONMapCurrentMap);
        writer.Write(json);
        writer.Close();
    }
}
