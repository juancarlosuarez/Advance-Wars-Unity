using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateMapWithoutJson : MonoBehaviour
{
    [SerializeField] private MeshFilter _mainMeshFilter;
    [SerializeField] private MeshFilter _meshFilterHighLight;
    private int vertexIndex = 0;
    private List<Vector3> vertices;
    private List<int> triangles;
    private List<Vector2> uvs;

    //All data related with the Tiles
    private int maxID;

    //Data related with the map
    private int _sizeX;
    private int _sizeY;
    private string _nameMap;

    public static GenerateMapWithoutJson _sharedInstance;


    private void Awake()
    {
        _sharedInstance = this;
    }

    private void Start()
    {
        GenerateMap(false, VoxelDataTest.xSize, VoxelDataTest.ySize, "Plain");
    }

    public void GenerateMap(bool isMapReset, int sizeX, int sizeY, string nameMap)
    {
        if (isMapReset) ResetValuesFromPreviousMap();
        PrepareValuesFromMap(sizeX, sizeY, nameMap);
        BuildMapMesh();
        BuildMapTiles();
        MoveSelectObjectToRightPosition();
        MakeBases();
    }

    private void MoveSelectObjectToRightPosition()
    {
        var idTileWhereIWannaToPutMyCamera = 21;
        CommandQueue.GetInstance.AddCommand(new CommandMoveSelectorImmediatly(idTileWhereIWannaToPutMyCamera), false);
        CommandQueue.GetInstance.AddCommand(new CommandMoveCameraInmediatly(idTileWhereIWannaToPutMyCamera), false);
    }

    private void ResetValuesFromPreviousMap()
    {
        var allTiles = WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference.Values;

        foreach (var tile in allTiles)
        {
            if (tile.occupiedUnit != null) Destroy(tile.occupiedUnit.gameObject);
        }
    }

    private void BuildMapMesh()
    {
        for (int x = 0; x < _sizeX; x++)
        {
            for (int y = 0; y < _sizeY; y++)
            {
                Vector2 position = new Vector2(x, y);
                AddDataToChunk(position);
            }
        }

        CreateMesh();
        CreateMeshHighLightTile();
        SaveCurrentMapData();
    }

    private void SaveCurrentMapData()
    {
        var currentMap = WorldScriptableObjects.GetInstance()._currentMapDisplayData;

        currentMap.width = _sizeX;
        currentMap.height = _sizeY;
        currentMap.mapName = _nameMap;
    }

    private void BuildMapTiles()
    {
        for (int x = 0; x < _sizeX; x++)
        {
            for (int y = 0; y < _sizeY; y++)
            {
                Vector2 position = new Vector2(x, y);
                AddDataToTile(position);
                maxID++;
            }
        }

        AddNeighboursToEachTile();
        CalculateCornersMap();
    }


    private void AddDataToTile(Vector2 position)
    {
        var newTileData = WorldScriptableObjects.GetInstance().builderTile
            .BuildTile(TypesOfTiles.Plain, maxID, position);
        if (position == new Vector2(0, 0)) WorldScriptableObjects.GetInstance().tileSelected.reference = newTileData;
    }

    private void AddNeighboursToEachTile() =>
        WorldScriptableObjects.GetInstance().builderTile.AddNeighbourToTile(maxID--);

    private void AddDataToChunk(Vector2 position)
    {
        for (int i = 0; i < 6; i++)
        {
            int triangleIndex = VoxelDataTest.voxelTris[0, i];
            vertices.Add(VoxelDataTest.voxelVertsCube[triangleIndex] + position);
            triangles.Add(vertexIndex);
            vertexIndex++;
        }

        AddUV();
    }

    private void AddUV()
    {
        Vector2 textureCoordinate =
            new ChangeTextureForTile().TextureCoordinate(0, VoxelDataTest.TextureAtlasSizeInBlocks);
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

    private void PrepareValuesFromMap(int sizeX, int sizeY, string nameMap)
    {
        _sizeX = sizeX;
        _sizeY = sizeY;
        _nameMap = nameMap;

        vertices = new List<Vector3>();
        triangles = new List<int>();
        uvs = new List<Vector2>();
        vertexIndex = 0;

        maxID = 0;
        WorldScriptableObjects.GetInstance().maxVoxelID.reference = 0;
        WorldScriptableObjects.GetInstance().currentVoxelID.reference = 0;
        WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference = new Dictionary<int, TileRefactor>();
        CommandQueue.GetInstance.ExecuteCommandImmediately(new CommandResetValuesStatsJSON(), false);
        ResetDataCurrentMap();
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

    private void MakeBases()
    {
        var allTiles = WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference;
        
        var basePlayer1 = new UnitDataPrefab();
        var tilesPlayer1 = allTiles[45];
        basePlayer1.factionUnit = FactionUnit.Player1;
        basePlayer1.nameUnit = NameUnit.Base;

        var basePlayer2 = new UnitDataPrefab();
        var tilesPlayer2 = allTiles[175];
        basePlayer2.factionUnit = FactionUnit.Player2;
        basePlayer2.nameUnit = NameUnit.Base;
        
        var spawnerBuilds = new SpawnerBuilds();
        spawnerBuilds.PutElement(basePlayer1, tilesPlayer1);
        spawnerBuilds.PutElement(basePlayer2, tilesPlayer2);
    }
}
