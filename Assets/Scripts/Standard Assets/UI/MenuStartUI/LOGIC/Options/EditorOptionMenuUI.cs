using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorOptionMenuUI : IStartOption
{
    public void Trigger()
    {
        CommandQueue.GetInstance.AddCommand(new CommandSelectMapID(new DataStartMap(2, false)), false);
        CommandQueue.GetInstance.AddCommand(new CommandChangeScene("Gameplay"), false);
    }
}
