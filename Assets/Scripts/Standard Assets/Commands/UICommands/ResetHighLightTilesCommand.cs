using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetHighLightTilesCommand : ICommand
{
    private List<TileRefactor> _tilesHighLight;
    public ResetHighLightTilesCommand(List<TileRefactor> tilesHighLight)
    {
        _tilesHighLight = tilesHighLight;
    }
    public void Execute()
    {
        var changeTexture = new ChangeTextureForTile();
        var currentMesh = WorldScriptableObjects.GetInstance().gridHighLightData.meshData;

        foreach (var c in _tilesHighLight)
        {
            changeTexture.Change(c.dataVariable.textureID, currentMesh.mesh, c);
        }
        
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
