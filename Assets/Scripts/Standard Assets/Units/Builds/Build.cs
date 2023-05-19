using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : AbstractBaseUnit
{
    public TypeOfBuild typeOfBuild;

    public Sprite spriteUI;
    public TerrainTypes typeTileWhereMyBuildCanBe;

    public bool isThisBeingConquered = false;
}
