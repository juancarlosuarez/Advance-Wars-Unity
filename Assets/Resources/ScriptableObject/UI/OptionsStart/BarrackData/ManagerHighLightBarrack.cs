using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/UI/OptionsStart/BarrackElements/HighLightOptions")]
public class ManagerHighLightBarrack : SerializedScriptableObject
{
    [SerializeField] private List<Dictionary<NameUnit, Sprite>> dictionaryHighLightUnits;

    public Sprite GetSprite(NameUnit nameUnit, FactionUnit currentPlayer) =>
        dictionaryHighLightUnits[(int)currentPlayer - 1][nameUnit];

}
