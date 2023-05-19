using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YieldOptionMenu : IStartOption
{
    public void Trigger()
    {
        CommandQueue.GetInstance.AddCommand(new CommandLoseGame(), true);
    }
}
