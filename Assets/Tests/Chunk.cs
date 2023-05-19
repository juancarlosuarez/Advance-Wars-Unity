using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshFilter meshFilterHighLight;

    int vertexIndex = 0;
    private List<Vector3> vertices;
    private List<int> triangles;
    private List<Vector2> uvs;

    private int maxID = 0;
    private void Start()
    {
        CreateMap(VoxelDataTest.xSize, VoxelDataTest.ySize);
        //new GenerateMap().BuildMap();
    }

    public void CreateMap(int xSize, int ySize)
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();
        uvs = new List<Vector2>();
        maxID = 0;
        WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference = new Dictionary<int, TileRefactor>();
        
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                Vector2 position = new Vector2(x, y);
                AddDataToChunk(position, 1);
                AddDataToTile(position);
            }
        }
        AddNeighboursToEachTile();
        CreateMesh();
        CreateMeshHighLightTile();
    }
    private void AddDataToTile(Vector2 position)
    {
        var newTileData = WorldScriptableObjects.GetInstance().builderTile.BuildTile(TypesOfTiles.Plain, maxID, position);
        maxID++;
        //esto funciona bien al menos la posicion se registra bien, si ya has solucionado el bug borra este comentario
        if (position == new Vector2(0, 0)) WorldScriptableObjects.GetInstance().tileSelected.reference = newTileData;
    }

    private void AddNeighboursToEachTile()
    {
        WorldScriptableObjects.GetInstance().builderTile.AddNeighbourToTile(maxID--);
    }
    private void AddDataToChunk(Vector2 pos, int textureID)
    {
        for (int i = 0; i < 6; i++)
        {
            int triangleIndex = VoxelDataTest.voxelTris[0, i];
            vertices.Add(VoxelDataTest.voxelVertsCube[triangleIndex ] + pos);
            triangles.Add(vertexIndex);
            vertexIndex++;
        }

        AddTexture(0);
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

        WorldScriptableObjects.GetInstance().gridData.meshData = meshFilter;
        WorldScriptableObjects.GetInstance().gridData.meshData.mesh = mesh;
        
        meshFilter.mesh = mesh;
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

         WorldScriptableObjects.GetInstance().gridHighLightData.meshData = meshFilterHighLight;
         WorldScriptableObjects.GetInstance().gridHighLightData.meshData.mesh = meshHighLightTiles;
         
        meshFilterHighLight.mesh = meshHighLightTiles;
    }
    private void AddTexture (int textureID)
    {
        Vector2 textureCoordinate = new ChangeTextureForTile().TextureCoordinate(textureID, VoxelDataTest.TextureAtlasSizeInBlocks);
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
}