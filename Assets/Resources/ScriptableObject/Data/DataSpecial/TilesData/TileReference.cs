using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Individual/Tile")]
public class TileReference : ScriptableObject
{
    public Tile reference;
    public string name;
    public void GetName()
    {
        name = reference.name;
    }
}
