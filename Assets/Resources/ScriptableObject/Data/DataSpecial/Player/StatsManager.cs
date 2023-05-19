using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Manager/StatsManager/Manager")]
public class StatsManager : SerializedScriptableObject
{
    
    [SerializeField] private Dictionary<FactionUnit, DataStats> statsEachPlayer;
    public DataStats GetStatsPlayer(FactionUnit player) => statsEachPlayer[player];
}
