using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Soldiers/AttackType/Transport")]
public class TransportCondictionAttack : ScriptableObject ,ICondictionAttackSoldiers
{
    public bool CanAttack() => false;
}
