using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSelectCommand : ICommand
{
    public void Execute()
    {
        Debug.Log("activar select ");
        var select = GameObject.Find("Select").GetComponent<SpriteRenderer>();
        select.enabled = true;
        
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
