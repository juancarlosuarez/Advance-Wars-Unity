using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/UI/UnitsEditMap/Delete")]
public class ElementDeleteSliderMenu : ScriptableObject, IElementSliderMenu
{
    
    public void AssignData()
    {
        var putElementsEditMap = WorldScriptableObjects.GetInstance().managerPutElementsInTheMapFromCustomMap;
        
        putElementsEditMap._currentTypeElementSelected = PutElementsEditMap.TypeElementGeneric.Delete;
    }
}
