using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Lists/GameObjects")]
public class ListGameObjectsReference : ScriptableObject
{
    public List<GameObject> reference;
}