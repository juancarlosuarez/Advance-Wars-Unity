using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CommandResetValuesStatsJSON : ICommand
{
    private SaveMap.DataMap _dataMap;
    private string _pathJSONMapData;
    public void Execute()
    {
        ResetValues();
        FinishExecute();
    }

    private void ResetValues()
    {
        WorldScriptableObjects.GetInstance().currentPLayer.reference = FactionUnit.Player1;
        var managerStats = WorldScriptableObjects.GetInstance().statsPlayersManager;
        GetDataMapFromDocumentJSON();
        for (int i = 1; i < 5; i++)
        {
            var eachStats = managerStats.GetStatsPlayer((FactionUnit)i);
            var moneyPlayer = GetMoneyFromEachPlayer(i);
            eachStats.ResetStats();
            
            eachStats.SetGoldAmount(moneyPlayer);
            CommandQueue.GetInstance.AddCommand(new CommandUpdateInterfaceGamePlay(), false);
        }
    }

    private void GetDataMapFromDocumentJSON()
    {
        var idMap = WorldScriptableObjects.GetInstance().idMap._id;
        _pathJSONMapData = Application.dataPath + Path.AltDirectorySeparatorChar +
                           "/StreamingAssets/JSON/MapsLoad/DesignMap/DataMap/SaveDataMap" + idMap + ".json";
        
        using StreamReader reader = new StreamReader(_pathJSONMapData);
        string json = reader.ReadToEnd();

        _dataMap = JsonUtility.FromJson<SaveMap.DataMap>(json);
        reader.Close();
    }

    private int GetMoneyFromEachPlayer(int count)
    {
        switch (count)
        {
            case 1:
                return _dataMap.moneyPlayer1;
            case 2:
                return _dataMap.moneyPlayer2;
            default:
                return 5000;
        }
        
    }
    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
