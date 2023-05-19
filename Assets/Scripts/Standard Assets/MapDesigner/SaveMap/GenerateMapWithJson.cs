using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GenerateMapWithJson : MonoBehaviour
{
    private SaveMap.ListDataTile _dataEachTile;
    private SaveMap.ListUnitDataSave _dataEachUnit;
    private SaveMap.DataMap _dataMap;
    private DataStartMap _dataStartMap;

    private string _pathJSONTile;
    private string _pathJSONUnit;
    private string _pathJSONMapData;
    private string _pathJSONIDMap;

    private int _idMap;

    //All data related with the mesh.
    [SerializeField] private MeshFilter _mainMeshFilter;
    [SerializeField] private MeshFilter _meshFilterHighLight;
    private int vertexIndex = 0;
    private List<Vector3> vertices;
    private List<int> triangles;
    private List<Vector2> uvs;
    
    //All data related with the Tiles
    private int maxID;
    private int countUnits;

    public static event Action StartGamePlay;
    public static event Action StartEditor;
    private void Awake()
    {
        //Obtain Reference From the JSON
        GetIDMapFromJSON();
        
        
        _pathJSONTile = Application.dataPath + Path.AltDirectorySeparatorChar +
                        "/StreamingAssets/JSON/MapsLoad/DesignMap/DataTile/SaveDataTile" + _dataStartMap._id + ".json";
        _pathJSONUnit = Application.dataPath + Path.AltDirectorySeparatorChar +
                        "/StreamingAssets/JSON/MapsLoad/DesignMap/DataUnit/SaveDataUnit" + _dataStartMap._id + ".json";
        _pathJSONMapData = Application.dataPath + Path.AltDirectorySeparatorChar +
                           "/StreamingAssets/JSON/MapsLoad/DesignMap/DataMap/SaveDataMap" + _dataStartMap._id + ".json";

        GenerateMapFromStart();
    }
    //Theres just one map at the same time, for that we will destroy the previous one
    private void GenerateMapFromStart()
    {
        GetDataFromJsonDocuments();
        PrepareValuesFromMap();
        BuildMapMesh();
        BuildMapTiles();
        MoveSelectObjectToRightPosition();
        GetReferenceFirstTileSelected();

        if (_dataStartMap._isGamePlayOn)
        {
            print("StartGameplay");
            StartGamePlay?.Invoke();
            PutMusicGameplay();
        }
        else
        {
            print("StartEdit");
            StartEditor?.Invoke();
            PutMusicEdit();
        }
    }
    private void GetDataFromJsonDocuments()
    {
        GetDataTileFromJson();
        GetDataUnitFromJson();
        GetSizeMap();
    }

    private void GetIDMapFromJSON()
    {
        _pathJSONIDMap = Application.dataPath + Path.AltDirectorySeparatorChar +
                         "StreamingAssets/JSON/MapsLoad/DesignMap/JSONData/IDDocument.json";
        using StreamReader reader = new StreamReader(_pathJSONIDMap);

        string json = reader.ReadToEnd();
        
        _dataStartMap = JsonUtility.FromJson<DataStartMap>(json);
        WorldScriptableObjects.GetInstance().idMap = _dataStartMap;
        
        if (_dataStartMap._isGamePlayOn) _dataStartMap._id = 4;
        else _dataStartMap._id = 2;
        
        WorldScriptableObjects.GetInstance().idMap = _dataStartMap;
        reader.Close();
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
        using StreamReader reader = new StreamReader(_pathJSONMapData);
        string json = reader.ReadToEnd();

        _dataMap = JsonUtility.FromJson<SaveMap.DataMap>(json);
        reader.Close();
    }
    private void PrepareValuesFromMap()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();
        uvs = new List<Vector2>();
        
        maxID = 0;
        countUnits = 0;
        WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference = new Dictionary<int, TileRefactor>();
        CommandQueue.GetInstance.ExecuteCommandImmediately(new CommandResetValuesStatsJSON(), false);
        ResetDataCurrentMap();
    }
    private void BuildMapTiles()
    {
        //ResetStats();
        for (int x = 0; x < _dataMap.xSize; x++)
        {
            for (int y = 0; y < _dataMap.ySize; y++)
            {
                AddDataToTile();
                maxID++;
            }
        }
        AddNeighboursToEachTile();
        ActivateAllUnitsForActions();
        CalculateCornersMap();
    }
    private void AddNeighboursToEachTile() => WorldScriptableObjects.GetInstance().builderTile.AddNeighbourToTile(maxID--);

    private void ActivateAllUnitsForActions()
    {
        var currentPlayer = WorldScriptableObjects.GetInstance().currentPLayer.reference;
        CommandQueue.GetInstance.AddCommand(new CommandPutAvailableAllSoldiers(currentPlayer), false);
    }
    private void BuildMapMesh()
    {
        for (int x = 0; x < _dataMap.xSize; x++)
        {
            for (int y = 0; y < _dataMap.ySize; y++)
            {
                Vector2 position = new Vector2(x, y);
                AddDataToChunk(position);
            }
        }
        CreateMesh();
        CreateMeshHighLightTile();
        SaveCurrentMap();
    }
    private void CreateMesh()
    {
        Mesh mesh = new Mesh
        {
            vertices = vertices.ToArray(),
            triangles = triangles.ToArray(),
            uv = uvs.ToArray()
        };

        mesh.RecalculateNormals();

        WorldScriptableObjects.GetInstance().gridData.vertices = vertices;
        WorldScriptableObjects.GetInstance().gridData.triangles = triangles;
        WorldScriptableObjects.GetInstance().gridData.uvs = uvs;

        WorldScriptableObjects.GetInstance().gridData.meshData = _mainMeshFilter;
        WorldScriptableObjects.GetInstance().gridData.meshData.mesh = mesh;
        
        _mainMeshFilter.mesh = mesh;
    }
    private void CreateMeshHighLightTile()
    {
        Mesh meshHighLightTiles = new Mesh
        {
            vertices = vertices.ToArray(),
            triangles = triangles.ToArray(),
            uv = uvs.ToArray()
        };

        meshHighLightTiles.RecalculateNormals();
        
        WorldScriptableObjects.GetInstance().gridHighLightData.vertices = vertices;
        WorldScriptableObjects.GetInstance().gridHighLightData.triangles = triangles;
        WorldScriptableObjects.GetInstance().gridHighLightData.uvs = uvs;

        WorldScriptableObjects.GetInstance().gridHighLightData.meshData = _meshFilterHighLight;
        WorldScriptableObjects.GetInstance().gridHighLightData.meshData.mesh = meshHighLightTiles;
         
        _meshFilterHighLight.mesh = meshHighLightTiles;
    }
    private void SaveCurrentMap()
    {
        var currentMap = WorldScriptableObjects.GetInstance()._currentMapDisplayData;

        currentMap.width = _dataMap.xSize;
        currentMap.height = _dataMap.ySize;
        currentMap.mapName =  _dataMap.mapName;
        currentMap.currentDays = _dataMap.currentDay;
        currentMap.countPlayerTurn = _dataMap.countPlayerTurn;
    }
    private void AddDataToChunk(Vector2 position)
    {
        for (int i = 0; i < 6; i++)
        {
            int triangleIndex = VoxelDataTest.voxelTris[0, i];
            vertices.Add(VoxelDataTest.voxelVertsCube[triangleIndex ] + position);
            triangles.Add(vertexIndex);
            vertexIndex++;
        }
        AddUV();
    }
    private void AddUV ()
    {
        Vector2 textureCoordinate = new ChangeTextureForTile().TextureCoordinate(0, VoxelDataTest.TextureAtlasSizeInBlocks);
        float x = textureCoordinate.x;
        float y = textureCoordinate.y;

        uvs.Add(new Vector2(x, y));
        uvs.Add(new Vector2(x, y + VoxelDataTest.NormalizedBlockTextureSize));
        uvs.Add(new Vector2(x + VoxelDataTest.NormalizedBlockTextureSize, y));
        uvs.Add(new Vector2(x + VoxelDataTest.NormalizedBlockTextureSize, y));
        uvs.Add(new Vector2(x, y + VoxelDataTest.NormalizedBlockTextureSize));
        uvs.Add(new Vector2(x + VoxelDataTest.NormalizedBlockTextureSize,
            y + VoxelDataTest.NormalizedBlockTextureSize));
    }
    private void AddDataToTile()
    {
        var tileDataSelected = _dataEachTile.data[maxID];
        
        TypesOfTiles typeOfTile = (TypesOfTiles)tileDataSelected.idTilePrefab;
        int idTile = maxID;
        Vector2 positionNewTile = new Vector2(tileDataSelected.xPosition, tileDataSelected.yPosition);

        var newTile = WorldScriptableObjects.GetInstance().builderTile.BuildTile(typeOfTile, idTile, positionNewTile);
        if (tileDataSelected.isSoldierInThisTile)
        {
            MakeUnit(newTile, countUnits);
            countUnits++;
        }

        if (tileDataSelected.isBuildInThisTile)
        {
            MakeUnit(newTile, countUnits);
            countUnits++;
        }
        
    }
    private void MakeUnit(TileRefactor tileWhereUnitShouldBe, int countUnits)
    {
        var dataUnit = _dataEachUnit.unitDataList[countUnits];
        
        var newDataPrefab = new UnitDataPrefab();
        newDataPrefab.factionUnit = (FactionUnit)dataUnit.factionUnit;
        newDataPrefab.nameUnit = (NameUnit)dataUnit.nameUnit;
        var lifeUnit = dataUnit.currentLifeUnit;
        

        if (IsUnitBuild(newDataPrefab.nameUnit))
        {
            var spawnerBuilds = new SpawnerBuilds();
            spawnerBuilds.PutElement(newDataPrefab, tileWhereUnitShouldBe);
        }
        else
        {
            var spawnerUnits = new SpawnerUnitRefactor((int)lifeUnit);
            spawnerUnits.PutElement(newDataPrefab, tileWhereUnitShouldBe);
        }
    }
    private bool IsUnitBuild(NameUnit nameUnit)
    {
        if (nameUnit == NameUnit.City || nameUnit == NameUnit.Base || nameUnit == NameUnit.HQ || nameUnit == NameUnit.Port || nameUnit == NameUnit.Arprt) return true;
        return false;
    }
    private void CalculateCornersMap()
    {
        var currentMap = WorldScriptableObjects.GetInstance()._currentMapDisplayData;
        currentMap.CalculateCorners();
    }

    private void ResetDataCurrentMap()
    {
        var currentMap = WorldScriptableObjects.GetInstance()._currentMapDisplayData;
        currentMap.ResetDataMap();
    }

    private void MoveSelectObjectToRightPosition()
    {
        var currentMapData = WorldScriptableObjects.GetInstance()._currentMapDisplayData;
        var idTileWhereIWannaToPutMyCamera = currentMapData.idTileMainCityEachPlayer[FactionUnit.Player1];
        
        CommandQueue.GetInstance.AddCommand(new CommandMoveSelectorImmediatly(idTileWhereIWannaToPutMyCamera), false);
        CommandQueue.GetInstance.AddCommand(new CommandMoveCameraInmediatly(idTileWhereIWannaToPutMyCamera), false);
    }

    private void GetReferenceFirstTileSelected()
    {
        var allTiles = WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference;
        var currentMapData = WorldScriptableObjects.GetInstance()._currentMapDisplayData;
        var idTileSelected = currentMapData.idTileMainCityEachPlayer[FactionUnit.Player1];

        WorldScriptableObjects.GetInstance().tileSelected.reference = allTiles[idTileSelected];
    }

    private void PutMusicEdit() => SoundManager._sharedInstance.PlayMusic(MusicNames.EditMapSong);
    private void PutMusicGameplay() => SoundManager._sharedInstance.PlayMusic(MusicNames.Player1Song);
}
