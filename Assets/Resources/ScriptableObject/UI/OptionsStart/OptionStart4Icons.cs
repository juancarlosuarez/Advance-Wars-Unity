using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/UI/OptionsStart/OptionStart4Icons")]
public class OptionStart4Icons : ScriptableObject
{
    [SerializeField] private List<DataOptionStart4Icons> _data;

    public List<DataOptionStart4Icons> GetData() => _data;
}

[System.Serializable]
public struct DataOptionStart4Icons
{
    public List<Sprite> icons;
    public List<string> texts;
}