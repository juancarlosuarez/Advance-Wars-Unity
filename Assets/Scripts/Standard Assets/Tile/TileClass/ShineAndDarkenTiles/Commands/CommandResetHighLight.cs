using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandResetHighLight : ICommand
{
    private List<TileRefactor> _tilesToGetReset;
    public CommandResetHighLight(List<TileRefactor> tilesToReset)
    {
        _tilesToGetReset = tilesToReset;
    }
    public void Execute()
    {
        var instanceHighLight = new HighLightTilesRefactor();
        instanceHighLight.ResetHighLightTiles(_tilesToGetReset);
        
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
