using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControllerStatic : IGestorMenuUI
{
    private int _countTotalID;
    private int _currentID;
    private int _currentIDUI;
    private int _previousID;
    private int _previousIDUI;
    private bool _orderMenuIsCorrect;
    private bool _isEventAssign;
    private ControllerUIMenuStatic _currentMenu;
    
    private int CurrentID
    {
        get => _currentID;
        set
        {
            if (value >= _countTotalID) _currentID = _countTotalID;
            if (value < 0) _currentID = 0;
            if (value >= 0 && value < _countTotalID) _currentID = value;
        }
    }

    private int CurrentIdUi
    {
        get => _currentIDUI;
        set
        {
            if (value >= _countTotalID) _currentIDUI = _countTotalID;
            if (value < 0) _currentIDUI = 0;
            if (value >= 0 && value < _countTotalID) _currentIDUI = value;
        }
    }
    private MenuControllerStatic(){}
    private static MenuControllerStatic _sharedInstance;
    public static MenuControllerStatic GetInstance()
    {
        if (_sharedInstance == null)
        {
            _sharedInstance = new MenuControllerStatic();
                
        }

        return _sharedInstance;
    }
    
    public void StartToControlMenu(IPlainOptions menuThatIWillManage, bool orderMenuIsCorrect)
    {
        _currentMenu = (ControllerUIMenuStatic)menuThatIWillManage;
        StateGamePlay.GetInstance().ChangeState(GameState.GamePlayUI);
        _orderMenuIsCorrect = orderMenuIsCorrect;
        ResetValuesBeforeMenu();
        PlayerController.sharedInstance.ChangeControlToUI();
        
        _currentMenu.DisplayMenu();
        
        _currentMenu.HighLight(_currentID, _currentIDUI);
        
        if (!_isEventAssign)
        {
            AssignEvents();
        }
    }

    public void ResetValuesBeforeMenu()
    {
        _currentID = 0;
        _currentIDUI = 0;
        _previousID = 0;
        _previousIDUI = 0;
        _countTotalID = _currentMenu.GetCountList();
    }
    public void MovePointerToAnotherOption(int addAndSubtract)
    {
        if (addAndSubtract == 0) return;
            
        _previousID = _currentID;
        _previousIDUI = _currentIDUI;

        if (addAndSubtract > 0)
        {
            CurrentID--;
            if (!_orderMenuIsCorrect) CurrentIdUi--;
            else CurrentIdUi++;
        }

        if (addAndSubtract < 0)
        {
            if (!_orderMenuIsCorrect) CurrentIdUi++;
            else CurrentIdUi--;
                
            CurrentID++;
        }
        _currentMenu.LowLight(_previousID, _previousIDUI);
        _currentMenu.HighLight(_currentID, _currentIDUI);
    }

    public void SelectOption()
    {
        if (!_currentMenu.CanSelectOption(_currentID))
        {
            SoundManager._sharedInstance.PlayEffectSound(EffectNames.Error);
            return;
        }
        ResetEvents();
        _currentMenu.CloseMenuByTriggerButton();
        _currentMenu.Trigger(_currentID);
    }
    private void ExitWithButtonB()
    {
        ResetEvents();
        ResetValuesBeforeMenu();
        _currentMenu.CloseMenuByPressB();
    }
    private void AssignEvents()
    {
        _isEventAssign = true;
        UIControls.PressButtonA += SelectOption;
        UIControls.PressButtonUpArrow += MovePointerToAnotherOption;
        UIControls.PressButtonDownArrow += MovePointerToAnotherOption;
        UIControls.PressButtonB += ExitWithButtonB;
        PlayerController.ResetEventsNoMonobehavior += ResetEvents;
    }
    private void ResetEvents()
    {
        _isEventAssign = false;
        UIControls.PressButtonA -= SelectOption;
        UIControls.PressButtonUpArrow -= MovePointerToAnotherOption;
        UIControls.PressButtonDownArrow -= MovePointerToAnotherOption;
        UIControls.PressButtonB -= ExitWithButtonB;
        PlayerController.ResetEventsNoMonobehavior -= ResetEvents;
    }
}
