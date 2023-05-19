using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class FactoryPrefabricMenusUI : SerializedMonoBehaviour
{
    [SerializeField] private Dictionary<ControllerMenuName, GameObject> prefabsMenus;
    private Dictionary<ControllerMenuName, GameObject> _allMenus;
    private ControllerMenuName _currentMenuActive;

    private static FactoryPrefabricMenusUI _sharedInstance;

    public void ActiveMenuUI(ControllerMenuName nameMenu, Vector2 positionMenu)
    {
        _currentMenuActive = nameMenu;

        var currentMenuSelected = _allMenus[nameMenu];
        
        currentMenuSelected.SetActive(true);
        currentMenuSelected.transform.position = positionMenu;
    }

    public void DisableCurrentMenu()
    {
        var currentMenuSelected = _allMenus[_currentMenuActive];
        currentMenuSelected.SetActive(false);
        
        FactoryControllersMenuStartUI.sharedInstance.CloseCurrentMenu();
    }
    
    public static FactoryPrefabricMenusUI GetInstance()
    {
        return _sharedInstance;
    }
    private void Awake()
    {
        Singleton();
        InstantiateAllElements();
        PutElementsInCanvas();
    }

    private void Singleton()
    {
        _sharedInstance = this;
    }
    
    private void InstantiateAllElements()
    {
        _allMenus = new Dictionary<ControllerMenuName, GameObject>();
        foreach (var prefabs in prefabsMenus)
        {
            var currentMenu = Instantiate(prefabs.Value);
            _allMenus.Add(prefabs.Key, currentMenu);
            currentMenu.SetActive(false);
        }
    }
    
    private void PutElementsInCanvas()
    {
        var parent = GameObject.Find("Canvas");
        foreach (var menu in _allMenus)
        {
            menu.Value.SetParent(parent);
        }
    }
}
