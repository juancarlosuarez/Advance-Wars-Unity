using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandAttack : ICommand
{
    private DamageValues _currentDamageValues;
    public CommandAttack(DamageValues currentDamageValues)
    {
        _currentDamageValues = currentDamageValues;
    }
    public void Execute()
    {
        StartAnimation();
        MakeDamageToUnits();
        
        FinishExecute();
    }
    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }

    private void StartAnimation()
    {
        //StartANimation
    }
    private void MakeDamageToUnits()
    {
        var soldierAttacker = _currentDamageValues.tileAttacker.occupiedSoldier;
        var soldierDefender = _currentDamageValues.tileDefender.occupiedSoldier;

        soldierAttacker.currentLife -= (int)_currentDamageValues.valueDamageFromDefender;
        soldierDefender.currentLife -= (int)_currentDamageValues.valueDamageFromAttacker;
        

        soldierAttacker.currentLifeUI = GetLifeUI(soldierAttacker.currentLife);
        soldierDefender.currentLifeUI = GetLifeUI(soldierDefender.currentLife);
        
        soldierAttacker.managerLifeUnitUI.SetLife(soldierAttacker);
        soldierDefender.managerLifeUnitUI.SetLife(soldierDefender);

        if (soldierDefender.currentLife <= 0)
        {
            Debug.Log("Destroy Defender");
            CommandQueue.GetInstance.ExecuteCommandImmediately(new CommandDestroyUnit(soldierDefender, true), false);
        }
        if (soldierAttacker.currentLife <= 0)
        {
            Debug.Log("Destroy Attacker");
            CommandQueue.GetInstance.ExecuteCommandImmediately(new CommandDestroyUnit(soldierAttacker, true), false);
        }
    }

    private int GetLifeUI(int currentLife)
    {
        var life = currentLife / 10;
        if (life == 0) life = 1;
        return life;
    }
}

public class DamageValues
{
    public TileRefactor tileAttacker;
    public TileRefactor tileDefender;
    public float valueDamageFromAttacker;
    public float valueDamageFromDefender;
}
