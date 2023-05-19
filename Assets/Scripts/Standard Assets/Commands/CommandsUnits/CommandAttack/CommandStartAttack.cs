using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandStartAttack : ICommand
{
    private float damageFromAttacker;
    private float damageFromDefender;
    private int _count;
    public CommandStartAttack(Vector2 damageFromAttackerAndDefender, int count)
    {
        damageFromAttacker = damageFromAttackerAndDefender.x;
        damageFromDefender = damageFromAttackerAndDefender.y;
        _count = count;
    }
    public void Execute()
    {
        var currentDamage = new DamageValues();
        currentDamage.tileAttacker = WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference;
        currentDamage.tileDefender = WorldScriptableObjects.GetInstance().currentTilesWhereEnemyAre.reference[_count];
        currentDamage.valueDamageFromAttacker = damageFromAttacker;
        currentDamage.valueDamageFromDefender = damageFromDefender;
        
        CommandQueue.GetInstance.AddCommand(new CommandAttack(currentDamage), false);
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
