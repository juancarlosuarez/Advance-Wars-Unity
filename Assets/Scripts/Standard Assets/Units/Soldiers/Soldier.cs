using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;
[RequireComponent(typeof(SpriteRenderer))]
public class Soldier : AbstractBaseUnit
{
    public Sprite spriteUI;
    
    public SOListInt baseDamageVSAnotherUnits;

    //Luck
    public bool thisUnitCanMakeSomeAction;
    public int numberMoveAvailable;

    public int rangeMin;
    public int rangeMax;

    public List<TerrainTypes> terrainUnitCanTransit;
    public TypeSoldier typeSoldier;

    public ICondictionAttackSoldiers conditionsAttack;
    public ManagerLifeUnitUI managerLifeUnitUI;
    public SpriteRenderer lifeUISpriteRenderer;

    [Range(0, 100)] public int currentLife;
    
    
    public bool thisUnitIsConquerCity = false;

    [Header("Components")] [NonSerialized]public SpriteRenderer spriteRenderer;
    public Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}

public enum TypeWheels
{
    Inftry, Mech, TireA, TireB, Tank, Air, Ship, Trpt
}
public enum TypeSoldier
{
    Infantry, Transport, Mechanic, Artillery
}
