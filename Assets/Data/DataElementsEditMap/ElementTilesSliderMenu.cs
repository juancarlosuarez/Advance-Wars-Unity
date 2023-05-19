using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/UI/UnitsEditMap/TileElement")]
public class ElementTilesSliderMenu : ScriptableObject, IElementSliderMenu
{
    public TypesOfTiles valueReference;
    public void AssignData()
    {
        Debug.Log("me cago en todos los tiles");
        var putElementsEditMap = WorldScriptableObjects.GetInstance().managerPutElementsInTheMapFromCustomMap;
        
        putElementsEditMap._tileSelected = valueReference;
        putElementsEditMap._currentTypeElementSelected = PutElementsEditMap.TypeElementGeneric.Tile;
    }
}
