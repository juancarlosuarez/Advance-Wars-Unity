using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Soldiers/Barracks/ListUnits")]
public class BarrackListUnits : ScriptableObject
{
    public List<UnitDisplay> unitList;
}
[System.Serializable]
public class UnitDisplay
{
    public Sprite unitSprite;
    public string nameUnit;
    public int goldCost;
}
