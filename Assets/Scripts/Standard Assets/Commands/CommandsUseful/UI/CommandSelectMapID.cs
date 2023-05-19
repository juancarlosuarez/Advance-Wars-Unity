using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CommandSelectMapID : ICommand
{
    private readonly string _path = Application.dataPath + Path.AltDirectorySeparatorChar +
                                    "StreamingAssets/JSON/MapsLoad/DesignMap/JSONData/IDDocument.json";
    private DataStartMap _dataStartMap;

    public CommandSelectMapID(DataStartMap dataStartMap)
    {
        _dataStartMap = dataStartMap;
    }
    public void Execute()
    {
        WriteJSONDocument();
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
    private void WriteJSONDocument()
    {
        Debug.Log("Saving Data at " + _path);
        string json = JsonUtility.ToJson(_dataStartMap);
        
        using StreamWriter writer = new StreamWriter(_path);
        writer.Write(json);
        writer.Close();
    }
}

public class DataStartMap
{
    public int _id;
    public bool _isGamePlayOn;
    public DataStartMap(int id, bool isGamePlayOn)
    {
        _id = id;
        _isGamePlayOn = isGamePlayOn;
    }
    
}