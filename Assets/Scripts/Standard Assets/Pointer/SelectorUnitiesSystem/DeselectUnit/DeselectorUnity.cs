using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeselectorUnity : MonoBehaviour
{
    [SerializeField] BoolReference hadPointerUnitSelected;
    public void DeselectUnit()
    {
        hadPointerUnitSelected.boolReference = false;
        HighLightTiles.sharedInstance.TurnOffAllTiles();
    }
}
