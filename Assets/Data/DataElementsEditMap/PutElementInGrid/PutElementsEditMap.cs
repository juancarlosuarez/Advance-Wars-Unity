using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

[CreateAssetMenu(menuName = "ScriptableObject/UI/UnitsEditMap/Selector")]
public class PutElementsEditMap : ScriptableObject
{
    public TypeElementGeneric _currentTypeElementSelected;
    public UnitDataPrefab unitSelected;
    public UnitDataPrefab _buildSelected;
    public TypesOfTiles _tileSelected;
    [SerializeField] DataUnitEditorMapUI _dataUI;
    public enum TypeElementGeneric
    {
        Soldier = 0, Build = 1, Tile = 2, Delete = 3
    }
    public void PutItemInChamber(IElementSliderMenu element, DataUnitEditorMapUI dataUI)
    {
        //_elementChoose = newElement;
        element.AssignData();
        
        _dataUI = dataUI;
    }
    public void PutElementInGrid()
    {
        switch (_currentTypeElementSelected)
        {
            case TypeElementGeneric.Soldier: PutElementSoldierInGrid(); 
                break;
            case TypeElementGeneric.Build: PutElementBuildInGrid(); 
                break;
            case TypeElementGeneric.Tile: PutElementTileInGrid();
                break;
            case TypeElementGeneric.Delete: DeleteInGridUnit();
                break;
        }
    }
    public DataUnitEditorMapUI GetDataUIElement() => _dataUI;
    
    private void PutElementSoldierInGrid()
    {
        Debug.Log("Try Put Soldier In Grid");
        var spawnerUnit = new SpawnerUnitRefactor(100);
        var tileSelected = WorldScriptableObjects.GetInstance().tileSelected.reference;
        Debug.Log(unitSelected.nameUnit.ToString());
        spawnerUnit.PutElement(unitSelected, tileSelected);
    }
    private void PutElementBuildInGrid()
    {
        Debug.Log("Try Put Build In Grid");
        var spawnerBuild = new SpawnerBuilds();
        var tileSelected = WorldScriptableObjects.GetInstance().tileSelected.reference;
        
        spawnerBuild.PutElement(_buildSelected, tileSelected);
    }
    private void PutElementTileInGrid()
    {
        Debug.Log("Try Put Tile In Grid");
        var tileSelected = WorldScriptableObjects.GetInstance().tileSelected.reference;
        WorldScriptableObjects.GetInstance().builderTile.BuildTile(_tileSelected, tileSelected.spawnID, tileSelected.positionTileInGrid);
    }

    private void DeleteInGridUnit()
    {
        var tileSelected = WorldScriptableObjects.GetInstance().tileSelected.reference;
        if (tileSelected.occupiedUnit != null)
        {
            Debug.Log("borro unidad");
            Destroy(tileSelected.occupiedUnit.gameObject);
            
            UnsetUnitToTheGridRefactor.CleanReferences(tileSelected);
        }
    }
}
public interface ISpawnElementInTheGrid
{
    public void PutElement<T>(T elementThatITryToPutInTheGrid, TileRefactor tileWhereIWillPutIt) where T : ITsSpawnableInGrid;
    public bool ElementAccomplishRequirement();
}
public interface ITsSpawnableInGrid
{ }

public interface IElementSliderMenu
{
    public void AssignData();
}