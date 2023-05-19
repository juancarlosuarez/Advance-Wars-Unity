using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tile : MonoBehaviour
{
    public List<GameObject> highLightObject;

    public Soldier occupiedSoldier; //
    public Build occupiedBuild;//
    public AbstractBaseUnit occupiedUnit;//

    public bool isWalkable;
    public int ammountEffortToPass;
    public int defenseTerrain;
    //public bool Walkable => isWalkable && occupiedUnit == null;
    public bool Walkable => CanTileBeWalkable();
    
    
    [HideInInspector] public List<Tile> allNeightbourTile = new List<Tile>();//
    [HideInInspector] public List<Tile> stickNeightbourTile = new List<Tile>();//
    
    [HideInInspector] public Tile upNeightbourTile;
    [HideInInspector] public Tile downNeightbourTile;
    [HideInInspector] public Tile rightNeightbourTile;
    [HideInInspector] public Tile leftNeightbourTile;

    private Vector2 positionInGrid;//
    [HideInInspector] public int spawnID;

    public SetUnitsGrid setUnitComponent;
    [HideInInspector] public NodeBase nodeBaseComponent;
    public HighLightTile highLightComponent;

    public SpriteRenderer tileSpriteRenderer;
    public int positionInArray;

    public TerrainTypes terrainType;
    private void Start()
    {
        //searchNeighbours.CalculateNeightbourTile(this);
    }
    private void Awake()
    {
        nodeBaseComponent = GetComponent<NodeBase>();
        tileSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private bool CanTileBeWalkable()
    {
        if (occupiedUnit == null && isWalkable) return true;
        if (occupiedUnit != null && occupiedUnit.typeUnit == TypesUnit.Soldier) return false;
        return true;
    }
    //Esta clase solo se debe de encargar de dar valor a todas estas variables y colocar los seter and getter, por lo que aun te queda un rato de trabajo
    //habia pensado en colocar el init aqui y que este haga todo lo que deba a hacer pero no puede ser ya que daria error al no encontrar a los vecinos
    //por lo que debe ir en el start si o si, a crear mas clases para este puto desastre que te has montado aca
    #region
    public virtual void Init(int x, int y, int countSpawnID)
    {
        SetTilePosition(x, y);
        spawnID = countSpawnID;
    }
    private void SetTilePosition(int x, int y)
    {
        positionInGrid = new Vector2(x, y);
    }
    public Vector2 GetTilePosition()
    {
        return positionInGrid;
    }
    public void SetUpNeightbour(Tile upTile)
    {
        upNeightbourTile = upTile;
    }
    public void SetDownNeightbour(Tile downTile)
    {
        downNeightbourTile = downTile;
    }
    public void SetRightNeightbour(Tile rightTile)
    {
        rightNeightbourTile = rightTile;
    }
    public void SetLeftNeightbour(Tile leftTile)
    {
        leftNeightbourTile = leftTile;
    }
    public static Tile FindTileNeighbourWithTileCentral(Tile centralTile, Vector2 newPosition)
    {
        foreach (Tile c in centralTile.allNeightbourTile)
        {
            if (newPosition == c.GetTilePosition()) return c;
        }
        return null;
    }
    #endregion
}
public static class FindTile
{
    public static Tile FindTileBetweenNeighBours(this Tile tile,  Tile tileCentral, Vector2 newPosition)
    {
        foreach (var c in tileCentral.allNeightbourTile)
        {
            if (newPosition == c.GetTilePosition()) return c;
        }

        return null;
    }
}
    public enum  TerrainTypes
    {
        WaterTerrain, PassableTerrain, DifficultTerrain
    }
