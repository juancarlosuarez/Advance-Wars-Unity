using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Soldiers/TransportUnitsAvailable")]
public class UnitsThatTransportCanTake : ScriptableObject
{
    public List<NameUnit> unitsAvailable;
}
