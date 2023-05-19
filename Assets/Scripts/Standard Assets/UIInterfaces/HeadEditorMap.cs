using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadEditorMap : MonoBehaviour
{
    [SerializeField] private GameObject _desactivable;

    [SerializeField] private NumberSet horizontalTilePosition;
    [SerializeField] private NumberSet verticalTilePosition;

    [SerializeField] private SetSpritesCanvas spriteElementSelect;
    [SerializeField] private SetString nameElementSelect;
    [SerializeField] private PutElementsEditMap _putElementsEditMap;

    [Header("Scriptable Objects")] [SerializeField]
    private SOElementSelectedEditorMap elementSelected;

    private void OnDisable()
    {
        ControllerUnitsEditMap.UpdateDataUISelectELement -= UpdateCounterElementSelected;
    }

    private void OnEnable()
    {
        ControllerUnitsEditMap.UpdateDataUISelectELement += UpdateCounterElementSelected;
    }

    public void OpenInterface()
    {
        
        var positionTile = WorldScriptableObjects.GetInstance().tileSelected.reference.positionTileInGrid;
        _desactivable.SetActive(true);
        //Esto debo cambiarlo, no tengo porque tener acceso a esta clase, cuando me interesan el sprite y el nombre
        UpdateHorizontalVerticalElement(positionTile);
        UpdateCounterElementSelected(_putElementsEditMap.GetDataUIElement());
    }
    public void UpdateData()
    {
        if (!_desactivable.activeSelf) return;

        var positionTile = WorldScriptableObjects.GetInstance().tileSelected.reference.positionTileInGrid;
        UpdateHorizontalVerticalElement(positionTile);        
    }
    public void UpdateHorizontalVerticalElement(Vector2 positionTile)
    {
        horizontalTilePosition.UpdateData((int)positionTile.x);
        verticalTilePosition.UpdateData((int)positionTile.y);
    }

    private void UpdateCounterElementSelected(DataUnitEditorMapUI dataUI)
    {
        spriteElementSelect.UpdateData(dataUI.sprite);
        nameElementSelect.UpdateData(dataUI.name.ToString());
    }
    public void StopDisplayElementsEditorMenu()
    {
        _desactivable.SetActive(false);
    }
    //Para cambiar la parte de arriba usare un evento que notifique cuando el selector se ha movido exitoxamente
    //Para la otro parte es algo mas dedicado...
}
