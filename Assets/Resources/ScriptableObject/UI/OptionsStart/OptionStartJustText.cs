using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/UI/OptionsStart/JustText")] 
public class OptionStartJustText : ScriptableObject
{
    [SerializeField] private List<DataOptionStartJustText> data;

    public List<DataOptionStartJustText> GetData() => data;
}
//OptionStartSomeElementsWithIconAndText
[System.Serializable]
public class DataOptionStartJustText
{
    public String text;
}