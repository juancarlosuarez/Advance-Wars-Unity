using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSearchNeighBour
{
    private ListTilesReference allTilesInTheGrid;
    public TileSearchNeighBour()
    {
        allTilesInTheGrid =
            Resources.Load<ListTilesReference>("ScriptableObject/Data/DataSpecial/ListTilesData/AllTilesInTheGrid");
    }

    public void CalculateNeightbourTile(Tile tileCentral)
    {
        var positionInGrid = tileCentral.GetTilePosition();
        
        tileCentral.allNeightbourTile = new List<Tile>();
        tileCentral.stickNeightbourTile = new List<Tile>();

        Tile upNeightbourTile = FindTileWithXYEveryone(new Vector2(positionInGrid.x, positionInGrid.y + 1));
        Tile downNeightbourTile = FindTileWithXYEveryone(new Vector2(positionInGrid.x, positionInGrid.y - 1));
        Tile rightNeightbourTile = FindTileWithXYEveryone(new Vector2(positionInGrid.x + 1, positionInGrid.y));
        Tile leftNeightbourTile = FindTileWithXYEveryone(new Vector2(positionInGrid.x - 1, positionInGrid.y));

        Tile topRightNeightbourTile = FindTileWithXYEveryone(new Vector2(positionInGrid.x + 1, positionInGrid.y + 1));
        Tile topLeftNeightbourTile = FindTileWithXYEveryone(new Vector2(positionInGrid.x - 1, positionInGrid.y + 1));
        Tile downRightNeightbourTile = FindTileWithXYEveryone(new Vector2(positionInGrid.x + 1, positionInGrid.y - 1));
        Tile downLeftNeightbourTile = FindTileWithXYEveryone(new Vector2(positionInGrid.x - 1, positionInGrid.y + 1));

        if (upNeightbourTile)
        {
            tileCentral.allNeightbourTile.Add(upNeightbourTile);
            tileCentral.stickNeightbourTile.Add(upNeightbourTile);
            tileCentral.SetUpNeightbour(upNeightbourTile);
            
        }
        if (downNeightbourTile)
        {
            tileCentral.allNeightbourTile.Add(downNeightbourTile);
            tileCentral.stickNeightbourTile.Add(downNeightbourTile);
            tileCentral.SetDownNeightbour(downNeightbourTile);
        }
        if (rightNeightbourTile)
        {
            tileCentral.allNeightbourTile.Add(rightNeightbourTile);
            tileCentral.stickNeightbourTile.Add(rightNeightbourTile);
            tileCentral.SetRightNeightbour(rightNeightbourTile);
        }
        if (leftNeightbourTile)
        {
            tileCentral.allNeightbourTile.Add(leftNeightbourTile);
            tileCentral.stickNeightbourTile.Add(leftNeightbourTile);
            tileCentral.SetLeftNeightbour(leftNeightbourTile);
        }

        if (topRightNeightbourTile) tileCentral.allNeightbourTile.Add(topRightNeightbourTile);
        if (topLeftNeightbourTile) tileCentral.allNeightbourTile.Add(topLeftNeightbourTile);
        if (downRightNeightbourTile) tileCentral.allNeightbourTile.Add(downRightNeightbourTile);
        if (downLeftNeightbourTile) tileCentral.allNeightbourTile.Add(downLeftNeightbourTile);
    }
    private Tile FindTileWithXYEveryone(Vector2 actualPosition)
    {
        for (int i = 0; i != allTilesInTheGrid.reference.Count; i++)
        {
            if (allTilesInTheGrid.reference[i].GetTilePosition() == actualPosition)
            {
                return allTilesInTheGrid.reference[i];
            }
        }
        return null;
    }
    
}
