using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class WorldScriptableObjects : MonoBehaviour
{
    private static WorldScriptableObjects sharedInstance;

    private void Awake()
    {
        sharedInstance = this;
        
        ResetReferenceThatNeedIt();
    }

    private void ResetReferenceThatNeedIt()
    {
        currentVoxelID.reference = 0;
        maxVoxelID.reference = 0;

        hadPointerUnitSelected.boolReference = false;
        isDeleteOn.boolReference = false;
        
        allTilesInTheGrid.reference.Clear();
    }
    public static WorldScriptableObjects GetInstance() => sharedInstance;
    
    public IntReference currentVoxelID;
    public IntReference maxVoxelID;
    public IntReference keyForBarrackMenuData;
    public DataStartMap idMap;
    public DictionaryTiles allTilesInTheGrid;

    public BoolReference hadPointerUnitSelected;
    public BoolReference isDeleteOn;
    public BoolReference willUnitMove;
    public bool firstDayPass;
    public bool isInterfaceInRightPosition;
    
    public SOVector lastDirection;
    public TilesScriptableObject tileSelected;
    public TilesScriptableObject tileSelectedWithUnit;
    
    [Header("ListTiles")]
    public ListTilesScriptableObjectsRefactor tilesPathHighLight;
    public ListTilesScriptableObjectsRefactor currentTilesWhereEnemyAre;
    public ListTilesScriptableObjectsRefactor tilesWhereUnitCanBeDispose;
    public ListTilesScriptableObjectsRefactor allTilesWHereMyArrowsWillBe;
    public ListTilesScriptableObjectsRefactor tilesRangeAction;
    [Header("me da pereza ir uno por uno")]
    public SOListInt idsArrows;
    
    public ScriptableObjectPlayers currentPLayer;
    
    public MeshDataSO gridData;
    public MeshDataSO gridHighLightData;

    public MapData _currentMapDisplayData;
    public MapData _resetMapData;
    //Structs Container
    public ArrowsDataPoolSO poolDataPathArrows;

    [Space] [Space] [Header("Managers Reference")]
    public BuilderTileSO builderTile;
    public PutElementsEditMap managerPutElementsInTheMapFromCustomMap;
    public StatsManager statsPlayersManager;

}
