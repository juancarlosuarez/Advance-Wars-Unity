using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObject/DataSpecial/Tiles/DataManager")]
public class TileDataManagerSO : SerializedScriptableObject
{
    [SerializeField] Dictionary<TypesOfTiles, VariableDataTileSO> DictionaryTerrainData =
        new Dictionary<TypesOfTiles, VariableDataTileSO>();



    private Dictionary<TypesOfTiles, TileRefactor> DictionaryTiles;
    
    
    //This class build all the data, when its finish give me access to the reference with the information,
    //for build each new Tile required.

    private VariableDataTileSO _currentDataVariable;
    public VariableDataTileSO GetTerrainData(TypesOfTiles type)
    {
        return DictionaryTerrainData[type];
    }


    private void BuildTile()
    {
        
    }

    private void ValuesTerrain()
    {
        
    }

    private void ValuesNodeBase()
    {
        
    }
}