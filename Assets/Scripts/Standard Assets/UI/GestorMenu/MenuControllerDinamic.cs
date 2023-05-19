
public class MenuControllerDinamic : IGestorMenuUI
{

    private int _countTotalID;
    private int _currentID;
    private int _previousID;
    
    private int _currentIdUi;
    private int _previousIdUi;
    
    private int CurrentID
    {
        get => _currentID;
        set
        {
            if (value >= _countTotalID) _currentID = 0;
            if (value < 0) _currentID = _countTotalID - 1;
            if (value >= 0 && value < _countTotalID) _currentID = value;
        }
    }

    private int CurrentIdUi
    {
        get => _currentIdUi;
        set
        {
            if (value >= _countTotalID) _currentIdUi = 0;
            if (value < 0) _currentIdUi = _countTotalID - 1;
            if (value >= 0 && value < _countTotalID) _currentIdUi = value;
        }
    }
    private ControllerUIMenuStatic _currentMenu;
    private bool _orderMenuIsCorrect = false;
    private static MenuControllerDinamic _sharedInstance;
    private static bool _isEventAssign = false;
    
    private int _countTotalIDPrevious;
    private int _currentIDPrevious;
    private int _previousIDPrevious;
    private int _currentIDUIPrevious;
    private int _previousIDUIPrevious;
    private ControllerUIMenuStatic _currentMenuPrevious;
    private bool _orderMenuIsCorrectPrevious;
    
    private MenuControllerDinamic(){}

    public static MenuControllerDinamic GetInstance()
    {
        if (_sharedInstance == null)
        {
            _sharedInstance = new MenuControllerDinamic();
            
        }

        return _sharedInstance;
    }

    public void StartPreviousMenu()
    {
        AssignEvents();
        StateGamePlay.GetInstance().ChangeState(GameState.GamePlayUI);
        PlayerController.sharedInstance.ChangeControlToUI();
        GetDataPreviousMenu();
        
        _currentMenu.DisplayMenu();
        _currentMenu.HighLight(_currentID, _currentIdUi);
        
        if (!_isEventAssign)
        {
            AssignEvents();
        }
    }

    private void GetDataPreviousMenu()
    {
        _currentMenu = _currentMenuPrevious;
        _orderMenuIsCorrect = _orderMenuIsCorrectPrevious;
        _countTotalID = _countTotalIDPrevious;
        _currentID = _currentIDPrevious;
        _currentIdUi = _currentIDUIPrevious;
        _previousID = _previousIDPrevious;
        _previousIdUi = _previousIDUIPrevious;
    }

    private void PutDataPreviousMenu()
    {
        _currentMenuPrevious = _currentMenu;
        _orderMenuIsCorrectPrevious = _orderMenuIsCorrect;
        _countTotalIDPrevious = _countTotalID;
        _currentIDPrevious = _currentID;
        _currentIDUIPrevious = _currentIdUi;
        _previousIDPrevious = _previousID;
        _previousIDUIPrevious = _previousIdUi;
    }
    public void StartToControlMenu(IPlainOptions menuThatIWillManage, bool orderMenuIsCorrect)
    {
        _currentMenu = (ControllerUIMenuStatic)menuThatIWillManage;
        StateGamePlay.GetInstance().ChangeState(GameState.GamePlayUI);
        _orderMenuIsCorrect = orderMenuIsCorrect;
        ResetValuesBeforeMenu();
        PlayerController.sharedInstance.ChangeControlToUI();

        _currentMenu.DisplayMenu();

        _currentMenu.HighLight(_currentID, _currentID);
        
        if (!_isEventAssign)
        {
            AssignEvents();
        }
    }
    public void ResetValuesBeforeMenu()
    {
        _currentID = 0;
        _currentIdUi = 0;
        _previousID = 0;
        _previousIdUi = 0;
        _countTotalID = _currentMenu.GetCountList();
    }
    public void MovePointerToAnotherOption(int addAndSubtract)
    {
        if (addAndSubtract == 0) return;
        
        _previousID = _currentID;
        _previousIdUi = _currentIdUi;

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
        
        
        _currentMenu.LowLight(_previousID, _previousIdUi);
        _currentMenu.HighLight(_currentID, _currentIdUi);
    }

    public void SelectOption()
    {
        if (!_currentMenu.CanSelectOption(_currentID))
        {
            SoundManager._sharedInstance.PlayEffectSound(EffectNames.Error);
            return;
        }
        PutDataPreviousMenu();
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