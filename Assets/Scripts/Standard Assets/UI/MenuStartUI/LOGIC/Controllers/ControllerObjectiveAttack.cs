using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class ControllerObjectiveAttack : ControllerUIMenuStatic
{
    private PrepareValuesAttack _prepareValuesAttack;
    public static Action<int, int> SendCurrentDamageSelect;
    public static Action StopCombat;
    private List<TileRefactor> _tilesWhereEnemiesAre;
    private bool _firstRound;
    public override void DisplayMenu()
    {
        _firstRound = true;
        _tilesWhereEnemiesAre = WorldScriptableObjects.GetInstance().currentTilesWhereEnemyAre.reference;
        
        _prepareValuesAttack = new PrepareValuesAttack();
        _prepareValuesAttack.PrepareValues();
        
    }
    
    public override void CloseMenuByPressB()
    {
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.Exit);
        StopCombat?.Invoke();
        MenuControllerDinamic.GetInstance().StartPreviousMenu();
        CommandQueue.GetInstance.AddCommand(new ActiveSelectUnitUICommand(), false);
        CommandQueue.GetInstance.AddCommand(new ResetHighLightTilesCommand(_tilesWhereEnemiesAre), false);
    }

    public override void CloseMenuByTriggerButton()
    {
        CommandQueue.GetInstance.AddCommand(new ResetHighLightTilesCommand(_tilesWhereEnemiesAre), false);
    }

    public override void HighLight(int count, int countUI)
    {
        if (!_firstRound) SoundManager._sharedInstance.PlayEffectSound(EffectNames.MoveBetweenSubMenus);
        else _firstRound = false;
        
        var currentDamage = _prepareValuesAttack.allDamagesCalculate[count];
        SendCurrentDamageSelect?.Invoke((int)currentDamage.x, count);
        CommandQueue.GetInstance.AddCommand(new CommandDisplayEnemys(), false);
        CommandQueue.GetInstance.AddCommand(new CommandDisplayHighLightObjective(count), false);
    }

    public override void LowLight(int count, int countUI)
    {
        
    }

    public override void Trigger(int count)
    {
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.SelectOption);
        StopCombat?.Invoke();
        CommandQueue.GetInstance.AddCommand(new CommandTaskMove(), true);
        CommandQueue.GetInstance.AddCommand(new CommandStartAttack(_prepareValuesAttack.allDamagesCalculate[count], count), false);
    }

    public override int GetCountList()
    {
        return WorldScriptableObjects.GetInstance().currentTilesWhereEnemyAre.reference.Count;
    }

    public override bool CanSelectOption(int count)
    {
        return true;
    }
}
