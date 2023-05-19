using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObjectsCanvas : MonoBehaviour
{
    private static WorldObjectsCanvas _sharedInstance;


    public static WorldObjectsCanvas GetInstance()
    {
        return _sharedInstance;
    }
    
    private void Awake()
    {
        if (_sharedInstance == null) _sharedInstance = this;
    }

    public GameObject smallArrowHighLight;
    public GameObject bigArrowHighLight;
}
