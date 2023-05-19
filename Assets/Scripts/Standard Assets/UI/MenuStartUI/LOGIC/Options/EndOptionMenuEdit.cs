using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOptionMenuEdit : IStartOption
{
    public void Trigger()
    {
        
        CommandQueue.GetInstance.AddCommand(new CommandChangeTurn(), false);
    }
}
