using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VoxelDataTest
{
    public static readonly int xSize = 16;//56;
    public static readonly int ySize = 30;//28;
    
    public static readonly int TextureAtlasSizeInBlocks = 4;
    public static float NormalizedBlockTextureSize {

        get { return 1f / (float)TextureAtlasSizeInBlocks; }

    }
    public static readonly Vector2[] voxelVertsCube = new Vector2[4]
    {
        new Vector2(0.0f, 0.0f), //0
        new Vector2(1.0f, 0.0f), //1
        new Vector2(1.0f, 1.0f), //2
        new Vector2(0.0f, 1.0f), //3
        
    };
    public static readonly int[,] voxelTris = new int[1, 6]
    {
        { 0, 3, 1, 1, 3, 2 }
    };

    public static readonly Vector2[] voxelUvs = new Vector2[6]
    {
        new Vector2(0.0f, 0.0f),
        new Vector2(0.0f, 1.0f),
        new Vector2(1.0f, 0.0f),
        new Vector2(1.0f, 0.0f),
        new Vector2(0.0f, 1.0f),
        new Vector2(1.0f, 1.0f),
    };
}
