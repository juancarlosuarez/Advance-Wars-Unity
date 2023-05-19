using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Soldiers/AttackType/Infantry")]
public class InfantryCondictionAttack : ScriptableObject ,ICondictionAttackSoldiers
{
    public bool CanAttack() => true;
}
