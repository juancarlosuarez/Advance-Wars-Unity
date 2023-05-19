using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObject/UIController")]
public class SOMenuManager : ScriptableObject
{
    public IGestorMenuUI reference;

    private void Awake()
    {
        reference = null;
    }
}
