using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/UI/UnitsEditMap/SoldierElement")]
public class ElementSoldierSliderMenu : ScriptableObject, IElementSliderMenu
{
    public UnitDataPrefab valueReference;
    public void AssignData()
    {
       var putElementsEditMap = WorldScriptableObjects.GetInstance().managerPutElementsInTheMapFromCustomMap;
        
        putElementsEditMap.unitSelected = valueReference;
        putElementsEditMap._currentTypeElementSelected = PutElementsEditMap.TypeElementGeneric.Soldier;
    }
}

[System.Serializable]
public class UnitDataPrefab : ITsSpawnableInGrid
{
    public NameUnit nameUnit;
    public FactionUnit factionUnit;
}
