using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IntroductionControls : MonoBehaviour
{
    public static event Action PressButtonStart;
    public static event Action PressButtonA;

    private PlayerCtrl playerController;
    private void Start()
    {
        playerController = PlayerController.sharedInstance.playerController;
        
        playerController.Introduction.Enable();

        playerController.Introduction.A.started += ButtonA;
        playerController.Introduction.Start.started += ButtonStart;
    }

    private void ButtonStart(InputAction.CallbackContext context) => PressButtonStart?.Invoke();
    private void ButtonA(InputAction.CallbackContext context) => PressButtonA?.Invoke();
}
