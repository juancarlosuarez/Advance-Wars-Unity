using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSelectUnitUICommand : ICommand
{
    public void Execute()
    {
        Debug.Log("ActiveSelectUnitUI");
        CommandQueue.GetInstance.AddCommand(new CommandDisplayPath(false), false);
        CommandQueue.GetInstance.AddCommand(new PutArrowCommand(), false);
        CommandQueue.GetInstance.AddCommand(new ActiveSelectCommand(), false);
        
        FinishExecute();
    }
    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
