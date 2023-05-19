using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class DropOptionCalculate : IOptionsUnitConditions
{
    private Soldier _unitSelected;
    public OptionsUnit GetOptionAssociated() => OptionsUnit.Drop;
    public bool DoesOptionMeetCondition()
    {
        _unitSelected = WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference.occupiedSoldier;

        if (!ThereIsUnitInTile()) return false;
        if (!IsUnitSelectedTransport()) return false;
        if (!ThereIsUnitInTransport()) return false;
        if (!TheresTileAvailable()) return false;

        return true;
    }

    private bool ThereIsUnitInTile() => _unitSelected;
    private bool IsUnitSelectedTransport() => _unitSelected.typeSoldier == TypeSoldier.Transport;
    private bool ThereIsUnitInTransport()
    {
        var transport = (TransportSoldier)_unitSelected;
        return transport.unitInsideTransport != null;
    }

    private bool TheresTileAvailable()
    {
        var transport = (TransportSoldier)_unitSelected;
        var tileWhereTransportWillBe = WorldScriptableObjects.GetInstance().tileSelected.reference;
        bool theresTileAvailable = false;
        WorldScriptableObjects.GetInstance().tilesWhereUnitCanBeDispose.reference = new List<TileRefactor>();
        foreach (var tile in tileWhereTransportWillBe.NeighboursInCross)
        {
            bool unitDroppedCanTouchTile =
                transport.unitInsideTransport.terrainUnitCanTransit.Contains(tile.dataVariable.terrainTypes);
            bool isTileOccupied = tile.occupiedSoldier;
            bool isTileOccupiedByTransport = transport.occupiedTileRefactor == tile;
            if (unitDroppedCanTouchTile && (!isTileOccupied || isTileOccupiedByTransport))
            {
                WorldScriptableObjects.GetInstance().tilesWhereUnitCanBeDispose.reference.Add(tile);
                theresTileAvailable = true;
            }
        }
        return theresTileAvailable;
    }
}
