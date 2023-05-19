using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Data/EditorMap/ElementSelected")]
public class SOElementSelectedEditorMap : ScriptableObject
{
    public Sprite spriteElementSelected;
    public string nameElementSelected;

    public Tile tilePrefab;
    public AbstractBaseUnit unitPrefab;

    public bool elementIsATile;
    public void SetValuesElementMenuSelectorTile(ElementsTileSelectorMenu element)
    {
        spriteElementSelected = element.GetSpriteElement;
        nameElementSelected = element.GetNameElement;

        tilePrefab = element.GetTilePrefab();
        elementIsATile = true;
        //else unitPrefab = element.GetComponent<AbstractBaseUnit>();
    }

    public void SetValuesElementMenuSelectorUnit(ElementsUnitSelectorMenu element)
    {
        spriteElementSelected = element.GetSpriteUnit();
        nameElementSelected = element.GetNameUnit();

        unitPrefab = element.GetUnitPrefab();
        elementIsATile = false;
    }
}

