using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/UI/UnitsEditMap/BuildElement")]
public class ElementBuildSliderMenu : ScriptableObject, IElementSliderMenu
{
    public UnitDataPrefab valueReference;
    public void AssignData()
    {
        var putElementsEditMap = WorldScriptableObjects.GetInstance().managerPutElementsInTheMapFromCustomMap;
        
        putElementsEditMap._buildSelected = valueReference;
        putElementsEditMap._currentTypeElementSelected = PutElementsEditMap.TypeElementGeneric.Build;
    }
}