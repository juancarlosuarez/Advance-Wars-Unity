using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandOpenMenu : ICommand
{
    private ControllerMenuName _newMenuName;
    private int _typeMenu;
    private bool _orderUIIsCorrect;
    public CommandOpenMenu(ControllerMenuName newMenuName, int typeMenu, bool orderUIIsCorrect)
    {
        _newMenuName = newMenuName;
        _typeMenu = typeMenu;
        _orderUIIsCorrect = orderUIIsCorrect;
    }
    public void Execute()
    {
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(_newMenuName, _typeMenu, _orderUIIsCorrect);
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
