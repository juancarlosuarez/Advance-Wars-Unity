using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeBuildOption : IOptionsUnitConditions
{
    public OptionsUnit GetOptionAssociated() => OptionsUnit.Take;
    private TileRefactor _tileSelected;
    private TileRefactor _tileSelectedWithUnit;
    
    public bool DoesOptionMeetCondition()
    {
        _tileSelected = WorldScriptableObjects.GetInstance().tileSelected.reference;
        _tileSelectedWithUnit = WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference;

        if (HasTileSelectedUnit()) return false;
        if (!HasTileSelectedBuild()) return false;
        if (!IsUnitSelectCapableToConquer()) return false;
        if (!IsThisBuildConquerable()) return false;

        return true;
    }

    private bool HasTileSelectedUnit() => _tileSelected.occupiedSoldier &&
                                          _tileSelectedWithUnit.occupiedSoldier != _tileSelected.occupiedSoldier;
    private bool HasTileSelectedBuild() => _tileSelected.occupiedBuild;

    private bool IsThisBuildConquerable() => _tileSelectedWithUnit.occupiedSoldier.playerThatCanControlThisUnit !=
                                             _tileSelected.occupiedBuild.playerThatCanControlThisUnit;

    private bool IsUnitSelectCapableToConquer() =>
        _tileSelectedWithUnit.occupiedSoldier.typeSoldier == TypeSoldier.Infantry;




}
