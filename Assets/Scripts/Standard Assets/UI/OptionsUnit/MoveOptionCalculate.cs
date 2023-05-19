using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOptionCalculate : IOptionsUnitConditions
{
    private readonly TileRefactor _selectedTile;
    private readonly TileRefactor _selectedTileWithUnit;
    private readonly BoolReference _willUnitMove;

    public MoveOptionCalculate()
    {
        _selectedTile = WorldScriptableObjects.GetInstance().tileSelected.reference;
        _selectedTileWithUnit = WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference;
        _willUnitMove = Resources.Load<BoolReference>("ScriptableObject/Data/BoolData/WillUnitMove");
    }
    public OptionsUnit GetOptionAssociated() => OptionsUnit.Move;
    
    public bool DoesOptionMeetCondition()
    {
        _willUnitMove.boolReference = false;

        //I make this, because the transport system need to have this variable like true in case that the unit will move,
        //but just in the case that i use the move command, this way i get sure that the player cant move the unit inside another
        _willUnitMove.boolReference = IsUnitAllyATransport();
        if (TileSelectedIsTheSame()) return false;
        if (TileSelectedHadUnitAlly()) return false;
        
        _willUnitMove.boolReference = true;
        return true;
    }

    private bool TileSelectedIsTheSame() => _selectedTile == _selectedTileWithUnit;
    private bool TileSelectedHadUnitAlly()
    {
        if (_selectedTile.occupiedSoldier == null) return false;

        if (_selectedTile.occupiedSoldier.playerThatCanControlThisUnit ==
            _selectedTileWithUnit.occupiedSoldier.playerThatCanControlThisUnit) return true;
        return false;
    }

    private bool IsUnitAllyATransport()
    {
        if (_selectedTile.occupiedSoldier == null) return false;
        
        var currentPlayer = WorldScriptableObjects.GetInstance().currentPLayer.reference;
        
        if (_selectedTile.occupiedSoldier.typeSoldier == TypeSoldier.Transport &&
            _selectedTile.occupiedSoldier.playerThatCanControlThisUnit == currentPlayer) return true;
        
        return false;
    }
}
