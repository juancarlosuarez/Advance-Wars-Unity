using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllerUIMenuSlider
{
    private GameObject parentMenu;
    private readonly List<DataUnitEditorMapUISO> _elementsData;
    private ManagerPositionsListRadialsMenu _managerPositionsList;
    
    private readonly Dictionary<int, List<GameObject>> _allElementsMenu = new Dictionary<int, List<GameObject>>();
    private List<Vector2> _positionElements;

    private bool _isAlreadySpawnedThisMenu = false;

    private int _localPosition;
    private int LocalPosition
    {
        get => _localPosition;
        set
        {
            int maxValueForList = _managerPositionsList.GetMaxValueForList;
            
            if (value <= maxValueForList && value >= 0) _localPosition = value;
            if (value > maxValueForList) _localPosition = 0;
            if (value < 0)
            {
                _localPosition = maxValueForList;
            }
        }
    }
    public ControllerUIMenuSlider(List<DataUnitEditorMapUISO> elementsData, ManagerPositionsListRadialsMenu managerPositionList)
    {
        _elementsData = elementsData;
        _managerPositionsList = managerPositionList;
        managerPositionList.ResetValues();
    }
    public void BuildMenuUI()
    {
        if (!_isAlreadySpawnedThisMenu) GetValuesForMenu();
        UpdateElements();
        //PutMenuInFrame();
    }

    private void GetValuesForMenu()
    {
        var builderElementsUI = new BuilderElementsUnitEditMap();
        _isAlreadySpawnedThisMenu = true;
        var canvas = GameObject.Find("Canvas");
        parentMenu = new GameObject("SliderMenu");
        parentMenu.SetParent(canvas);
        _positionElements = builderElementsUI.GetPositionElements();
        for (int player = 0; player < 5; player++)
        {
            var elements = builderElementsUI.MakeAllElements(_elementsData[player], parentMenu);
            _allElementsMenu.Add(player, elements);
        }
    }
    private void UpdateElements()
    {
        var builderElementsUI = new BuilderElementsUnitEditMap();
        _positionElements = builderElementsUI.GetPositionElements();
        LocalPosition = _managerPositionsList.GetInitPosition();
        for (int i = 0; i < 5; i++)
        {
            var listWhereIWillGetMyElement = _allElementsMenu[_managerPositionsList.GetCurrentKeyDictionary()];
            var element = listWhereIWillGetMyElement[_localPosition];
            element.SetActive(true);
            element.transform.position = _positionElements[i];
            
            LocalPosition++;
        }
    }

    private void PutMenuInFrame()
    {
        var cameraPosition = Camera.main.gameObject.transform.position;
        var offset = new Vector2(-2, -2);
        Debug.Log("PutMenuInFramehutneaohenothuatnoezpuigch4zpvygcrhizpypyeu");
    
        parentMenu.transform.position = new Vector2(cameraPosition.x + offset.x, cameraPosition.y + offset.y);
    }
    public void DisableCurrentElements()
    {
        foreach (var element in _allElementsMenu[_managerPositionsList.GetCurrentKeyDictionary()]) element.SetActive(false);
    }
    public void MoveValuesRight()
    {
        DisableCurrentElements();
        _managerPositionsList.IncreaseValues();
        UpdateElements();
    }
    public void MoveValuesLeft()
    {
        DisableCurrentElements();
        _managerPositionsList.DecreaseValues();
        UpdateElements();
    }
    public void ChangeListElementsUp()
    {
        DisableCurrentElements();
        _managerPositionsList.ChangeKeyDictionary(true);
        UpdateElements();
    }
    public void ChangeListElementsDown()
    {
        DisableCurrentElements();
        _managerPositionsList.ChangeKeyDictionary(false);
        UpdateElements();
    }
}

