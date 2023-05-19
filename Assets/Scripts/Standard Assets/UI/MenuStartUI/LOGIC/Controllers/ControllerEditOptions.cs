using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllerEditOptions : ControllerUIMenuStatic
{
    private List<Vector2> positionArrowHighLight;

    //[SerializeField] private SOGameObject smallArrowReference;
    private readonly int[] _keysOptionId = {18, 19};
    private DataUIMenuStart _currentDataUI;
    private Vector2 _positionMenu;

    public static event Action OpenInterfaceByOption;
    private bool _firstRound;
    //public static event Action CloseMenuStartEditMapByB;
    public override bool CanSelectOption(int count)
    {
        return true;
    }

    public override void CloseMenuByTriggerButton()
    {
        //Destroy(this);
        _currentDataUI.arrowHighLight.SetActive(false);
        FactoryControllersMenuStartUI.sharedInstance.CloseCurrentMenu();
        //UIManagerGamePlay.sharedInstance.CloseMenuStartEdit();
        //UIManagerGamePlay.sharedInstance.OpenEditorFromStart();

    }
    public override void CloseMenuByPressB()
    {
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.Exit);
        _currentDataUI.arrowHighLight.SetActive(false);
        _currentDataUI.referenceToMenu.SetActive(false);
        FactoryControllersMenuStartUI.sharedInstance.CloseCurrentMenu();
        
        
        //FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.EditMenuStart, 0, true);
        CommandQueue.GetInstance.AddCommand(new CommandOpenMenu(ControllerMenuName.EditMenuStart, 0, false), false);
        
    }

    public override int GetCountList() => 2;
    public override void HighLight(int count, int countUI)
    {
        if (!_firstRound) SoundManager._sharedInstance.PlayEffectSound(EffectNames.MoveBetweenMenus);
        else _firstRound = false;
        _currentDataUI.arrowHighLight.transform.position = positionArrowHighLight[countUI];
    }

    public override void LowLight(int count, int countUI)
    {
    }

    public override void DisplayMenu()
    {
        //UIManagerGamePlay.sharedInstance.CloseEditorFromStart();
        _firstRound = true;
        _currentDataUI = FactoryBuildersEachMenuGamePlayUI.sharedInstance.GetDataMenu(nameFrameGeneric);
        GetPositionMenu();
        CalculatePositionArrow();
    }

    private void GetPositionMenu()
    {
        var camera = GameObject.Find("Main Camera");
        var positionCamera = camera.transform.position;
        _positionMenu = new Vector2(positionCamera.x - 4, positionCamera.y);
        FactoryBuildersEachMenuGamePlayUI.sharedInstance.ActiveUIMenu(nameFrameGeneric,
            _positionMenu);
        
    }
    private void CalculatePositionArrow()
    {

        //smallArrowReference.reference.SetParent(gameObject);
        _currentDataUI.arrowHighLight.SetActive(true);
        
        positionArrowHighLight = _currentDataUI.arrowPositions.Select(pos => 
            new Vector2(pos.x + _positionMenu.x, pos.y + _positionMenu.y)).ToList();
    }
    public override void Trigger(int count)
    {
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.SelectOption);
        _currentDataUI.arrowHighLight.SetActive(false);
        FactoryControllersMenuStartUI.sharedInstance.CloseCurrentMenu();
        _currentDataUI.referenceToMenu.SetActive(false);
        
        if (count == 0) OpenInterfaceByOption?.Invoke();
        OptionSelect(_keysOptionId[count]).Trigger();
    }
}
