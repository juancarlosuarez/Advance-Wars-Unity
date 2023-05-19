using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportSoldier : Soldier
{
    public Soldier unitInsideTransport;

    public UnitsThatTransportCanTake listTransport;
    //There no needed to make a check of the unit, because is already made in the calculate of options.
    public void PutUnitInsideTransport(Soldier newUnit)
    {
        unitInsideTransport = newUnit;
        
        UnsetUnitToTheGridRefactor.UnSetUnit(unitInsideTransport);
        unitInsideTransport.gameObject.SetActive(false);
    }

    public void DropUnit(TileRefactor tileWhereWillBeDroppedUnit)
    {
        var setUnit = new SetUnitToTheGridRefactor();
        setUnit.SetUnit(tileWhereWillBeDroppedUnit, unitInsideTransport);
        unitInsideTransport.gameObject.SetActive(true);
        unitInsideTransport = null;
    }
}
