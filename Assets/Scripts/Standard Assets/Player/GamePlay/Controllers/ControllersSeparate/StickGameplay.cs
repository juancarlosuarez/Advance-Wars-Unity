using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StickGameplay : MonoBehaviour
{
    public static StickGameplay sharedInstance;

    private bool canMoveSelect = true;
    private float delayToChangePositionSelect;
    private float delayToChangePositionSelectStore = .2f;
    private void Awake()
    {
        sharedInstance = this;
    }

    public void OnEnable()
    {
        GamePlayControls.StopPressButtonArrows += StickReset;
        EditMapControllers.StopPressArrows += StickReset;
        GamePlayControls.PressSomeArrows += Stick;
    }

    public void OnDisable()
    {
        GamePlayControls.StopPressButtonArrows -= StickReset;
        EditMapControllers.StopPressArrows -= StickReset;
        GamePlayControls.PressSomeArrows -= Stick;
    }
    private void Stick(Vector2 dir)
    {
        if (!(StateGamePlay.GetInstance().IsStateGamePlayActionable() || StateGamePlay.GetInstance().IsStateEditingMap())) return;
        if (canMoveSelect)
        {
          
            StartDelay();
        }
        else Chronometer();
    }
    private void StartDelay()
    {
        delayToChangePositionSelect = delayToChangePositionSelectStore;
        canMoveSelect = false;
    }
    private void Chronometer()
    {
        if (delayToChangePositionSelect > 0) delayToChangePositionSelect -= Time.deltaTime;
        else canMoveSelect = true;
    }

    public void StickReset()
    {
        if (!(StateGamePlay.GetInstance().IsStateGamePlayActionable() || StateGamePlay.GetInstance().IsStateEditingMap())) return;
        canMoveSelect = true;    
    }

}
