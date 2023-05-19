using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager : MonoBehaviour
{
    [SerializeField] ListGameObjectsReference objectsToSpawns;

    private void Awake()
    {
        InstantiatePrefabsSingleton();
    }
    private void InstantiatePrefabsSingleton()
    {
        foreach (GameObject singleton in objectsToSpawns.reference) 
        {
            var singletonPrefabs = Instantiate(singleton, Vector2.zero, Quaternion.identity);
            singletonPrefabs.transform.SetParent(this.transform);
        }
    }
}
