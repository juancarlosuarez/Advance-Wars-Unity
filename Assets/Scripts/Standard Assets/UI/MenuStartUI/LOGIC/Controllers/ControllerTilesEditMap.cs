using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTilesEditMap : ControllerUIMenuOptionsSlider
{
    
    private ControllerUIMenuSlider _uiReference;
    [SerializeField] private ManagerPositionsListRadialsMenu managerPositionLists;
    [SerializeField] private List<DataUnitEditorMapUISO> elementsDataUI;
    public List<List<IElementSliderMenu>> prefabsTilesAndBuilds;
    [SerializeField] private PutElementsEditMap _putElementsEditMap;
    

    public static event Action CloseMenu;
    public static event Action<DataUnitEditorMapUI> UpdateDataUISelectElement;
    private bool _firstRound;
    public override void Trigger()
    {
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.SelectOption);
        
        int numberSelect = managerPositionLists.GetSelectPosition();
        var key = managerPositionLists.GetCurrentKeyDictionary();
        var dataUISelect = elementsDataUI[managerPositionLists.GetCurrentKeyDictionary()]
            .GetDataMenu[managerPositionLists.GetSelectPosition()];
        var listPrefabsSelect = prefabsTilesAndBuilds[key];
        var prefabSelected = listPrefabsSelect[numberSelect];
        print("Trigger In Menu Selector Tiles");
        _putElementsEditMap.PutItemInChamber(prefabSelected, dataUISelect);
        UpdateDataUISelectElement?.Invoke(dataUISelect);
        
    }

    public override void MoveElementsInTheSelector(bool isMovementRight)
    {
        if (!_firstRound) SoundManager._sharedInstance.PlayEffectSound(EffectNames.MoveBetweenMenus);
        else _firstRound = false;
        
        if (isMovementRight) _uiReference.MoveValuesRight();
        else _uiReference.MoveValuesLeft();
    }
    public override void DisplayMenu()
    {
        if (_uiReference is null) _uiReference = new ControllerUIMenuSlider(elementsDataUI, managerPositionLists); 
        _uiReference.BuildMenuUI();
        _firstRound = true;
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
        _uiReference.DisableCurrentElements();
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.Exit);
        CloseMenu?.Invoke();
    }

    public override void CloseMenuByTriggerButton()
    {
        print("CloseMenuByTrigger in UnitSelector");
        _uiReference.DisableCurrentElements();
        CloseMenu?.Invoke();
    }
}
