using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOptionMenu : IStartOption
{
    public void Trigger()
    {
        WorldScriptableObjects.GetInstance().isDeleteOn.boolReference = true;
        PlayerController.sharedInstance.ChangeControlToGamePlay();
        InterfacesManager.sharedInstance.CloseGamePlayInterfaces();
    }
}
