using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Data/Lists/DictionaryTiles")]
public class DictionaryTiles : SerializedScriptableObject
{
    public Dictionary<int, TileRefactor> reference = new Dictionary<int, TileRefactor>();
}
