using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerSelectMap : ControllerUIMenuStatic
{
    public static event Action BackControllerSelectMap;
    
    private DataUIMenuStart _currentDataUI;
    private List<Vector2> positionArrowHighLight = new List<Vector2>();

    [SerializeField] private Vector2 _positionMenuSelectMap;
    [SerializeField] private Vector2 _positionDisplayBuilds;
    [SerializeField] private IntReference _currentKeyForBuildsDisplay; //This scriptableObject its used by the display builds builder.

    [SerializeField] private GameObject _prefabMapScroll;
    private GameObject _mapScroll;

    private bool _firstRound;
    public override void DisplayMenu()
    {
         _firstRound = true;
         _currentDataUI = FactoryBuildersEachMenuGamePlayUI.sharedInstance.GetDataMenu(nameFrameGeneric);
         CalculatePositionArrow();

         _currentKeyForBuildsDisplay.reference = 0;
         FactoryBuildersEachMenuGamePlayUI.sharedInstance.JustActiveUIMenu(nameFrameGeneric,
             _positionMenuSelectMap);
         FactoryBuildersEachMenuGamePlayUI.sharedInstance.JustActiveUIMenu(
             NameElementWithFramesGeneric.DisplayBuilds, _positionDisplayBuilds);
         PutArrowHierarchyCorrectly();

         if (_mapScroll == null) InstantiateMapScroll();
         else _mapScroll.SetActive(true);
         
    }
     public override void CloseMenuByPressB()
    {
        FactoryBuildersEachMenuGamePlayUI.sharedInstance.DisableMenusControllers();
        FactoryControllersMenuStartUI.sharedInstance.CloseCurrentMenu();
        _currentDataUI.arrowHighLight.SetActive(false);
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.Exit);
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.SelectModeGame, 0, true);
    }
     public override void CloseMenuByTriggerButton()
    {
        //Siguiente Menu el de escoger mapa wowoww
    }

    public override void HighLight(int count, int countUI)
    {
        if (!_firstRound) SoundManager._sharedInstance.PlayEffectSound(EffectNames.MoveBetweenMenus);
        else _firstRound = false;
        
        _currentDataUI.arrowHighLight.transform.position = positionArrowHighLight[count];
        _currentKeyForBuildsDisplay.reference = count;
        FactoryBuildersEachMenuGamePlayUI.sharedInstance.UpdateMenu(NameElementWithFramesGeneric.DisplayBuilds);
        FactoryBuildersEachMenuGamePlayUI.sharedInstance.JustActiveUIMenu(NameElementWithFramesGeneric.DisplayBuilds, _positionDisplayBuilds);
    }

    public override void LowLight(int count, int countUI)
    {
        
    }

    public override void Trigger(int count)
    {
        var newMapJSON = new StartNewMapJSON();
        newMapJSON.NewMap(count);
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.SelectOption);
        CommandQueue.GetInstance.AddCommand(new CommandSelectMapID(new DataStartMap(4, true)), false);
        CommandQueue.GetInstance.AddCommand(new CommandChangeScene("Gameplay"), false);
    }
    private void CalculatePositionArrow()
    {
        _currentDataUI.arrowHighLight.SetActive(true);
        
        positionArrowHighLight = _currentDataUI.arrowPositions.Select(pos => 
            new Vector2(pos.x + _positionMenuSelectMap.x, pos.y + _positionMenuSelectMap.y)).ToList();
    }

    private void InstantiateMapScroll()
    {
        var parent = GameObject.Find("SelectMap");
        
        _mapScroll = Instantiate(_prefabMapScroll, Vector3.zero, Quaternion.identity);
        _mapScroll.SetParent(parent);
        _mapScroll.transform.SetSiblingIndex(0);
    }
    private void PutArrowHierarchyCorrectly()
    {
        WorldObjectsCanvas.GetInstance().smallArrowHighLight.transform.SetAsLastSibling();
        WorldObjectsCanvas.GetInstance().bigArrowHighLight.transform.SetAsLastSibling();
    }
    public override int GetCountList()
    {
        return 3;
    }

    public override bool CanSelectOption(int count)
    {
        return true;
    }
}
