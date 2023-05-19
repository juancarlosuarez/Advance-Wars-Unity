using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class UIControls : MonoBehaviour
{
    public static event Action<int> PressButtonDownArrow;
    public static event Action<int> PressButtonUpArrow;
    public static event Action<int> PressButtonRightArrow;
    public static event Action<int> PressButtonLeftArrow;
    
    public static event Action PressButtonA;
    public static event Action PressButtonB;
    public static event Action PressButtonR1;
    public static event Action PressButtonL1;

    private PlayerCtrl playerControllerUI;

    private void Start()
    {
        playerControllerUI = PlayerController.sharedInstance.playerController;

        playerControllerUI.UI.A.started += ButtonA;
        playerControllerUI.UI.B.started += ButtonB;
        playerControllerUI.UI.R1.started += ButtonR1;
        playerControllerUI.UI.L1.started += ButtonL1;
        
        playerControllerUI.UI.ArrowsUp.started += ButtonArrowUp;
        playerControllerUI.UI.ArrowDown.started += ButtonArrowDown;
        playerControllerUI.UI.ArrowRight.started += ButtonArrowRight;
        playerControllerUI.UI.ArrowLeft.started += ButtonArrowLeft;
    }

    private void OnDisable()
    {
        playerControllerUI.UI.A.started -= ButtonA;
        playerControllerUI.UI.A.started -= ButtonB;
        playerControllerUI.UI.R1.started -= ButtonR1;
        playerControllerUI.UI.L1.started -= ButtonL1;
        
        playerControllerUI.UI.ArrowsUp.started -= ButtonArrowUp;
        playerControllerUI.UI.ArrowDown.started -= ButtonArrowDown;
        playerControllerUI.UI.ArrowRight.started -= ButtonArrowRight;
        playerControllerUI.UI.ArrowLeft.started -= ButtonArrowLeft;
    }

    private void ButtonA(InputAction.CallbackContext context) => PressButtonA?.Invoke();
    private void ButtonB(InputAction.CallbackContext context) => PressButtonB?.Invoke();
    private void ButtonArrowUp(InputAction.CallbackContext context) => PressButtonUpArrow?.Invoke(1);
    private void ButtonArrowDown(InputAction.CallbackContext context) => PressButtonDownArrow?.Invoke(-1);
    private void ButtonArrowRight(InputAction.CallbackContext context) => PressButtonRightArrow?.Invoke(1);
    private void ButtonArrowLeft(InputAction.CallbackContext context) => PressButtonLeftArrow?.Invoke(-1);

    private void ButtonR1(InputAction.CallbackContext context)
    {
        if (StateGamePlay.GetInstance().IsStateEditingMapUI() || StateGamePlay.GetInstance().IsStateEditingMap())
        {
            print("ButtonR1");
            PressButtonR1?.Invoke();
        }
    }
    private void ButtonL1(InputAction.CallbackContext context)
    {
        if (StateGamePlay.GetInstance().IsStateEditingMapUI() || StateGamePlay.GetInstance().IsStateEditingMap())
        {
            print("ButtonL1");
            PressButtonL1?.Invoke();
        } 
    }
}
