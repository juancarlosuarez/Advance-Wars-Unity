using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
public class ChangePlayerWhoControlUnit
{
    //private ScriptableObjectPlayers currentPlayer;

    public ChangePlayerWhoControlUnit(AbstractBaseUnit unit, Tile tileSelected)
    {
        var currentPlayer = Resources.Load<ScriptableObjectPlayers>("ScriptableObject/Data/DataSpecial/Player/CurrentPlayer");
        ChangePlayerWhoControlThisUnit(unit, tileSelected, currentPlayer);
    }

    public void ChangePlayerWhoControlThisUnit(AbstractBaseUnit unit, Tile tileSelected, ScriptableObjectPlayers currentPlayer)
    {
        
        var dictionaryOfUnits = Resources.Load<SOListUnitsPrefabs>("ScriptableObject/Data/DataSpecial/ListUnitsPrefabs/DictionaryUnits");

        var unitDictionaryArray = dictionaryOfUnits.GetDictionaryOfUnits()[unit.nameUnit];

        var unitSelect = unitDictionaryArray[(int)currentPlayer.reference];

        Object.Destroy(unit.gameObject);

        new SpawnerUnit(tileSelected, unitSelect);
        //var listUnit = unit.prefabsForThisUnit.InstantiateAsync();
        
        
        
        //var dictionary = listUnit.GetReference();

        //var prefabToSpawn = dictionary[(int)currentPlayer.reference];
        //var prefabToSpawn = dictionary[4];

        //Object.Destroy(unit);

        //var prefab = new SpawnerUnit(tileSelected, (BaseUnit)prefabToSpawn);
        //prefab.InstantiateUnitInBarrack();        

    }

}
