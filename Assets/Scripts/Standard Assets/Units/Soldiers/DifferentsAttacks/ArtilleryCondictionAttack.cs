using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Soldiers/AttackType/Artillery")]
public class ArtilleryCondictionAttack : ScriptableObject ,ICondictionAttackSoldiers
{
    [SerializeField] private BoolReference _willUnitMove;
    
    public bool CanAttack()
    {
        var tileSelected = WorldScriptableObjects.GetInstance().tileSelected.reference;
        var tileSelectedWithUnit = WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference;
        return tileSelected == tileSelectedWithUnit;
    }
}
