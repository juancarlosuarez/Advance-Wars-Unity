using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Build", menuName = "ScriptableObject/Units/Builds")]
public class ScriptableBuilds : ScriptableObject
{
    public TypeOfBuild typeOfBuild;
    public Build buildPrefab;
        

}
    public enum TypeOfBuild
    {
        City = 0,
        Military = 1
    }
