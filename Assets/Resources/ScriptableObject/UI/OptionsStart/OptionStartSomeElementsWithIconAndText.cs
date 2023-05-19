using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObject/UI/OptionsStart/OptionStartSomeElementsWithIconAndText")]
public class OptionStartSomeElementsWithIconAndText : ScriptableObject
{
    [SerializeField] private List<DataOptionStartSomeElementsWithIconAndText> _data;
    [SerializeField] private String textElementIndividual;

    public List<DataOptionStartSomeElementsWithIconAndText> GetData() => _data;
    public String GetDataElementWithoutIcon => textElementIndividual;
}
[System.Serializable]
public struct DataOptionStartSomeElementsWithIconAndText
{
    public String text;
    public Sprite icon;
}


