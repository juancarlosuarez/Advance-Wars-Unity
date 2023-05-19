using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/DataSpecial/TileComponent/UIData")]
public class TilesUIDataSO : ScriptableObject
{
    [field: SerializeField] public Sprite SpriteTile { get; private set;}
    [field: SerializeField] public string NameTile { get; private set; }
}
