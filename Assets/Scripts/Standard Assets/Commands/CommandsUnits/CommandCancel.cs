using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CommandTaskCancel : ICommand
{

    public void Execute()
    {
        //Se cierra el menu options y se vuelve al GamePlay pero sin deselecionar la unidad
        Debug.Log("Cancel");
        FinishExecute();
    }
    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
