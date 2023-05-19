using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Type Terrain Default", menuName = "ScriptableObject/DataSpecial/TileRefactor/Terrain")]
public class VariableDataTileSO : ScriptableObject 
{
    public byte ammountEffortToPass;
    public byte terrainDefense;
    public TerrainTypes terrainTypes;
    public TypesOfTiles typeTile;
    public int textureID;
}
