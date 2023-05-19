using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;
using System;
using System.Threading.Tasks;
using File = System.IO.File;

public class SaveMap : MonoBehaviour
{
    private MapData _currentMap;
    public static SaveMap _sharedInstance;
    private List<TileRefactor> _allGrid;
    private TileRefactor[] _gridOrganized;
    private TileRefactor[] _gridCopy;
    private List<bool> _isSoldierInThisTile;
    private List<bool> _isBuildInThisTile;
    private List<Soldier> _allSoldiersInGrid;
    private List<Build> _allBuildsInGrid;
    
    private string _pathJSONTile;
    private string _pathJSONUnit;
    private string _pathJSONMap;
    
    public ListDataTile listTileCopy = new ListDataTile();
    public ListUnitDataSave listUnitData = new ListUnitDataSave();
    public DataMap dataMap = new DataMap();

    private OptionStartJustText _slotsUIData;
    private int _idSlot;

    private void Awake()
    {
        _slotsUIData = Resources.Load<OptionStartJustText>("ScriptableObject/UI/OptionsStart/SaveDataElementUI");
        
        _sharedInstance = this;
    }
    public void StartSaveMap(MapData mapSend, int idSlot)
    {
        _idSlot = idSlot;
        
        _pathJSONTile = Application.dataPath + Path.AltDirectorySeparatorChar + "StreamingAssets/JSON/MapsLoad/DesignMap/DataTile/SaveDataTile" + idSlot +".json";
        _pathJSONUnit = Application.dataPath + Path.AltDirectorySeparatorChar + "StreamingAssets/JSON/MapsLoad/DesignMap/DataUnit/SaveDataUnit" + idSlot +".json";
        _pathJSONMap = Application.dataPath + Path.AltDirectorySeparatorChar + "StreamingAssets/JSON/MapsLoad/DesignMap/DataMap/SaveDataMap" + idSlot + ".json";

        _currentMap = mapSend;
        Save();
    }
    private void Save()
    {
        CleanReferences();
        ObtainGrid();
        OrganizeGrid();
        FindSoldierInGrid();
        FindBuildInGrid();
        AssignValuesToMapStorage();
        
        SaveJSON();
    }

    private void SaveJSON()
    {
        DataForTheJson();
        outputJSONTile();
        outputJSONUnit();
        outputJSONDataMap();
    }

    private void CleanReferences()
    {
        listUnitData.unitDataList = new List<UnitDataSave>();
        listTileCopy.data = new List<TileDataSave>();
    }
    private void ObtainGrid()
    {
        _allGrid = WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference.Values.ToList();
    }

    private void OrganizeGrid()
    {
        _gridOrganized = _allGrid.OrderBy(tile => tile.spawnID).ToArray();
    }

    private void FindSoldierInGrid()
    {
        _isSoldierInThisTile = _gridOrganized.Select(TileHasSoldier).ToList();

        _allSoldiersInGrid = _gridOrganized.Where(TileHasSoldier)
            .Select(tilesWithUnits => tilesWithUnits.occupiedSoldier).ToList();
    }
    private bool TileHasSoldier(TileRefactor tileAnalyzed) => tileAnalyzed.occupiedSoldier;
    private void FindBuildInGrid()
    {
        _isBuildInThisTile = _gridOrganized.Select(TileHasBuild).ToList();

        _allSoldiersInGrid = _gridOrganized.Where(TileHasBuild)
            .Select(tilesWithUnits => tilesWithUnits.occupiedSoldier).ToList();
    }

    private bool TileHasBuild(TileRefactor tileAnalyzed) => tileAnalyzed.occupiedBuild; 
    private bool TileHasUnit(TileRefactor tileAnalyzed) => tileAnalyzed.occupiedUnit;

    private void AssignValuesToMapStorage()
    {
        //esto se debe cambiar
        //_currentMap.tilesOfTheGrid = _gridCopy;


    //     var dataOptionStartJustText = _slotsUIData.GetData()[_idSlot];
    //     dataOptionStartJustText.text = _currentMap.mapName;
    //     
    //     FactoryBuildersEachMenuGamePlayUI.sharedInstance.UpdateMenu(NameElementWithFramesGeneric.SaveMenu);
    //     FactoryBuildersEachMenuGamePlayUI.sharedInstance.UpdateMenu(NameElementWithFramesGeneric.LoadMenu);
    }


