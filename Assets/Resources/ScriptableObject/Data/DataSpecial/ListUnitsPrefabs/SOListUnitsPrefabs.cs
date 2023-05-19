using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Lists/GroupPrefabsUnit")]
public class SOListUnitsPrefabs : ScriptableObject
{
    private Dictionary<NameUnit ,AbstractBaseUnit[]> reference;

    [SerializeField] PrefabsUnits[] storePrefabs;

    public Dictionary<NameUnit, AbstractBaseUnit[]> StartDictionary()
    {
        reference = new Dictionary<NameUnit, AbstractBaseUnit[]>();
        foreach(PrefabsUnits arrayWithPrefabs in storePrefabs)
        {
            reference.Add(arrayWithPrefabs.nameUnit, arrayWithPrefabs.prefabs);
        }
        
        return reference;
    }

    public Dictionary<NameUnit, AbstractBaseUnit[]> GetDictionaryOfUnits()
    {
        if (reference == null) StartDictionary();
        return reference;
    }

    public int GetCountIndividualList => storePrefabs.Length;


}
[System.Serializable]
public class PrefabsUnits
{
    public NameUnit nameUnit;   
    public AbstractBaseUnit[] prefabs;
}
public enum NameUnit
{
    Infantry = 0, Mech = 1, Recon = 2, Tank = 3, MdTank = 4, NeoTank = 5, APC = 6, Artillery = 7,
    Rockets = 8, AntiAir = 9, Mssls = 10, BCptr = 11, TCptr = 14, Fghtr = 13, Bmbr = 12, BShp = 15,
    Crsr = 16, Lndr = 17, Sub = 18, City = 20, Base = 21, HQ = 22, Arprt = 23, Port = 24, Brdg = 25,
    Road = 26, Reef = 27, Shoal = 28, River = 29, Wood = 30, Mtn = 31, Sea = 32, Plain = 33, Delete = 34
    
}
public enum FactionUnit
{
    Player1 = 1, Player2 = 2, Player3 = 3, Player4 = 4, Neutral = 0
}

