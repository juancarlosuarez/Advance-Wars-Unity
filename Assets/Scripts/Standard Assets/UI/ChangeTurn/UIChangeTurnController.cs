using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIChangeTurnController : MonoBehaviour
{
    [SerializeField] private RectTransform letterD;
    [SerializeField] private RectTransform letterA;
    [SerializeField] private RectTransform letterY;
    [SerializeField] private TextMeshProUGUI numberOfDaysText;
    [SerializeField] private GameObject desactivable;

    private int _numberOfDays;
    private int _countPlayerTurn;
    private int _countMaxPlayerTurn = 2;
    private void Start()
    {
        var currentMap = WorldScriptableObjects.GetInstance()._currentMapDisplayData;
        _numberOfDays = currentMap.currentDays;
        _countPlayerTurn = currentMap.countPlayerTurn;
        numberOfDaysText.text = _numberOfDays.ToString();
    }
    private void StartAnimation()
    {
        print("Start ANimation");
        desactivable.SetActive(true);
        PlayerController.sharedInstance.StopControls();
        GetResetScaleElements();
        SetCorrectPosition();
        CountPlayers();
        
        CommandQueue.GetInstance.AddCommand(new VanishingImageHorizontally(letterD), true);
        CommandQueue.GetInstance.AddCommand(new VanishingImageHorizontally(letterA), true);
        CommandQueue.GetInstance.AddCommand(new VanishingImageHorizontally(letterY), true);
        CommandQueue.GetInstance.AddCommand(new VanishingImageHorizontally(numberOfDaysText.rectTransform), true);

        CommandNotifyFinish.FinishNotification += FinishAnimation;
        CommandQueue.GetInstance.AddCommand(new CommandNotifyFinish(), false);
    }

    private void GetResetScaleElements()
    {
        letterD.localScale = Vector3.one;
        letterA.localScale = Vector3.one;
        letterY.localScale = Vector3.one;
        numberOfDaysText.rectTransform.localScale = Vector3.one;
    }

    private void FinishAnimation()
    {
        CommandNotifyFinish.FinishNotification -= FinishAnimation;
        PlayerController.sharedInstance.ChangeControlToGamePlay();
        desactivable.SetActive(false);
    }
    private void CountPlayers()
    {
        if (_countPlayerTurn == _countMaxPlayerTurn)
        {
            _countPlayerTurn = 1;
            _numberOfDays++;
            numberOfDaysText.text = _numberOfDays.ToString();
            UpdateDataInGlobalReference();
            return;
        }
        _countPlayerTurn++;
        UpdateDataInGlobalReference();
    }

    private void UpdateDataInGlobalReference()
    {
        var currentMap = WorldScriptableObjects.GetInstance()._currentMapDisplayData;
        currentMap.currentDays = _numberOfDays;
        currentMap.countPlayerTurn = _countPlayerTurn;
    }
    private void SetCorrectPosition()
    {
        var positionCamera = Camera.main.gameObject.transform.position;
        var offsetX = 2;
        var offsetY = 2;
        var positionX = positionCamera.x + offsetX;
        var positionY = positionCamera.y - offsetY;
        desactivable.transform.position = new Vector3(positionX, positionY, desactivable.transform.position.z);
    }
    private void OnDisable()
    {
        CommandChangeTurn.ChangeTurnUI -= StartAnimation;
        CommandNotifyFinish.FinishNotification -= FinishAnimation;
    }

    private void OnEnable()
    {
        CommandChangeTurn.ChangeTurnUI += StartAnimation;
    }
}
