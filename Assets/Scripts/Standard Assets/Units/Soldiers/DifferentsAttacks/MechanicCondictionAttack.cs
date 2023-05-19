using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Soldiers/AttackType/Mechanic")]
public class MechanicCondictionAttack : ScriptableObject , ICondictionAttackSoldiers
{
    public bool CanAttack() => true;
}
