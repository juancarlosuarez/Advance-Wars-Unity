using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DisplayDataUnits : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _display;
    [SerializeField] private GameObject _desactivable;
    private void UpdateDisplay(Sprite sprite)
    {
        _display.sprite = sprite;
    }
    private void ActivateMenu(Vector2 positionMenu)
    {
        _desactivable.transform.position = positionMenu;
        _desactivable.SetActive(true);
    }
    private void DisableMenu()
    {
        _desactivable.SetActive(false);
    }
    private void OnDisable()
    {
        ControllerBarrackUI.DisableDataUnits -= DisableMenu;
        ControllerBarrackUI.UpdateDataUnits -= UpdateDisplay;
        ControllerBarrackUI.EventDisplayDataUnits -= ActivateMenu;
    }
    private void OnEnable()
    {
        ControllerBarrackUI.DisableDataUnits += DisableMenu;
        ControllerBarrackUI.UpdateDataUnits += UpdateDisplay;
        ControllerBarrackUI.EventDisplayDataUnits += ActivateMenu;
    }
}
