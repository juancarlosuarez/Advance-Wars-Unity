using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ControllerUIFileStartOption : ControllerUIMenuStatic
{
    private DataUIMenuStart _currentDataUI;
    private List<Vector2> _positionArrowHighLight;
    private int[] _keysOptionId = { 5, 6, 7 };
    private Vector2 _positionMenu;
    private bool _firstRound;
    public override void HighLight(int count, int countUI)
    {
        if (!_firstRound) SoundManager._sharedInstance.PlayEffectSound(EffectNames.MoveBetweenMenus);
        else _firstRound = false;
        _currentDataUI.arrowHighLight.transform.position = _positionArrowHighLight[countUI];
    }

    public override void LowLight(int count, int countUI)
    {
    }

    public override void Trigger(int count)
    {
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.SelectOption);
        OptionSelect(_keysOptionId[count]).Trigger();
    }


    public override void DisplayMenu()
    {
        _firstRound = true;
        _currentDataUI =
            FactoryBuildersEachMenuGamePlayUI.sharedInstance.GetDataMenu(nameFrameGeneric);
        CalculatePositionArrow();
        FactoryBuildersEachMenuGamePlayUI.sharedInstance.ActiveUIMenu(nameFrameGeneric,
            _positionMenu);
        
    }
    
    public override void CloseMenuByTriggerButton()
    {
        _currentDataUI.arrowHighLight.SetActive(false);
        FactoryControllersMenuStartUI.sharedInstance.CloseCurrentMenu();
    }
    private void CalculatePositionArrow()
    {
        var camera = GameObject.Find("Main Camera");
        var positionCamera = camera.transform.position;
        _positionMenu = new Vector2(positionCamera.x - 4, positionCamera.y);
        _currentDataUI.arrowHighLight.SetActive(true);

        _positionArrowHighLight = _currentDataUI.arrowPositions.Select(pos => 
            new Vector2(pos.x + _positionMenu.x, pos.y + _positionMenu.y)).ToList();
    }

    public override int GetCountList() => 3;
    

    public override bool CanSelectOption(int count)
    {
        return true;
    }

    public override void CloseMenuByPressB()
    {
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.Exit);
        _currentDataUI.arrowHighLight.SetActive(false);
        FactoryControllersMenuStartUI.sharedInstance.CloseCurrentMenu();
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.EditMenuStart, 0, false);
    }
}
