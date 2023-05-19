using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/UI/OptionsStart/BarrackElements/Elements")]
public class OptionBarrackMenuUI : ScriptableObject
{
    [SerializeField] private List<DataOptionBarrackMenu> _data;

    public List<DataOptionBarrackMenu> GetData()
    {
        var copyList = _data;
        return copyList;
    } 
}
[System.Serializable]
public struct DataOptionBarrackMenu
{
    public Sprite spriteSoldier;
    public NameUnit nameUnit;
    public FactionUnit faction;
    public int goldCost;
}
