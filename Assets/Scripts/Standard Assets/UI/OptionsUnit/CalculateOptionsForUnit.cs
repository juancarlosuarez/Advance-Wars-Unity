using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateOptionsForUnit : MonoBehaviour
{
    //BORRAR ESTA MIERDA PORFAVOR
    private List<MultiplesOptionsUnit> optionsCalculated;

    [SerializeField] ActionsAvailablesData actionsAvailables;
    private List<MultiplesOptionsUnit> allActionsAvailables;

    //0 es ataque, 1 es move, 2 es cancel, 4 en tomar

    private List<Tile> tilesWithUnit;
    
    public static CalculateOptionsForUnit sharedInstance;

    private List<Tile> tilesWhereBuildsIs;
    private List<Tile> tilesWhereSoldierIs;
    [Header("ScriptableObjects")]
    [SerializeField] BoolReference willUnitMove;
    [SerializeField] ListTilesReference path;
    [SerializeField] ListTilesReference tilesWhereUnitEnemyIs;
    [SerializeField] ListActionUnit currentActionList;
    [SerializeField] ScriptableAction attackAction;

    [SerializeField] TileReference tileSelected;
    [SerializeField] TileReference tileSelectedWithUnit;
    [SerializeField] ScriptableObjectPlayers currentPlayer;
    [SerializeField] ActionReference moveAction;

    [SerializeField] private SOListInt _listActionID;

    [SerializeField] private SOVector _positionMenuOptionsUnit;
    private void Awake() => sharedInstance = this;
    public void CalculeOptionsSystem(List<Tile> tilesToGetAnalysis)
    {
        Reset();

        if (SearchUnits(tilesToGetAnalysis)) WhatOptionsMyUnitHave();

        IsPathValid(path.reference);

        ShowMenu(tileSelected.reference);
    }
    private void Reset()
    {
        allActionsAvailables = actionsAvailables.options;
        optionsCalculated = new List<MultiplesOptionsUnit>();

        tilesWithUnit = new List<Tile>();
        tilesWhereUnitEnemyIs.reference = new List<Tile>();
        tilesWhereBuildsIs = new List<Tile>();

        path.reference = PathFinding.sharedInstance.FindPathDefinitive(tileSelectedWithUnit.reference, tileSelected.reference);
        _listActionID.reference.Clear();

    }
    private bool SearchUnits(List<Tile> tilesToGetAnalysis)
    {

        bool isUnitInTheTile = tileSelected.reference.occupiedBuild != null && tileSelected.reference.occupiedBuild.playerThatCanControlThisUnit != currentPlayer.reference;

        foreach (Tile tileBeingAnalyzed in tilesToGetAnalysis)
        {
            if (!tileBeingAnalyzed.occupiedUnit) continue;    
            
            isUnitInTheTile = true;
            tilesWithUnit.Add(tileBeingAnalyzed);
        }
        return isUnitInTheTile;
    }
    private void WhatOptionsMyUnitHave()
    {
        if (tileSelected.reference.occupiedBuild != null && tileSelected.reference.occupiedBuild.playerThatCanControlThisUnit != currentPlayer.reference) optionsCalculated.Add(allActionsAvailables[3]);  

        foreach (Tile tileBeingAnalyzed in tilesWithUnit)
        {
            AbstractBaseUnit unit = tileBeingAnalyzed.occupiedUnit;

            if (!IsUnitEnemy(unit)) continue;
            
            if (IsUnitSoldier(unit))
            {
                tilesWhereUnitEnemyIs.reference.Add(tileBeingAnalyzed);

                if (!optionsCalculated.Contains(allActionsAvailables[0]))
                {
                    _listActionID.reference.Add(0);
                    attackAction.reference = allActionsAvailables[0].action;
                    optionsCalculated.Add(allActionsAvailables[0]);
                }
                continue;
            }
        }
        
    }
    private bool IsUnitEnemy(AbstractBaseUnit unit)
    {
        if (unit.playerThatCanControlThisUnit != currentPlayer.reference) return true;
        return false;
    }
    private bool IsUnitSoldier(AbstractBaseUnit unit)
    {
        if (unit.typeUnit == TypesUnit.Soldier) return true;
        return false;
    }
    private void IsPathValid(List<Tile> path)
    {
        if (path == null) 
        {
            willUnitMove.boolReference = false;
            return;
        }
        willUnitMove.boolReference = true;
        optionsCalculated.Add(allActionsAvailables[1]);
        _listActionID.reference.Add(1);
        moveAction.reference = allActionsAvailables[1].action;
    }

    private void ShowMenu(Tile tileSelected)
    {
        optionsCalculated.Add(allActionsAvailables[2]);
        _listActionID.reference.Add(2);

        var positionTileSelect = tileSelected.transform.position;
        _positionMenuOptionsUnit.reference = new Vector2(positionTileSelect.x, positionTileSelect.y + 1);
        FactoryBuildersEachMenuGamePlayUI.sharedInstance.UpdateMenu(NameElementWithFramesGeneric.OptionUnits);
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.OptionUnits, 0, true);
        // ActionUnitMenu menuUnit = Instantiate(prefabMenuUnit.reference, new Vector2(tileSelected.transform.position.x, tileSelected.transform.position.y + 1), Quaternion.identity);
        // menuUnit.Init(optionsCalculated);
        // menuUnit.transform.SetParent(canvas.transform);
        //
        // currentActionList.reference = new List<ActionUnit>();
        // foreach (MultiplesOptionsUnit c in optionsCalculated) currentActionList.reference.Add(c.action);
        //
        //
        // menuController.reference = MenuController.GetInstance();
        //
        // menuController.reference.StartToControlMenu(menuUnit, false);

    }

}
