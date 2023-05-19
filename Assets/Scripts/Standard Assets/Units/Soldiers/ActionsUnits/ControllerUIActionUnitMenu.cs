using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ControllerUIActionUnitMenu : ControllerUIMenuStatic
{
    [Header("ScriptableObjects")]
    private List<MultiplesOptionsUnit> currentActions; //Borrar la clase
    
    private List<SystemOptionUnitsUI> listActionUI; //Borrar la clase

    private List<int> _idActions;
    
    [Header("ScriptableObjects")]
    [SerializeField] ActionReference currentActionSelected;
    private DataUIMenuStart _currentDataUI;
    private List<Vector2> _positionArrowHighLight;
    [SerializeField] SOListInt _listIDActions;
    [SerializeField] private SOListInt _listActionsID;
    [SerializeField] private Vector2 _positionMenuRight;
    [SerializeField] private Vector2 _positionMenuLeft;
    private bool _firstRound;
    public override bool CanSelectOption(int count)
    {
        return true;
    }

    public override void CloseMenuByTriggerButton()
    {
        DestroyMenu();
    }
    public void DestroyMenu() 
    {
        _currentDataUI.arrowHighLight.SetActive(false);
        StateGamePlay.GetInstance().ChangeState(GameState.GamePlayActionable);
        
        FactoryControllersMenuStartUI.sharedInstance.CloseCurrentMenu();
        FactoryBuildersEachMenuGamePlayUI.sharedInstance.DisableMenusControllers();
    } 
    public override int GetCountList()
    {
        return _listIDActions.reference.Count;
    }

    public override void HighLight(int count, int countUI)
    {
        if (!_firstRound) SoundManager._sharedInstance.PlayEffectSound(EffectNames.MoveBetweenMenus);
        else _firstRound = false;
        _currentDataUI.arrowHighLight.transform.position = _positionArrowHighLight[countUI];   
    }

    public override void LowLight(int count, int countUI)
    {
        
    }
    
    public override void DisplayMenu()
    {
        print("Menu options ON");
        
        _firstRound = true;
        _currentDataUI = FactoryBuildersEachMenuGamePlayUI.sharedInstance.GetDataMenu(nameFrameGeneric);

        var positionCamera = Camera.main.gameObject.transform.position;
        var positionMenu = new Vector2();
        if (WorldScriptableObjects.GetInstance().isInterfaceInRightPosition) positionMenu = new Vector2(positionCamera.x - _positionMenuRight.x, positionCamera.y - _positionMenuRight.y);
        else positionMenu = new Vector2(positionCamera.x - _positionMenuLeft.x, positionCamera.y - _positionMenuLeft.y);
        //var positionMenu = new Vector2(positionTile.x, positionTile.y + 1);
        
        FactoryBuildersEachMenuGamePlayUI.sharedInstance.ActiveUIMenu(nameFrameGeneric, positionMenu);
        GetIDsForActions();
        CalculatePositionArrow();
    }

    private void GetIDsForActions()
    {
        _idActions = new List<int>();
        foreach (var id in _listActionsID.reference)
        {
            switch (id)
            {
                case 0:
                    //Attack
                    _idActions.Add(8);
                    break;
                case 1:
                    _idActions.Add(7);
                    //Move
                    
                    break;
                case 2:
                    _idActions.Add(9);
                    //Cancel
                    break;
                case 3:
                    _idActions.Add(12);
                    break;
                case 4:
                    _idActions.Add(10);
                    break;
                case 5:
                    _idActions.Add(11);
                    break;
            }
        }
    }
    private void CalculatePositionArrow()
    {
        Vector2 positionMenu = _currentDataUI.referenceToMenu.transform.position;
        _currentDataUI.arrowHighLight.SetActive(true);
        
        _positionArrowHighLight = _currentDataUI.arrowPositions.Select(pos => 
            new Vector2(pos.x + positionMenu.x, pos.y + positionMenu.y)).ToList();
    }
    
    public override void Trigger(int count)
    {
     
        //I make this specific number, because is the cancel action and this function is just perfect.
        if (_idActions[count] != 9) OptionSelect(_idActions[count]).Trigger();
        else
        {
            Debug.Log("what the hell");
            CloseMenuByPressB();
            return;
        }
        CommandQueue.GetInstance.ExecuteCommandImmediately(new DeselectUnitUICommand(), false);
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.SelectOption);
    }
    public override void CloseMenuByPressB()
    {
        PlayerController.sharedInstance.ChangeControlToGamePlay();
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.Exit);
        print("DestroyMenu");
        DestroyMenu();
    }
}
