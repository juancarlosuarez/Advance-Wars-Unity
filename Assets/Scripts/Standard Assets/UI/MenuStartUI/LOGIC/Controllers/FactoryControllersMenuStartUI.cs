using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum ControllerMenuName
{
    MenuNull ,EditMenuStart, FileMenuStart, FillMenuStart, HelpMenuStart1, HelpMenuStart2, SaveMenuStart,
    LoadMenuStart, OptionUnits, ObjectiveAttack, LoadTransport, BarrackMenu, MenuStartGamePlay, SelectMap,
    OptionStartGameplay, SelectModeGame, SelectNewMap, OptionEdits
}
public class FactoryControllersMenuStartUI : ControllerUIMenuStatic
{
    [SerializeField] private SOMenuManager _menuManager;
    [SerializeField] private Dictionary<ControllerMenuName, ControllerUIStartParent> _allPrefabsControllers;
    private ControllerMenuName _currentMenuOpen; 
    
    public static FactoryControllersMenuStartUI sharedInstance;
    [SerializeField] private SOListControllersUIStart allControllersReferences;
    
    private void AssignValuesToDictionary()
    {
        _allPrefabsControllers = new Dictionary<ControllerMenuName, ControllerUIStartParent>();
        foreach (var controller in allControllersReferences.reference)
        {
            _allPrefabsControllers.Add(controller.controllerMenu, controller);
        }
    }
    private void Awake()
    {
        AssignValuesToDictionary();
        _currentMenuOpen = ControllerMenuName.MenuNull;
        sharedInstance = this;
    } 

    public void OpenMenu(ControllerMenuName newMenu, int typeOfMenu, bool dataAndUIHasTheSameOrder)
    {
        if (_currentMenuOpen == newMenu) return;
        _menuManager.reference = typeOfMenu switch
        {
            0 => MenuControllerDinamic.GetInstance(),
            1 => ChainedMenu.GetInstance(),
            2 => MenuControllerStatic.GetInstance(),
            _ => _menuManager.reference
        };
        _menuManager.reference.StartToControlMenu(_allPrefabsControllers[newMenu], dataAndUIHasTheSameOrder);
        _currentMenuOpen = newMenu;
    }
    public void CloseCurrentMenu()
    {
        //_menuManager.reference = null;
        _currentMenuOpen = ControllerMenuName.MenuNull;
    }

}
