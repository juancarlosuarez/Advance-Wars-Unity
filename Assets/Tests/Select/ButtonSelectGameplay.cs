using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelectGameplay : MonoBehaviour
{
    [SerializeField] private GameObject controllerSprite;
    private void OnDisable()
    {
        GamePlayControls.PressButtonSelect -= DisplayController;
        GamePlayControls.StopPressSelect -= StopDisplayController;
    }

    private void OnEnable()
    {
        GamePlayControls.PressButtonSelect += DisplayController;
        GamePlayControls.StopPressSelect += StopDisplayController;
    }

    private void DisplayController()
    {
        controllerSprite.SetActive(true);
    }

    private void StopDisplayController()
    {
        controllerSprite.SetActive(false);
    }
}
