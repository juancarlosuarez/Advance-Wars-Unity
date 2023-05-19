using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    public StateSpawnGamePlayElements stateSpawnGamePlayElements;

    void Awake()
    {
        sharedInstance = this;
    }

    void Start()
    {
        ChangeState(StateSpawnGamePlayElements.GenerateGrid);
    }

    public void ChangeState(StateSpawnGamePlayElements newPlayElements)
    {
        stateSpawnGamePlayElements = newPlayElements;
        switch (newPlayElements)
        {
            case StateSpawnGamePlayElements.GenerateGrid:
                GridManager.sharedInstance.GenerateGrid();
                break;
            case StateSpawnGamePlayElements.SpawnHeroes:
                UnitManager.sharedInstance.SpawnHeroes();
                UnitManager.sharedInstance.SpawnBuild();
                break;
            case StateSpawnGamePlayElements.SpawnEnemies:
                UnitManager.sharedInstance.SpawnEnemies();
                break;
            case StateSpawnGamePlayElements.HeroesTurn:
                break;
            case StateSpawnGamePlayElements.EnemiesTurn:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newPlayElements), newPlayElements, null);
        }
    }
}

public enum StateSpawnGamePlayElements
{
    GenerateGrid = 0,
    SpawnHeroes = 1,
    SpawnEnemies = 2,
    HeroesTurn = 3,
    EnemiesTurn = 4
}