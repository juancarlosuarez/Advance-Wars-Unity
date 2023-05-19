using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ControllerUnitsEditMap : ControllerUIMenuOptionsSlider
{
    private ControllerUIMenuSlider _uiReference;
    [SerializeField] private ManagerPositionsListRadialsMenu managerPositionLists;
    [SerializeField] private List<DataUnitEditorMapUISO> elementsDataUI;

    [SerializeField] private Dictionary<int, IElementSliderMenu[]> _dataKey;
    [SerializeField] private PutElementsEditMap _putElementsEditMap;
    
    public static event Action CloseMenu;
    public static event Action<DataUnitEditorMapUI> UpdateDataUISelectELement;

    private bool _firstRound;
    public override void Trigger()
    {
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.SelectOption);
        
        int numberSelected = managerPositionLists.GetSelectPosition();
        var dataUISelect = elementsDataUI[managerPositionLists.GetCurrentKeyDictionary()]
            .GetDataMenu[managerPositionLists.GetSelectPosition()];

        var prefabsSelected = _dataKey[managerPositionLists.GetCurrentKeyDictionary()];
        var prefab = prefabsSelected[numberSelected];
        print("Trigger in Menu Selector Units");
        _putElementsEditMap.PutItemInChamber(prefab ,dataUISelect);
        UpdateDataUISelectELement?.Invoke(dataUISelect);
    }
    public override void MoveElementsInTheSelector(bool isMovementRight)
    {
        if (!_firstRound) SoundManager._sharedInstance.PlayEffectSound(EffectNames.MoveBetweenMenus);
        else _firstRound = false;
        
        if (isMovementRight) _uiReference.MoveValuesRight();
        else _uiReference.MoveValuesLeft();
    }

    public override void ChangeElementsShowed(bool isMovementUp)
    {
        print("Change Element Inside ControllerUnitEditMap");
        if (isMovementUp) _uiReference.ChangeListElementsUp();
        else _uiReference.ChangeListElementsDown();
    }

    public override void CloseMenuByPressB()
    {
        print("CloseMenuByPressB in UnitSelector");
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.Exit);
        _uiReference.DisableCurrentElements();
        CloseMenu?.Invoke();
    }

    public override void CloseMenuByTriggerButton()
    {
        print("CloseMenuByTrigger in UnitSelector");
        _uiReference.DisableCurrentElements();
        CloseMenu?.Invoke();
    }
    public override void DisplayMenu()
    {
        
        if (_uiReference is null) _uiReference = new ControllerUIMenuSlider(elementsDataUI, managerPositionLists); 
        _uiReference.BuildMenuUI();
        _firstRound = true;

    }
}
