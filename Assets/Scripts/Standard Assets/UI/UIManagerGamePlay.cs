using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerGamePlay : MonoBehaviour
{
    public static event Action OpenEditorFromGameplayUI;
    public static event Action CloseEditorFromGameplayUI;
    public static event Action OpenEditorFromStartUI;
    public static event Action CloseEditorFromStartUI;

    public static event Action OpenGamePlayUI;
    public static event Action CloseGamePlayUI;

    public static event Action OpenMenuStartGameplayUI;
    public static event Action CloseMenuStartGameplayUI;

    public static event Action OpenMenuStartEditUI;
    public static event Action CloseMenuStartEditUI;

    //private StateUI currentState = StateUI.OpenGamePlay;

    public static UIManagerGamePlay sharedInstance;

    private UIManagerGamePlay(){}
    // private void Awake()
    // {
    //     sharedInstance = this;
    //     currentState = StateUI.OpenGamePlay;
    // }
    //
    // private void OnEnable() => GamePlayControls.PressButtonSelect += OpenEditorFromGamePlay;
    // private void OnDisable() => GamePlayControls.PressButtonSelect -= OpenEditorFromGamePlay;
    // public void OpenEditorFromGamePlay()
    // {
    //     if (currentState == StateUI.OpenEditorFromGamePlay) return;
    //     StateGamePlay.GetInstance().ChangeState(GameState.EditingMap);
    //     CloseGamePlay();
    //     currentState = StateUI.OpenEditorFromGamePlay;
    //     OpenEditorFromGameplayUI?.Invoke();
    // }
    //
    // public void CloseEditorFromGamePlay()
    // {
    //     if (currentState == StateUI.CloseEditorFromGamePlay) return;
    //     currentState = StateUI.CloseEditorFromGamePlay;
    //     CloseEditorFromGameplayUI?.Invoke();
    // }
    // public void OpenEditorFromStart()
    // {
    //     if (currentState == StateUI.OpenEditorFromStartEdit) return;
    //     StateGamePlay.GetInstance().ChangeState(GameState.EditingMap);
    //     PlayerController.sharedInstance.ChangeControlToEditMap();
    //     currentState = StateUI.OpenEditorFromStartEdit;
    //     OpenEditorFromStartUI?.Invoke();
    // }
    // public void CloseEditorFromStart()
    // {
    //     if (currentState == StateUI.CloseEditorFromStart) return;
    //     currentState = StateUI.CloseEditorFromStart;
    //     CloseEditorFromStartUI?.Invoke();
    // }
    // public void OpenGamePlay()
    // {
    //     if (currentState == StateUI.OpenGamePlay) return;
    //     currentState = StateUI.OpenGamePlay;
    //     OpenGamePlayUI?.Invoke();
    // }
    // public void CloseGamePlay()
    // {
    //     if (currentState == StateUI.CloseGamePlay) return;
    //     currentState = StateUI.CloseGamePlay;
    //     CloseGamePlayUI?.Invoke();
    // }
    // public void OpenMenuStartGamePlay()
    // {
    //     if (currentState == StateUI.OpenMenuStartGamePlay) return;
    //     currentState = StateUI.OpenMenuStartGamePlay;
    //     OpenMenuStartEditUI?.Invoke();
    // }
    // public void CloseMenuStartGamePlay()
    // {
    //     if (currentState == StateUI.CloseMenuStartGamePlay) return;
    //     currentState = StateUI.CloseMenuStartGamePlay;
    //     CloseMenuStartGameplayUI?.Invoke();
    // }
    // public void OpenMenuStartEdit()
    // {
    //     if (currentState == StateUI.OpenMenuStartEditMap) return;
    //     currentState = StateUI.OpenMenuStartEditMap;
    //     OpenMenuStartEditUI?.Invoke();
    // }
    // public void CloseMenuStartEdit()
    // {
    //     if (currentState == StateUI.CloseMenuStartEditMap) return;
    //     currentState = StateUI.CloseMenuStartEditMap;
    //     CloseMenuStartEditUI?.Invoke();
    // }
    // private enum StateUI
    // {
    //     OpenGamePlay, OpenEditorFromGamePlay, 
    //     OpenEditorFromStartEdit, CloseEditorFromGamePlay, CloseEditorFromStart,
    //     CloseGamePlay, OpenMenuStartGamePlay,
    //     CloseMenuStartGamePlay, OpenMenuStartEditMap, CloseMenuStartEditMap
    // }
    
}
