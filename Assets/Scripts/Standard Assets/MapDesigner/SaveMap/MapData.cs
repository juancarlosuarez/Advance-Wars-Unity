using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = ("ScriptableObject/DataSpecial/MapData"))]
public class MapData : SerializedScriptableObject
{
    public int height = 28;
    public int width = 28;

    public TileRefactor[] tilesOfTheGrid;
    public List<Soldier> allSoldierInGrid = new List<Soldier>();
    public List<Build> allBuildInGrid = new List<Build>();
    public string mapName;

    public int positionIDCornerLeftDown, positionIDCornerLeftUp, positionIDCornerRightDown, positionIDCornerRightUp;

    public List<int> allTilesCornerLeftDown, allTilesCornerLeftUp, allTilesCornerRightDown, allTilesCornerRightUp; //Esto no me vale, necesito los tiles que completan el circulo alrededor del mapa,
    //esto son el conjunto de tiles que completan la camara en las esquinas

    public List<int> allTilesEdgeLeft, allTilesEdgeRight, allTilesEdgeSouth, allTilesEdgeNorth;


    public int positionTileCameraLeftDown, positionTileCameraLeftUp, positionTileCameraRightDown, positionTileCameraRightUp;
    public List<int> allEdgesID;
    public bool HadMapData() => tilesOfTheGrid != null;

    public Dictionary<FactionUnit, int> idTileMainCityEachPlayer;
    public int currentDays;
    public int countPlayerTurn;
    public void CalculateCorners()
    {
        positionIDCornerLeftDown = 0;
        positionIDCornerLeftUp = height - 1;
        positionIDCornerRightDown = ((width - 1) * height);
        positionIDCornerRightUp = (width * height) - 1;

        allTilesCornerLeftDown = new GetterTilesFromCornersLeftDown().GetTilesPosition(this);
        allTilesCornerLeftUp = new GetterTilesFromCornersLeftUp().GetTilesPosition(this);
        allTilesCornerRightDown = new GetterTilesFromCornersRightDown().GetTilesPosition(this);
        allTilesCornerRightUp = new GetterTilesFromCornersRightUp().GetTilesPosition(this);

        var getterIDTileCentral = new GetterTileCentral(this);
        positionTileCameraLeftDown = getterIDTileCentral.GetTileCentralLeftDown();
        positionTileCameraLeftUp = getterIDTileCentral.GetTileCentralLeftUp();
        positionTileCameraRightDown = getterIDTileCentral.GetTileCentralRightDown();
        positionTileCameraRightUp = getterIDTileCentral.GetTileCentralRightUp();
        
        var getterEdges = new GetterEdgesCurrentMap(this);
        allTilesEdgeSouth = getterEdges.SouthEdges();
        allTilesEdgeNorth = getterEdges.NorthEdges();
        allTilesEdgeLeft = getterEdges.WestEdges();
        allTilesEdgeRight = getterEdges.EastEdges();
    }
    public void ResetDataMap()
    {
        allBuildInGrid = new List<Build>();
        allSoldierInGrid = new List<Soldier>();
        
        idTileMainCityEachPlayer = new Dictionary<FactionUnit, int>();
        for (int i = 1; i < 5; i++) idTileMainCityEachPlayer.Add((FactionUnit)i, 0);
        
    }
}
