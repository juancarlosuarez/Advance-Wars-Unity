using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/UI/ActionUnit/CurrentActionUnitList")]
public class ListActionUnit : ScriptableObject
{
    public List<ActionUnit> reference;
}
