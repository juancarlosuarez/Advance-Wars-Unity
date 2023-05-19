using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/UI/OptionsStart/TextAndIcon")] 
public class OptionStartTextAndIcon : ScriptableObject
{
    [SerializeField] private List<DataOptionStartTextAndIcon> _data;

    public List<DataOptionStartTextAndIcon> GetData() => _data;
}
[System.Serializable]
public struct DataOptionStartTextAndIcon
{
    public String text;
    public Sprite icon;
}