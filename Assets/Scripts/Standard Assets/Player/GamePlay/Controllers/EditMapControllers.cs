using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditMapControllers : MonoBehaviour
{
    public static event Action PressButtonRightArrow;
    public static event Action PressButtonLeftArrow;
    public static event Action StopPressArrows;

    public static event Action PressButtonA;
    public static event Action PressButtonB;
    public static event Action PressButtonR1;
    public static event Action PressButtonL1;
    public static event Action PressButtonStartEditMap;
    
    private PlayerCtrl playerControllerEditMap;

    private void Start()
    {
        playerControllerEditMap = PlayerController.sharedInstance.playerController;

        playerControllerEditMap.EditingMap.A.started += ButtonA;
        playerControllerEditMap.EditingMap.B.started += ButtonB;
        playerControllerEditMap.EditingMap.R1.started += ButtonR1;
        playerControllerEditMap.EditingMap.L1.started += ButtonL1;
        playerControllerEditMap.EditingMap.Start.started += ButtonStart;
        
        playerControllerEditMap.EditingMap.ArrowRight.canceled += StopButtonArrow;
        playerControllerEditMap.EditingMap.ArrowLeft.canceled += StopButtonArrow;
        playerControllerEditMap.EditingMap.ArrowsUp.canceled += StopButtonArrow;
        playerControllerEditMap.EditingMap.ArrowDown.canceled += StopButtonArrow;
    }

    private void OnDisable()
    {
        playerControllerEditMap.EditingMap.A.started -= ButtonA;
        playerControllerEditMap.EditingMap.B.started -= ButtonB;
        playerControllerEditMap.EditingMap.R1.started -= ButtonR1;
        playerControllerEditMap.EditingMap.L1.started -= ButtonL1;
        playerControllerEditMap.EditingMap.Start.started -= ButtonStart;
        
        playerControllerEditMap.EditingMap.ArrowRight.canceled -= StopButtonArrow;
        playerControllerEditMap.EditingMap.ArrowLeft.canceled -= StopButtonArrow;
        playerControllerEditMap.EditingMap.ArrowsUp.canceled -= StopButtonArrow;
        playerControllerEditMap.EditingMap.ArrowDown.canceled -= StopButtonArrow;
    }

    private void ButtonA(InputAction.CallbackContext context) => PressButtonA?.Invoke();
    private void ButtonB(InputAction.CallbackContext context) => PressButtonB?.Invoke();
    private void StopButtonArrow(InputAction.CallbackContext context) => StopPressArrows?.Invoke();
    private void ButtonR1(InputAction.CallbackContext context)
    {
        PressButtonR1?.Invoke();
    }

    private void ButtonL1(InputAction.CallbackContext context) => PressButtonL1?.Invoke();
    private void ButtonStart(InputAction.CallbackContext context) => PressButtonStartEditMap?.Invoke();
}
