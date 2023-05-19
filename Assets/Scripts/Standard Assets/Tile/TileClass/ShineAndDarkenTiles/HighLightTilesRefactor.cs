using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighLightTilesRefactor
{
    public void PutHighLightTile(TileRefactor tileHighLight, TypeHighLightTile typeHighLightTile)
    {
        var changeTexture = new ChangeTextureForTile();
        var currentMesh = WorldScriptableObjects.GetInstance().gridHighLightData.meshData;

        changeTexture.Change((int)typeHighLightTile, currentMesh.mesh, tileHighLight);
        
    }
    public void ResetHighLightTiles(List<TileRefactor> tilesHighLight)
    {
        var changeTexture = new ChangeTextureForTile();
        var currentMesh = WorldScriptableObjects.GetInstance().gridHighLightData.meshData;

        foreach (var c in tilesHighLight)
        {
            changeTexture.Change(c.dataVariable.textureID, currentMesh.mesh, c);
        }
    }
}

public enum TypeHighLightTile
{
    HighLightAttack = 3, HighLightPath = 7, HighLightActionRange = 11
}
