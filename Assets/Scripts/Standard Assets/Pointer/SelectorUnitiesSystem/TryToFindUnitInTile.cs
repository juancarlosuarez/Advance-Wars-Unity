using System.Collections;
using UnityEngine;


public class TryToFindUnitInTile
{
    private Tile tileSelected;
    public TryToFindUnitInTile(Tile _tileSelected)
    {
        tileSelected = _tileSelected;    
    }
    public AbstractBaseUnit System()
    {
        if (tileSelected.occupiedUnit != null) return tileSelected.occupiedUnit;
        return null;
    }
//DIOS BORRA ESTA MIERDA
}