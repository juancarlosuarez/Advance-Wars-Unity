using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactiveSelectCommand : ICommand
{
    public void Execute()
    {
        Debug.Log("desactivar select");
        var select = GameObject.Find("Select").GetComponent<SpriteRenderer>();
        select.enabled = false;
        
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
