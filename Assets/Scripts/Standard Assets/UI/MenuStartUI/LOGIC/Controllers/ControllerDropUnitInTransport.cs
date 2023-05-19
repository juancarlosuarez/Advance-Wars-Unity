using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDropUnitInTransport : ControllerUIMenuStatic
{
    private List<TileRefactor> _tilesWhereUnitCanBeDispose;
    private bool _firstRound;
    public override void DisplayMenu()
    {
        _firstRound = true;
        _tilesWhereUnitCanBeDispose = WorldScriptableObjects.GetInstance().tilesWhereUnitCanBeDispose.reference;
    }
    
    public override void CloseMenuByPressB()
    {
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.Exit);
        MenuControllerDinamic.GetInstance().StartPreviousMenu();
        CommandQueue.GetInstance.AddCommand(new ResetHighLightTilesCommand(WorldScriptableObjects.GetInstance().tilesWhereUnitCanBeDispose.reference), false);
        CommandQueue.GetInstance.AddCommand(new ActiveSelectUnitUICommand(), false);
    }

    public override void CloseMenuByTriggerButton()
    {
        
    }

    public override void HighLight(int count, int countUI)
    {
        if (!_firstRound) SoundManager._sharedInstance.PlayEffectSound(EffectNames.MoveBetweenSubMenus);
        else _firstRound = false;
        CommandQueue.GetInstance.AddCommand(new CommandHighLightTile(), false);
        CommandQueue.GetInstance.AddCommand(new CommandDisplayHighLightTileObjective(count), false);
    }

    public override void LowLight(int count, int countUI)
    {
        
    }

    public override void Trigger(int count)
    {
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.SelectOption);
        var transport = WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference.occupiedSoldier;
        //CommandQueueTask.GetInstance.AddCommand(new CommandTaskMove(), true);
        CommandQueue.GetInstance.AddCommand(new CommandResetHighLight(WorldScriptableObjects.GetInstance().tilesWhereUnitCanBeDispose.reference), false);
        CommandQueue.GetInstance.AddCommand(new CommandTaskMove(), true);
        CommandQueue.GetInstance.AddCommand(new CommandDrop(transport, _tilesWhereUnitCanBeDispose[count]), false);
        //transport.DropUnit(_tilesWhereUnitCanBeDispose[count]);
        
    }

    public override int GetCountList()
    {
        return WorldScriptableObjects.GetInstance().tilesWhereUnitCanBeDispose.reference.Count;
    }

    public override bool CanSelectOption(int count)
    {
        return true;
    }
}
