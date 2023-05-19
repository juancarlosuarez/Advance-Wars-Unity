using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitOptionMenu : IStartOption
{
    public void Trigger()
    {
        Application.Quit();
    }
}
