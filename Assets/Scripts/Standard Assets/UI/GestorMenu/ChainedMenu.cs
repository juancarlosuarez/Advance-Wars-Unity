using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainedMenu : IGestorMenuUI
{
    private ISliderMenuOptions _menuOptionsBeingControled;

    private int _maxID;
    private int _currentID;
    private int _previousID;
    private static ChainedMenu sharedInstance;

    public static ChainedMenu GetInstance()
    {
        if (sharedInstance == null) sharedInstance = new ChainedMenu();
        return sharedInstance;
    } 
    private ChainedMenu(){}
    public void StartToControlMenu(IPlainOptions menuThatIWantControl, bool doYouNeedToStopTwoCall)
    {
        //PlayerController.sharedInstance.ChangeControlToUI();
        
        _menuOptionsBeingControled = (ControllerUIMenuOptionsSlider)menuThatIWantControl;
        ResetValuesBeforeMenu();
        _menuOptionsBeingControled.DisplayMenu();
        
        //_menuBeingControled.HighLight(_menuBeingControled.SelectPositionList, _menuBeingControled.SelectPositionList);

        UIControls.PressButtonA += SelectOption;
        UIControls.PressButtonB += CloseMenuByTriggerB;
        UIControls.PressButtonRightArrow += MovePointerToAnotherOption;
        UIControls.PressButtonLeftArrow += MovePointerToAnotherOption;
        UIControls.PressButtonUpArrow += ChangeElementsShowed;
        UIControls.PressButtonDownArrow += ChangeElementsShowed;
        PlayerController.ResetEventsNoMonobehavior += Reset;
    }
    public void ResetValuesBeforeMenu()
    {
        _currentID = 0;
        _previousID = 0;
        //_maxID = _menuBeingControled.GetCountListElement();
    }

    public void MovePointerToAnotherOption(int addAndSubtract)
    {
        if (addAndSubtract > 1 || addAndSubtract == 0 || addAndSubtract < -1) return;
        
        switch (addAndSubtract)
        {
            case 1:
                _menuOptionsBeingControled.MoveElementsInTheSelector(true);
                break;
            case -1:
                _menuOptionsBeingControled.MoveElementsInTheSelector(false);
                break;
        }
    }

    private void ChangeElementsShowed(int addAndSubtract)
    {
        Debug.Log("Change Element Showed" + addAndSubtract);
        if (addAndSubtract > 1 || addAndSubtract == 0 || addAndSubtract < -1) return;
        
        switch (addAndSubtract)
        {
            case 1:
                _menuOptionsBeingControled.ChangeElementsShowed(true);
                break;
            case -1:
                _menuOptionsBeingControled.ChangeElementsShowed(false);
                break;
        }
    }
    public void SelectOption()
    {
        
        _menuOptionsBeingControled.Trigger();
        _menuOptionsBeingControled.CloseMenuByTriggerButton();
        
        UIControls.PressButtonA -= SelectOption;
        UIControls.PressButtonB -= CloseMenuByTriggerB;
        UIControls.PressButtonRightArrow -= MovePointerToAnotherOption;
        UIControls.PressButtonLeftArrow -= MovePointerToAnotherOption;
        UIControls.PressButtonUpArrow -= ChangeElementsShowed;
        UIControls.PressButtonDownArrow -= ChangeElementsShowed;
        PlayerController.ResetEventsNoMonobehavior -= Reset;
    }

    private void CloseMenuByTriggerB()
    {
        _menuOptionsBeingControled.CloseMenuByTriggerButton();
        Reset();
    }

    private void Reset()
    {
        UIControls.PressButtonA -= SelectOption;
        UIControls.PressButtonB -= CloseMenuByTriggerB; 
        UIControls.PressButtonRightArrow -= MovePointerToAnotherOption;
        UIControls.PressButtonLeftArrow -= MovePointerToAnotherOption;
        UIControls.PressButtonUpArrow -= ChangeElementsShowed;
        UIControls.PressButtonDownArrow -= ChangeElementsShowed;
        PlayerController.ResetEventsNoMonobehavior -= Reset;
    }
}
