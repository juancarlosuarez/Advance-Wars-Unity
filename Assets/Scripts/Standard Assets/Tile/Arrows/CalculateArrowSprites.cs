using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateArrowSprites
{
    
    public void Calculate(List<TileRefactor> tilesWhereArrowsAre)
    {
        List<int> idArrows = new List<int>();

        //ShowArrows.sharedInstance.StopShowArrows();

        for (int i = 0; i < tilesWhereArrowsAre.Count; i++)
        {
            Vector2 previousTile = i > 0 ? tilesWhereArrowsAre[i - 1].positionTileInGrid : WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference.positionTileInGrid;
            Vector2 futureTile = i < tilesWhereArrowsAre.Count - 1 ? tilesWhereArrowsAre[i + 1].positionTileInGrid : Vector2.zero;
            Vector2 currentTile = tilesWhereArrowsAre[i].positionTileInGrid;
            int idArrowNeed = (int)ArrowsTranslator.GetInstance().CalculateArrow(previousTile, currentTile, futureTile);
            idArrows.Add(idArrowNeed);
        }
        
        WorldScriptableObjects.GetInstance().allTilesWHereMyArrowsWillBe.reference = new List<TileRefactor>();
        WorldScriptableObjects.GetInstance().allTilesWHereMyArrowsWillBe.reference = tilesWhereArrowsAre;

        WorldScriptableObjects.GetInstance().idsArrows.reference = idArrows;

        //ShowArrows.sharedInstance.SetSpriteArrow(idArrows, allTilesWhereMyArrowIs);
    }
}

public class CalculateTilesArrowPath
{
    private List<TileRefactor> allTilesWhereMyArrowIs;
    private TileRefactor _tileWithUnit;
    private TileRefactor _tileSelected;
    
    public void Calculate(TileRefactor tileWithUnit, TileRefactor tileSelected, ref bool willArrowAppear)
    {
        _tileWithUnit = tileWithUnit;
        var movesAvailable = tileWithUnit.occupiedSoldier.numberMoveAvailable + 1;
        _tileSelected = tileSelected;


        if (tileSelected.occupiedSoldier != null)
        {
            var isUnitALly = _tileSelected.occupiedSoldier.playerThatCanControlThisUnit ==
                             tileWithUnit.occupiedSoldier.playerThatCanControlThisUnit;
            var isUnitTransport = tileSelected.occupiedSoldier.typeSoldier == TypeSoldier.Transport;
            if (isUnitALly && !isUnitTransport)
            {
                willArrowAppear = false;
                return;
            }
        }

        if (tileSelected.occupiedBuild != null)
        {
            
        }
        allTilesWhereMyArrowIs = new List<TileRefactor>();
        
        //if (IsFirstArrowTogether()) return;
        WorldScriptableObjects.GetInstance().allTilesWHereMyArrowsWillBe.reference =
            PathFindingRefactor.GetInstance().StartCalculatePath(_tileWithUnit, _tileSelected, movesAvailable);
        //if (IsMyNewTileInTheListTile()) FixArrow();
        //else AddArrowToList();
        //AddArrowToList();
    }

    private bool IsFirstArrowTogether()
    {
        if (_tileSelected.NeighboursInCross.Contains(_tileWithUnit)) return true;
         //WorldScriptableObjects.GetInstance().allTilesWHereMyArrowsWillBe.reference =
             //PathFindingRefactor.GetInstance().StartCalculatePath(_tileWithUnit, _tileSelected);
        return false;
    }
    private bool IsMyNewTileInTheListTile() => allTilesWhereMyArrowIs.Contains(_tileSelected);

    private void AddArrowToList()
    {
        allTilesWhereMyArrowIs.Add(_tileSelected);

        List<TileRefactor> offsetPath = OffsetPath(allTilesWhereMyArrowIs);
        WorldScriptableObjects.GetInstance().allTilesWHereMyArrowsWillBe.reference = offsetPath;
    }

    private List<TileRefactor> OffsetPath(List<TileRefactor> checkingTiles)
    {
        //We add +1 because, the arrow go from the unit, if you change that, just remove + 1
        int amountStepsAvailable = _tileWithUnit.occupiedSoldier.numberMoveAvailable + 1;

        foreach (var c in allTilesWhereMyArrowIs)
        {
            amountStepsAvailable -= c.dataVariable.ammountEffortToPass;
            if (amountStepsAvailable < 0)
            {
                return PathFindingRefactor.GetInstance().StartCalculatePath(_tileWithUnit, _tileSelected, _tileWithUnit.occupiedSoldier.numberMoveAvailable + 1);
            }
        }
        return checkingTiles;
    }

    private void FixArrow()
    {
        List<TileRefactor> tileListCutting = new List<TileRefactor>();
        foreach (var tile in allTilesWhereMyArrowIs)
        {
            tileListCutting.Add(tile);
            if (tile == _tileSelected) break;
        }
        WorldScriptableObjects.GetInstance().allTilesWHereMyArrowsWillBe.reference = tileListCutting;
    }
}

public class CalculatePathArrow
{
    private CalculateTilesArrowPath _calculateTilesArrowPath;
    private CalculateArrowSprites _calculateArrowSprites;
    private PoolArrowsPath _poolArrowsPath;

    private WorldScriptableObjects _WSO;
    private static CalculatePathArrow _sharedInstance;

    public static CalculatePathArrow GetInstance() => _sharedInstance ??= new CalculatePathArrow();
    
    private void OnApplicationQuit()
    {
        PlayerController.ResetEventsNoMonobehavior -= OnApplicationQuit;
        _sharedInstance = null;
    }

    private void Reset()
    {
        CommandChangeScene.ChangeScene -= Reset;
        _sharedInstance = null;
    }
    private CalculatePathArrow()
    {
        CommandChangeScene.ChangeScene += Reset;
        PlayerController.ResetEventsNoMonobehavior += OnApplicationQuit;
        _calculateTilesArrowPath = new CalculateTilesArrowPath();
        _calculateArrowSprites = new CalculateArrowSprites();
        
        _poolArrowsPath = new PoolArrowsPath();
        _poolArrowsPath.ResetValues();
        
        _WSO = WorldScriptableObjects.GetInstance();
    }
    public void GetArrowPath()
    {
        if (!_WSO.hadPointerUnitSelected.boolReference) return;
        bool arrowWillAppear = true;
        
        _calculateTilesArrowPath.Calculate(_WSO.tileSelectedWithUnit.reference, _WSO.tileSelected.reference, ref arrowWillAppear);

        if (!arrowWillAppear)
        {
            StopShowArrows();
            return;
        }
        _calculateArrowSprites.Calculate(_WSO.allTilesWHereMyArrowsWillBe.reference);
        StopShowArrows();

        for (int i = 0; i < _WSO.allTilesWHereMyArrowsWillBe.reference.Count; i++)
        {
            Vector2 positionArrow = _WSO.allTilesWHereMyArrowsWillBe.reference[i].positionTileInGrid;
            var idArrow = _WSO.idsArrows.reference[i];
            
            _poolArrowsPath.SpawnFromThePool((ArrowsTranslator.ArrowDirection)idArrow, positionArrow);
        }
    }

    public void StopShowArrows()
    {
        _poolArrowsPath.PutOutsideElementsBackToThePool();
    }
    
}




