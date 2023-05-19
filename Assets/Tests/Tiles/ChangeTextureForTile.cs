using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTextureForTile
{
    //private List<Vector2> uvs;
    public void Change(int textureID, Mesh mesh, TileRefactor tile)
    {
        Vector2 textureCoordinate = TextureCoordinate(textureID, VoxelDataTest.TextureAtlasSizeInBlocks);
        float x = textureCoordinate.x;
        float y = textureCoordinate.y;
        var uvs = mesh.uv;
        var idTile = tile.spawnID;
        
        uvs[idTile * 6] = new Vector2(x, y);
        uvs[idTile * 6 + 1] = new Vector2(x, y + VoxelDataTest.NormalizedBlockTextureSize);
        uvs[idTile * 6 + 2] = new Vector2(x + VoxelDataTest.NormalizedBlockTextureSize, y);
        uvs[idTile * 6 + 3] = new Vector2(x + VoxelDataTest.NormalizedBlockTextureSize, y);
        uvs[idTile * 6 + 4] = new Vector2(x, y + VoxelDataTest.NormalizedBlockTextureSize);
        uvs[idTile * 6 + 5] = new Vector2(x + VoxelDataTest.NormalizedBlockTextureSize,
            y + VoxelDataTest.NormalizedBlockTextureSize);

        mesh.uv = uvs;
    }
    public Vector2 TextureCoordinate(int textureID, int atlasSize)
    {
        float y = textureID / atlasSize;
        float x = textureID - (y * atlasSize);
        
        float normalizedBlockTextureSize = 1 / (float)atlasSize;
        
        x *= normalizedBlockTextureSize;
        y *= normalizedBlockTextureSize;

        y = 1f - y - normalizedBlockTextureSize;
        
        return new Vector2(x, y);
    }
}
