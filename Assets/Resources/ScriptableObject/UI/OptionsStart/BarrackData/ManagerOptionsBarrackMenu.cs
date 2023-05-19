using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/UI/OptionsStart/BarrackElements/ManagerOptions")]
public class ManagerOptionsBarrackMenu : SerializedScriptableObject
{
    [SerializeField] Dictionary<byte, OptionBarrackMenuUI> _dictionaryWithOptions;

    public OptionBarrackMenuUI GetOptionBarrackUnit(byte keyForDictionary)
    {
        var option = _dictionaryWithOptions[keyForDictionary];
        return option;
    }

    public int CountDictionary() => _dictionaryWithOptions.Count;
}
