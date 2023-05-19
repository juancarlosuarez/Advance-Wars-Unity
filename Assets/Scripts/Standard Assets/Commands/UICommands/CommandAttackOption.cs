using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CommandAttackOption : ICommand
{
    public void Execute()
    {
        //Abrir menu de objective attacks
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.ObjectiveAttack, 0, true);
        Debug.Log("OPTION ATTACK WORK");
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
