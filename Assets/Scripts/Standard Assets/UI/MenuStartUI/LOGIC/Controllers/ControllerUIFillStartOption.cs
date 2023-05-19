using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ControllerUIFillStartOption : ControllerUIMenuStatic
{
    private DataUIMenuStart _currentDataUI;
    [SerializeField] private TransformReference positionMenu;
    private List<Vector2> positionArrowHighLight;
    private bool _firstRound;
    public override void HighLight(int count, int countUI)
    {
        if (!_firstRound) SoundManager._sharedInstance.PlayEffectSound(EffectNames.MoveBetweenMenus);
        else _firstRound = false;
        _currentDataUI.arrowHighLight.transform.position = positionArrowHighLight[countUI];
    }

    public override void LowLight(int count, int countUI)
    {
    }

    public override void Trigger(int count)
    {
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.SelectOption);
    }

    public override void DisplayMenu()
    {
        _firstRound = true;
        _currentDataUI = FactoryBuildersEachMenuGamePlayUI.sharedInstance.GetDataMenu(nameFrameGeneric);
        FactoryBuildersEachMenuGamePlayUI.sharedInstance.ActiveUIMenu(nameFrameGeneric,
            positionMenu.reference.position);
        
        CalculatePositionArrow();
    }
    
    public virtual void CloseMenuByTriggerButton()
    {
    }
    public void CalculatePositionArrow()
    {
        Vector2 positionMenu = _currentDataUI.referenceToMenu.transform.position;

        positionArrowHighLight = _currentDataUI.arrowPositions.Select(pos => 
            new Vector2(pos.x + positionMenu.x, pos.y + positionMenu.y)).ToList();
    }

    public override int GetCountList() => 5;
    

    public override bool CanSelectOption(int count)
    {
        return true;
    }

    public override void CloseMenuByPressB()
    {
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.Exit);
        FactoryControllersMenuStartUI.sharedInstance.CloseCurrentMenu();
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.EditMenuStart, 0, false);
    }
}