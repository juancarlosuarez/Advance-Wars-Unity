using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandDisableUnit : ICommand
{
    private Soldier _currentUnit; 
    public CommandDisableUnit(Soldier currentUnit)
    {
        _currentUnit = currentUnit;
    }
    
    public void Execute()
    {
        _currentUnit.thisUnitCanMakeSomeAction = false;
        
        Color transparentColor = _currentUnit.spriteRenderer.color;
        transparentColor.a = .5f;
        _currentUnit.spriteRenderer.color = transparentColor;
        
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
