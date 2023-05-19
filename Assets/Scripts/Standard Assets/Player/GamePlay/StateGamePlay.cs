using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGamePlay
{
    private static StateGamePlay _sharedInstance;
    private StateGamePlay(){}
    
    public static StateGamePlay GetInstance() => _sharedInstance ??= new StateGamePlay();
    
    private void Update()
    {
        if (_currentGameState == GameState.EditingMap) PlayerController.sharedInstance.ControllerEditorMap();
    }

    private GameState _currentGameState = GameState.GamePlayActionable;

    public bool IsStateEditingMap() => _currentGameState == GameState.EditingMap;
    public bool IsStateGamePlayActionable() => _currentGameState == GameState.GamePlayActionable;
    public bool IsStateEditingMapUI() => _currentGameState == GameState.EditingMapUI;
    public bool IsStateGamePlayUI() => _currentGameState == GameState.GamePlayUI;

    public void ChangeState(GameState newState) => _currentGameState = newState;
}
    public enum GameState
    {
        GamePlayActionable, 
        GamePlayWithOutControls, 
        GamePlayUI,
        EditingMap,
        EditingMapUI
    }

