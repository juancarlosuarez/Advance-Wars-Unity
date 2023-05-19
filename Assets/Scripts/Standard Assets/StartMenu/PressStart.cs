using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressStart : MonoBehaviour
{
    [SerializeField] private GameObject _elementsDesactivable;

    private void Start()
    {
        PlayerController.sharedInstance.ChangeControlToIntroduction();
        SoundManager._sharedInstance.PlayMusic(MusicNames.IntroductionSong);
    }

    private void ActiveIntroduction()
    {
        PlayerController.sharedInstance.ChangeControlToIntroduction();
        _elementsDesactivable.SetActive(true);
    }
    private void PressStartOrA()
    {
        CommandQueue.GetInstance.AddCommand(new CommandTest(), false);
        DisableUI();
        ActiveMenuSelectMap();
    }
    private void DisableUI()
    {
        _elementsDesactivable.SetActive(false);
    }
    private void ActiveMenuSelectMap()
    {
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.SelectModeGame, 0, true);
    }
    private void OnDisable()
    {
        IntroductionControls.PressButtonA -= PressStartOrA;
        IntroductionControls.PressButtonStart -= PressStartOrA;
        ControllerMainMenu.BackControllerSelectMap -= ActiveIntroduction;
    }
    private void OnEnable()
    {
        IntroductionControls.PressButtonA += PressStartOrA;
        IntroductionControls.PressButtonStart += PressStartOrA;
        ControllerMainMenu.BackControllerSelectMap += ActiveIntroduction;
    }
    
}
