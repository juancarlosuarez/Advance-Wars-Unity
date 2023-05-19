using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class LoadOption : IOptionsUnitConditions
{
    private TileRefactor _tileSelected;
    private TileRefactor _tileWithTheUnitThatTryToLoad;
    public OptionsUnit GetOptionAssociated() => OptionsUnit.Load;
    public bool DoesOptionMeetCondition()
    {
        _tileSelected = WorldScriptableObjects.GetInstance().tileSelected.reference;
        _tileWithTheUnitThatTryToLoad = WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference;
        
        if (ThereIsUnitInTile() is false) return false;
        if (!IsUnitTransport()) return false;
        if (IsUnitInsideTransport()) return false;
        if (!IsUnitThatTryToLoadAvailable()) return false;
        
        return true;
    }
    private bool ThereIsUnitInTile() => _tileSelected.occupiedSoldier;
    private bool IsUnitTransport() => _tileSelected.occupiedSoldier.typeSoldier == TypeSoldier.Transport;

    private bool IsUnitInsideTransport()
    {
        var transport = _tileSelected.occupiedSoldier as TransportSoldier;
        return transport.unitInsideTransport != null;
    }
    private bool IsUnitThatTryToLoadAvailable()
    {
        var transport = _tileSelected.occupiedSoldier as TransportSoldier;
        return transport != null && transport.listTransport.unitsAvailable.Contains(_tileWithTheUnitThatTryToLoad.occupiedSoldier.nameUnit);
    }
}
