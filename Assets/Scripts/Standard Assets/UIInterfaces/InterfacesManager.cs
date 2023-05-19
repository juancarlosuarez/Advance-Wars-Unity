using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Random = System.Random;

public class InterfacesManager : MonoBehaviour
{
  [SerializeField] private HeadEditorMap _headEditorMap;
  [SerializeField] private HeadEditorMap _headEditorMapRight;
  [SerializeField] private HeadGamePlay _headGamePlay;
  [SerializeField] private HeadGamePlay _headGamePlayRight;
  [SerializeField] private HeadGamePlay _currentGameplayInterfaceSelected;
  [SerializeField] private HeadEditorMap _currentEditorInterfaceSelected;
  private bool _isGameplayOpen = true;
  private bool _isMenuOpen = false;

  public static InterfacesManager sharedInstance;

  private void Awake() => sharedInstance = this;

  public void OpenGamePlayInterfaces()
  {
    print("openGameplayInterface");
    PlayerController.sharedInstance.ChangeControlToGamePlay();
    _isGameplayOpen = true;
    _isMenuOpen = false;
    _currentGameplayInterfaceSelected.OpenInterfaceGamePlay();
    _currentEditorInterfaceSelected.StopDisplayElementsEditorMenu();
  }
  public void CloseGamePlayInterfaces()
  {
    _isGameplayOpen = false;
    _currentGameplayInterfaceSelected.CloseInterfaceGamePlay();
  }
  
  private void UpdateDataGamePlay() => _currentGameplayInterfaceSelected.UpdateData();
  private void UpdateDataEditMap()
  {
    
    _currentEditorInterfaceSelected.UpdateData();
  }

  private void OpenEditMapInterfaces()
  {
    if (!_isGameplayOpen && !_isMenuOpen)
    {
      return;
    }
    print("Interface edit map open");
    _isMenuOpen = false;
    PlayerController.sharedInstance.ChangeControlToEditMap();
    _isGameplayOpen = false;
    //_headEditorMap.OpenInterface();
    _currentEditorInterfaceSelected.OpenInterface();
    CloseGamePlayInterfaces();
  }
  private void CloseEditMapInterfaceByMenusRequest()
  {
    print("Interface closed, open Menus");
    //_headEditorMap.StopDisplayElementsEditorMenu();
    _currentEditorInterfaceSelected.StopDisplayElementsEditorMenu();
    PlayerController.sharedInstance.ChangeControlToUI();
    _isMenuOpen = true;
  }
  private void MoveGamePlayInterfaceRight()
  {
    if (!_isGameplayOpen) return;
    _headGamePlay.CloseInterfaceGamePlay();
    _currentGameplayInterfaceSelected = _headGamePlayRight;
    _currentGameplayInterfaceSelected.OpenInterfaceGamePlay();
    WorldScriptableObjects.GetInstance().isInterfaceInRightPosition = true;
    UpdateDataGamePlay();
  }

  private void MoveGamePlayInterfaceLeft()
  {
    if (!_isGameplayOpen) return;
    _headGamePlayRight.CloseInterfaceGamePlay();
    _currentGameplayInterfaceSelected = _headGamePlay;
    _currentGameplayInterfaceSelected.OpenInterfaceGamePlay();
    WorldScriptableObjects.GetInstance().isInterfaceInRightPosition = false;
    UpdateDataGamePlay();
  }

  private void MoveEditorInterfaceRight()
  {
    if (_isGameplayOpen) return;
    _headEditorMap.StopDisplayElementsEditorMenu();
    _currentEditorInterfaceSelected = _headEditorMapRight;
    _currentEditorInterfaceSelected.OpenInterface();
    UpdateDataEditMap();
  }