    private void DataForTheJson()
    {
        for (int i = 0; i < _gridOrganized.Length; i++)
        {
            var tile = _gridOrganized[i];
            var isSoldierInTile = _isSoldierInThisTile[i];
            var isBuildInTile = _isBuildInThisTile[i];
            var thereIsBuildAndSoldier = isSoldierInTile && isBuildInTile;
            
            var tileDataSave = new TileDataSave();
            tileDataSave.idTilePrefab = (int)tile.dataVariable.typeTile;
            tileDataSave.xPosition = tile.positionTileInGrid.x;
            tileDataSave.yPosition = tile.positionTileInGrid.y;
            tileDataSave.isSoldierInThisTile = isSoldierInTile;
            tileDataSave.isBuildInThisTile = isBuildInTile;
            tileDataSave.tileHasBothTypeUnit = thereIsBuildAndSoldier;
            listTileCopy.data.Add(tileDataSave);
            
            if (isSoldierInTile) DataJsonSoldier(tile);
            if (isBuildInTile) DataJsonBuild(tile);
        }
        DataJsonMap();
    }
    
    private void DataJsonMap()
    {
        var currentMap = WorldScriptableObjects.GetInstance()._currentMapDisplayData;
        var currentStatsPlayer1 = WorldScriptableObjects.GetInstance().statsPlayersManager.GetStatsPlayer(FactionUnit.Player1);
        var currentStatsPlayer2 = WorldScriptableObjects.GetInstance().statsPlayersManager.GetStatsPlayer(FactionUnit.Player2);
        
        dataMap.xSize = currentMap.width;
        dataMap.ySize = currentMap.height;
        dataMap.mapName = currentMap.mapName;
        dataMap.moneyPlayer1 = currentStatsPlayer1.GoldAmount();
        dataMap.moneyPlayer2 = currentStatsPlayer2.GoldAmount();
        dataMap.currentDay = currentMap.currentDays;
        dataMap.countPlayerTurn = currentMap.countPlayerTurn;
    }

    private void DataJsonSoldier(TileRefactor tileWithUnit)
    {
        var unitData = new UnitDataSave();

        unitData.nameUnit = (int)tileWithUnit.occupiedUnit.nameUnit;
        unitData.factionUnit = (int)tileWithUnit.occupiedUnit.playerThatCanControlThisUnit;
        unitData.currentLifeUnit = tileWithUnit.occupiedSoldier.currentLife;
        
        listUnitData.unitDataList.Add(unitData);
    }

    private void DataJsonBuild(TileRefactor tileWithBuild)
    {
        var unitData = new UnitDataSave();

        unitData.nameUnit = (int)tileWithBuild.occupiedBuild.nameUnit;
        unitData.factionUnit = (int)tileWithBuild.occupiedBuild.playerThatCanControlThisUnit;
        unitData.currentLifeUnit = tileWithBuild.occupiedBuild.currentLifeUI;
        
        listUnitData.unitDataList.Add(unitData);
    }
    private void outputJSONTile()
    {
        Debug.Log("Saving Data at " + _pathJSONTile);
        string kson = JsonUtility.ToJson(listTileCopy);
        
        using StreamWriter writer = new StreamWriter(_pathJSONTile);
        writer.Write(kson);
        writer.Close();
    }
    private void outputJSONUnit()
    {
        Debug.Log("Saving Data at " + _pathJSONUnit);
        string kson = JsonUtility.ToJson(listUnitData);
        
        using StreamWriter writer = new StreamWriter(_pathJSONUnit);
        writer.Write(kson);
        writer.Close();
    }

    private void outputJSONDataMap()
    {
        Debug.Log("Saving Data at " + _pathJSONMap);
        string json = JsonUtility.ToJson(dataMap);

        using StreamWriter writer = new StreamWriter(_pathJSONMap);
        writer.Write(json);
        writer.Close();

        //string json = JsonUtility.ToJson();
    }
    [Serializable]
    public class TileDataSave
    {
        public int idTilePrefab;
        public float xPosition;
        public float yPosition;
        public bool isSoldierInThisTile;
        public bool isBuildInThisTile;
        public bool tileHasBothTypeUnit;
    }
    [Serializable]
    public class UnitDataSave
    {
        public int nameUnit;
        public int factionUnit;
        public float currentLifeUnit;
    }
    [Serializable]
    public class DataMap
    {
        public int xSize;
        public int ySize;
        public string mapName;
        public int moneyPlayer1;
        public int moneyPlayer2;
        public int currentDay;
        public int countPlayerTurn;
    }
    [Serializable]
    public class ListDataTile
    {
        public List<TileDataSave> data;
    }
    [Serializable]
    public class ListUnitDataSave
    {
        public List<UnitDataSave> unitDataList;
    }
    
}
