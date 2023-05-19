using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePlayControls : MonoBehaviour
{
    public static event Action StopPressButtonArrows;
    public static event Action StopPressButtonB;
    public static event Action StopPressSelect;
    
    public static event Action PressButtonA;
    public static event Action PressButtonB;
    public static event Action PressButtonSquare;
    public static event Action PressButtonTriangle;
    public static event Action<Vector2> PressSomeArrows;


    public static event Action<int> PressButtonDownArrow;
    public static event Action<int> PressButtonUpArrow;
    public static event Action PressButtonRightArrow;
    public static event Action PressButtonLeftArrow;
    
    public static event Action PressButtonR1;
    public static event Action PressButtonL1;

    public static event Action PressButtonStart;
    public static event Action PressButtonSelect;

    private PlayerCtrl playerControllerGamePlay;
    private void Start()
    {
        playerControllerGamePlay = PlayerController.sharedInstance.playerController;
        
        playerControllerGamePlay.GamePlay.Enable();
        
        playerControllerGamePlay.GamePlay.A.started += ButtonA;
        playerControllerGamePlay.GamePlay.B.started += ButtonB;
        playerControllerGamePlay.GamePlay.Select.started += SelectButton;
        playerControllerGamePlay.GamePlay.Select.canceled += StopSelectButton;
        playerControllerGamePlay.GamePlay.Start.started += ButtonStart;
        playerControllerGamePlay.GamePlay.R1.started += R1Button;
        playerControllerGamePlay.GamePlay.L1.started += L1Button;
        
        playerControllerGamePlay.GamePlay.ArrowDown.canceled += StopPressArrows;
        playerControllerGamePlay.GamePlay.ArrowsUp.canceled += StopPressArrows;
        playerControllerGamePlay.GamePlay.ArrowLeft.canceled += StopPressArrows;
        playerControllerGamePlay.GamePlay.ArrowRight.canceled += StopPressArrows;
        playerControllerGamePlay.GamePlay.B.canceled += StopButtonB;
    }

    private void OnDisable()
    {
        playerControllerGamePlay.GamePlay.Disable();
        
        playerControllerGamePlay.GamePlay.A.started -= ButtonA;
        playerControllerGamePlay.GamePlay.B.started -= ButtonB;
        playerControllerGamePlay.GamePlay.Select.started -= SelectButton;
        playerControllerGamePlay.GamePlay.Select.canceled -= StopSelectButton;
        playerControllerGamePlay.GamePlay.Start.started -= ButtonStart;
        playerControllerGamePlay.GamePlay.R1.started -= R1Button;
        playerControllerGamePlay.GamePlay.L1.started -= L1Button;
        
        playerControllerGamePlay.GamePlay.ArrowDown.canceled -= StopPressArrows;
        playerControllerGamePlay.GamePlay.ArrowsUp.canceled -= StopPressArrows;
        playerControllerGamePlay.GamePlay.ArrowLeft.canceled -= StopPressArrows;
        playerControllerGamePlay.GamePlay.ArrowRight.canceled -= StopPressArrows;
        playerControllerGamePlay.GamePlay.B.canceled -= StopButtonB;
    }

    private void Update()
    {
        HoldStick();
    }

    private void ButtonStart(InputAction.CallbackContext context)
    {
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.MenuStartGamePlay, 0, false);
    }

    private void R1Button(InputAction.CallbackContext context) => PressButtonR1?.Invoke();
    private void L1Button(InputAction.CallbackContext context) => PressButtonL1?.Invoke();
    private void SelectButton(InputAction.CallbackContext context) => PressButtonSelect?.Invoke();
    private void StopSelectButton(InputAction.CallbackContext context) => StopPressSelect?.Invoke();
    private void ButtonA(InputAction.CallbackContext context) => PressButtonA?.Invoke();
    private void StopButtonB(InputAction.CallbackContext context) => StopPressButtonB?.Invoke();
    private void ButtonB(InputAction.CallbackContext context) => PressButtonB?.Invoke();
    private void HoldStick()
    {
        bool arrowDown = playerControllerGamePlay.GamePlay.ArrowDown.ReadValue<float>() > 0.1f 
                         || playerControllerGamePlay.EditingMap.ArrowDown.ReadValue<float>() > 0.1f;
        bool arrowUp = playerControllerGamePlay.GamePlay.ArrowsUp.ReadValue<float>() > 0.1f 
            || playerControllerGamePlay.EditingMap.ArrowsUp.ReadValue<float>() > 0.1f;
        bool arrowRight = playerControllerGamePlay.GamePlay.ArrowRight.ReadValue<float>() > 0.1f
            || playerControllerGamePlay.EditingMap.ArrowRight.ReadValue<float>() > 0.1f;
        bool arrowLeft = playerControllerGamePlay.GamePlay.ArrowLeft.ReadValue<float>() > 0.1f
            || playerControllerGamePlay.EditingMap.ArrowLeft.ReadValue<float>() > 0.1f;

        if (!arrowDown && !arrowUp && !arrowLeft && !arrowRight) return;

        Vector2 newDir = Vector2.zero;
        if (arrowDown) newDir += Vector2.down;
        if (arrowUp) newDir += Vector2.up;
        if (arrowRight) newDir += Vector2.right;
        if (arrowLeft) newDir += Vector2.left;
        
        if (newDir == Vector2.zero) return;
        //if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) PressButtonArrows?.Invoke();
        PressSomeArrows?.Invoke(newDir);
    }
    private void StopPressArrows(InputAction.CallbackContext context) => StopPressButtonArrows?.Invoke();
}