  private void MoveEditorInterfaceLeft()
  {
    if (_isGameplayOpen) return;
    _headEditorMapRight.StopDisplayElementsEditorMenu();
    _currentEditorInterfaceSelected = _headEditorMap;
    _currentEditorInterfaceSelected.OpenInterface();
    UpdateDataEditMap();
  }
  private void OnDisable()
  {
    //GamePlayControls.PressButtonSelect -= OpenEditMapInterfaces;
    GenerateMapWithJson.StartEditor -= OpenEditMapInterfaces;
    ResetOptionMenuEdit.ResetMap -= OpenEditMapInterfaces;
    ControllerUnitsEditMap.CloseMenu -= OpenEditMapInterfaces;
    ControllerTilesEditMap.CloseMenu -= OpenEditMapInterfaces;
    ControllerEditOptions.OpenInterfaceByOption -= OpenEditMapInterfaces;
    ControllerEditMenuStart.CloseMenuStartEditMapByB -= OpenEditMapInterfaces;
    OptionSaveMenuEdit.SaveMenuEdit -= OpenEditMapInterfaces;
    ManagerMenusEditMapMode.CloseEditorMapToOpenMenu -= CloseEditMapInterfaceByMenusRequest;
    SelectRefactorMovement.SelectorIsMoving -= UpdateDataGamePlay;
    GenerateMapWithJson.StartGamePlay -= OpenGamePlayInterfaces;
    GenerateMapWithJson.StartGamePlay -= UpdateDataGamePlay;
    SelectRefactorMovement.SelectorIsMoving -= UpdateDataEditMap;

    CameraMovementLinear.MoveInterfaceLeft -= MoveEditorInterfaceLeft;
    CameraMovementLinear.MoveInterfaceLeft -= MoveGamePlayInterfaceLeft;
    CameraMovementLinear.MoveInterfaceRight -= MoveEditorInterfaceRight;
    CameraMovementLinear.MoveInterfaceRight -= MoveGamePlayInterfaceRight;

    MoveCameraToAnotherTile.MoveInterfaceLeft -= MoveEditorInterfaceLeft;
    MoveCameraToAnotherTile.MoveInterfaceLeft -= MoveGamePlayInterfaceLeft;
    MoveCameraToAnotherTile.MoveInterfaceRight -= MoveEditorInterfaceRight;
    MoveCameraToAnotherTile.MoveInterfaceRight -= MoveGamePlayInterfaceRight;

    CommandNotifyFinish.FinishNotification -= UpdateDataGamePlay;
  }
  private void OnEnable()
  {
    //GamePlayControls.PressButtonSelect += OpenEditMapInterfaces;
    GenerateMapWithJson.StartEditor += OpenEditMapInterfaces;
    ResetOptionMenuEdit.ResetMap += OpenEditMapInterfaces;
    ControllerUnitsEditMap.CloseMenu += OpenEditMapInterfaces;
    ControllerTilesEditMap.CloseMenu += OpenEditMapInterfaces;
    ControllerEditMenuStart.CloseMenuStartEditMapByB += OpenEditMapInterfaces;
    ControllerEditOptions.OpenInterfaceByOption += OpenEditMapInterfaces;
    OptionSaveMenuEdit.SaveMenuEdit += OpenEditMapInterfaces;
    ManagerMenusEditMapMode.CloseEditorMapToOpenMenu += CloseEditMapInterfaceByMenusRequest;
    SelectRefactorMovement.SelectorIsMoving += UpdateDataGamePlay;
    GenerateMapWithJson.StartGamePlay += UpdateDataGamePlay;
    GenerateMapWithJson.StartGamePlay += OpenGamePlayInterfaces;
    SelectRefactorMovement.SelectorIsMoving += UpdateDataEditMap;
    
    CameraMovementLinear.MoveInterfaceLeft += MoveEditorInterfaceLeft;
    CameraMovementLinear.MoveInterfaceLeft += MoveGamePlayInterfaceLeft;
    CameraMovementLinear.MoveInterfaceRight += MoveEditorInterfaceRight;
    CameraMovementLinear.MoveInterfaceRight += MoveGamePlayInterfaceRight;
    
    MoveCameraToAnotherTile.MoveInterfaceLeft += MoveEditorInterfaceLeft;
    MoveCameraToAnotherTile.MoveInterfaceLeft += MoveGamePlayInterfaceLeft;
    MoveCameraToAnotherTile.MoveInterfaceRight += MoveEditorInterfaceRight;
    MoveCameraToAnotherTile.MoveInterfaceRight += MoveGamePlayInterfaceRight;

    CommandNotifyFinish.FinishNotification += UpdateDataGamePlay;
  }
}
