using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class ManagerMenusEditMapMode : MonoBehaviour
{
    
    [SerializeField] private ControllerUnitsEditMap menuOptionsUnitsSPrefab;
    [SerializeField] private ControllerTilesEditMap menuOptionsTilesPrefab;
    
    [SerializeField] private SOMenuManager menuController;
    [SerializeField] private PutElementsEditMap _putElementsEditMap;

    public static event Action CloseEditorMapToOpenMenu;

    private ControllerTilesEditMap _menuOptionsTiles;
    private ControllerUnitsEditMap _menuOptionsUnits;

    private void Awake()
    {
        menuController.reference = null;
    }
    
    public void OpenMenuTiles()
    {
        if (!_menuOptionsTiles)
        {
            StateGamePlay.GetInstance().ChangeState(GameState.EditingMapUI);
            CloseEditorMapToOpenMenu?.Invoke();

            _menuOptionsTiles = Instantiate(menuOptionsTilesPrefab, transform.position, Quaternion.identity);
            _menuOptionsTiles.transform.SetParent(transform);

            menuController.reference = ChainedMenu.GetInstance();
            menuController.reference.StartToControlMenu(_menuOptionsTiles, false);
        }
        else
        {
            CloseEditorMapToOpenMenu?.Invoke();
            StateGamePlay.GetInstance().ChangeState(GameState.EditingMapUI);
            
            menuController.reference = ChainedMenu.GetInstance();
            menuController.reference.StartToControlMenu(_menuOptionsTiles, false);
                    
        }
    }

    private void PutElementInGrid()
    {
        _putElementsEditMap.PutElementInGrid();
    }
    public void OpenMenuUnits()
    {
            print("intento abrir Menu Units");
        if (!_menuOptionsUnits)
        {
            StateGamePlay.GetInstance().ChangeState(GameState.EditingMapUI);
            CloseEditorMapToOpenMenu?.Invoke();

            _menuOptionsUnits = Instantiate(menuOptionsUnitsSPrefab, transform.position, Quaternion.identity);
            _menuOptionsUnits.transform.SetParent(transform);
                    
            menuController.reference = ChainedMenu.GetInstance();
            menuController.reference.StartToControlMenu(_menuOptionsUnits, false);
        }
        else
        {
            CloseEditorMapToOpenMenu?.Invoke();
            StateGamePlay.GetInstance().ChangeState(GameState.EditingMapUI);
            
            menuController.reference = ChainedMenu.GetInstance();
            menuController.reference.StartToControlMenu(_menuOptionsUnits, false);
        }
    }
    private void CloseEditorToGamePlay()
    {
        InterfacesManager.sharedInstance.OpenGamePlayInterfaces();
        // StateGamePlay.GetInstance().ChangeState(GameState.GamePlayActionable);
        //
        // //UIManagerGamePlay.sharedInstance.CloseEditorFromGamePlay();
        // //UIManagerGamePlay.sharedInstance.OpenGamePlay();
        //
        // PlayerController.sharedInstance.StopControls();
    }

    private void MenuUnitWhileIsTileOpen()
    {
        if (_menuOptionsUnits && _menuOptionsUnits.gameObject.activeSelf == true)
        {
            //_menuOptionsUnits.AnimationDisappearMenu();
            
        }
    }

    private void MenuTileWhileIsUnitOpen()
    {
        if (_menuOptionsTiles && _menuOptionsTiles.gameObject.activeSelf == true)
        {
            //_menuOptionsTiles.AnimationDisappearMenu();
        }
    }

    private void OpenMenuStartEdit()
    {
        CloseEditorMapToOpenMenu?.Invoke();
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.EditMenuStart, 0, false);
    }
    
    private void OnEnable()
    {
        
        EditMapControllers.PressButtonL1 += OpenMenuTiles;
        EditMapControllers.PressButtonR1 += OpenMenuUnits;
        EditMapControllers.PressButtonStartEditMap += OpenMenuStartEdit;

        //EditMapControllers.PressButtonB += CloseEditorToGamePlay;
        EditMapControllers.PressButtonA += PutElementInGrid;

        UIControls.PressButtonL1 += MenuUnitWhileIsTileOpen;
        UIControls.PressButtonR1 += MenuTileWhileIsUnitOpen;


        PlayerController.ResetEventsNoMonobehavior += ResetEvents;
        
    }

    private void OnDisable()
    {
        ResetEvents();
    }
    private void ResetEvents()
    {

        EditMapControllers.PressButtonL1 -= OpenMenuTiles;
        EditMapControllers.PressButtonR1 -= OpenMenuUnits;
        EditMapControllers.PressButtonStartEditMap -= OpenMenuStartEdit;

        //EditMapControllers.PressButtonB -= CloseEditorToGamePlay;
        EditMapControllers.PressButtonA -= PutElementInGrid;

        UIControls.PressButtonL1 -= MenuUnitWhileIsTileOpen;
        UIControls.PressButtonR1 -= MenuTileWhileIsUnitOpen;

        PlayerController.ResetEventsNoMonobehavior -= ResetEvents;
    }


}

