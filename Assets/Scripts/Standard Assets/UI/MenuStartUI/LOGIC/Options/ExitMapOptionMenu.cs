using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMapOptionMenu : IStartOption
{
    public void Trigger()
    {
        CommandQueue.GetInstance.AddCommand(new CommandDeleteCurrentJSON(), false);
        CommandQueue.GetInstance.AddCommand(new CommandChangeScene("Menu Start"), false);
    }
}
