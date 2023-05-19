using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Unit")]
public class ScriptableUnit : ScriptableObject
{
    public Faction Faction;
    [FormerlySerializedAs("UnitPrefab")] public Soldier prefab;
}

public enum Faction
{
    Hero = 0,
    Enemy = 1,
    Neutral = 2,
    BuildsEnemy = 3,
    BuildsAlly = 4
}