using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRefactorGamePlay : MonoBehaviour
{
    [SerializeField] private IntReference barrackID;
    private void OnDisable()
    {
        GamePlayControls.PressButtonA -= SelectSomething;
    }

    private void OnEnable()
    {
        GamePlayControls.PressButtonA += SelectSomething;
    }

    private void SelectSomething()
    {
        
        
        if (WorldScriptableObjects.GetInstance().hadPointerUnitSelected.boolReference)
        {
            if (!WorldScriptableObjects.GetInstance().tilesPathHighLight.reference
                    .Contains(WorldScriptableObjects.GetInstance().tileSelected.reference))
            {
                SoundManager._sharedInstance.PlayEffectSound(EffectNames.Error);
                return;
            }

            new CalculateOptionsForUnitRefactor().CalculateOptions();
            FactoryBuildersEachMenuGamePlayUI.sharedInstance.UpdateMenu(NameElementWithFramesGeneric.OptionUnits);
            FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.OptionUnits, 0, false);
        }
        else
        {
            if (WorldScriptableObjects.GetInstance().isDeleteOn.boolReference)
            {
                var soldierToDestroy = WorldScriptableObjects.GetInstance().tileSelected.reference.occupiedSoldier;
                CommandQueue.GetInstance.AddCommand(new CommandDestroyUnit(soldierToDestroy, false), false);
                return;
            }
            
            if (!TryToFindUnit())
            {
                return;
            }
            
            SelectUnit();
        }
    }

    private bool TryToFindUnit() => WorldScriptableObjects.GetInstance().tileSelected.reference.occupiedUnit;

    private bool IsUnitAlly(AbstractBaseUnit currentUnit)
    {
        if (currentUnit.playerThatCanControlThisUnit ==
            WorldScriptableObjects.GetInstance().currentPLayer.reference) return true;
        return false;
    }

    private bool IsUnitAvailableToSelect(Soldier currentSoldier) => currentSoldier.thisUnitCanMakeSomeAction;
    private void SelectUnit()
    {
        print("Unit Searched");
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.SelectUnit);
        TileRefactor selectedTile = WorldScriptableObjects.GetInstance().tileSelected.reference; 
        
        if (selectedTile.occupiedUnit.typeUnit == TypesUnit.Build) SelectBuild();
        if (selectedTile.occupiedUnit.typeUnit == TypesUnit.Soldier) SelectSoldier();
    }

    private void SelectBuild()
    {
        var tileSelected = WorldScriptableObjects.GetInstance().tileSelected.reference;
        if (IsUnitAlly(tileSelected.occupiedBuild))
        {
            if (tileSelected.occupiedBuild.typeOfBuild == TypeOfBuild.City) return;
            
            var barrackSelect = tileSelected.occupiedBuild as BarrackUnits; 
            if (barrackSelect.typeOfBuild == TypeOfBuild.Military)
            {
                CommandQueue.GetInstance.AddCommand(new CommandSelectBarrack(barrackSelect), false);
            }
            
        }
    }

    private void SelectSoldier()
    {
        var unitInTile = WorldScriptableObjects.GetInstance().tileSelected.reference.occupiedSoldier;

        if (IsUnitAlly(unitInTile) && IsUnitAvailableToSelect(unitInTile))
        {
            Debug.Log("Select Soldier Ally");
            WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference = WorldScriptableObjects.GetInstance().tileSelected.reference;
            WorldScriptableObjects.GetInstance().hadPointerUnitSelected.boolReference = true;
            DisplayRangeAvailableForWalk();
        }
    }
    private void DisplayRangeAvailableForWalk()
    {
        CommandQueue.GetInstance.AddCommand(new CommandDisplayPath(true), false);
    }
}
