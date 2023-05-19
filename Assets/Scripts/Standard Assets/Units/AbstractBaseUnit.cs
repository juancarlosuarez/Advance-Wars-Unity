using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
public abstract class AbstractBaseUnit : SerializedMonoBehaviour, ITsSpawnableInGrid
{
    [NonSerialized] public Tile occupiedTile;
    [NonSerialized] public TileRefactor occupiedTileRefactor;
    
    public FactionUnit playerThatCanControlThisUnit;
    public TypesUnit typeUnit;
    public NameUnit nameUnit;
    [Range(1, 20)] public int currentLifeUI;
    public SpriteRenderer spriteRenderer;
    
}
public enum TypesUnit
{

    Soldier, Build
}