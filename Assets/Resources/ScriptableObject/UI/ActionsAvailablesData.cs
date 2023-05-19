using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//DELETE THIS SHIT PLEASE
[CreateAssetMenu (menuName = "ScriptableObject/UIAllAction")]
public class ActionsAvailablesData : ScriptableObject
{
    public List<MultiplesOptionsUnit> options;
}
[System.Serializable]
public class MultiplesOptionsUnit
{
    public Sprite iconAction;
    public string nameAction;
    public ActionUnit action;
}
