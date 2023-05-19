// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class TileSearcherNeightbours : MonoBehaviour
// {
//     private static List<Tile> allTilesInTheGrid;
//     private static List<Tile> allTilesWalkable;
//
//     //Bueno pues tienes que refactorizar esta puta mierda, porque carajo usas monobehavior, si no es necesario que este
//     //en escena papu, simplemente haz un new en grid manager y usala, despues pues bueno a ver que haces o otra option es
//     //hacer un singleton pero no me convence esa ultima.
//     public static void SetListGrid(List<Tile> _allTilesInTheGrid, List<Tile> _allTilesWalkable)
//     {
//         allTilesInTheGrid = _allTilesInTheGrid;
//         allTilesWalkable = _allTilesWalkable;
//     }
//     public void CalculateNeightbourTile(Tile tileCentral)
//     {
//         var positionInGrid = tileCentral.GetTilePosition();
//         
//         tileCentral.allNeightbourTile = new List<Tile>();
//         tileCentral.stickNeightbourTile = new List<Tile>();
//
//         Tile upNeightbourTile = FindTileWithXYEveryone(new Vector2(positionInGrid.x, positionInGrid.y + 1));
//         Tile downNeightbourTile = FindTileWithXYEveryone(new Vector2(positionInGrid.x, positionInGrid.y - 1));
//         Tile rightNeightbourTile = FindTileWithXYEveryone(new Vector2(positionInGrid.x + 1, positionInGrid.y));
//         Tile leftNeightbourTile = FindTileWithXYEveryone(new Vector2(positionInGrid.x - 1, positionInGrid.y));
//
//         Tile topRightNeightbourTile = FindTileWithXYEveryone(new Vector2(positionInGrid.x + 1, positionInGrid.y + 1));
//         Tile topLeftNeightbourTile = FindTileWithXYEveryone(new Vector2(positionInGrid.x - 1, positionInGrid.y + 1));
//         Tile downRightNeightbourTile = FindTileWithXYEveryone(new Vector2(positionInGrid.x + 1, positionInGrid.y - 1));
//         Tile downLeftNeightbourTile = FindTileWithXYEveryone(new Vector2(positionInGrid.x - 1, positionInGrid.y + 1));
//
//         if (upNeightbourTile)
//         {
//             tileCentral.allNeightbourTile.Add(upNeightbourTile);
//             tileCentral.stickNeightbourTile.Add(upNeightbourTile);
//             tileCentral.SetUpNeightbour(upNeightbourTile);
//         }
//         if (downNeightbourTile)
//         {
//             tileCentral.allNeightbourTile.Add(downNeightbourTile);
//             tileCentral.stickNeightbourTile.Add(downNeightbourTile);
//             tileCentral.SetDownNeightbour(downNeightbourTile);
//         }
//         if (rightNeightbourTile)
//         {
//             tileCentral.allNeightbourTile.Add(rightNeightbourTile);
//             tileCentral.stickNeightbourTile.Add(rightNeightbourTile);
//             tileCentral.SetRightNeightbour(rightNeightbourTile);
//         }
//         if (leftNeightbourTile)
//         {
//             tileCentral.allNeightbourTile.Add(leftNeightbourTile);
//             tileCentral.stickNeightbourTile.Add(leftNeightbourTile);
//             tileCentral.SetLeftNeightbour(leftNeightbourTile);
//         }
//
//         if (topRightNeightbourTile) tileCentral.allNeightbourTile.Add(topRightNeightbourTile);
//         if (topLeftNeightbourTile) tileCentral.allNeightbourTile.Add(topLeftNeightbourTile);
//         if (downRightNeightbourTile) tileCentral.allNeightbourTile.Add(downRightNeightbourTile);
//         if (downLeftNeightbourTile) tileCentral.allNeightbourTile.Add(downLeftNeightbourTile);
//
//
//     }
//     private Tile FindTileWithXYEveryone(Vector2 actualPosition)
//     {
//         for (int i = 0; i != allTilesInTheGrid.Count; i++)
//         {
//             if (allTilesInTheGrid[i].GetTilePosition() == actualPosition)
//             {
//                 return allTilesInTheGrid[i];
//             }
//         }
//         return null;
//     }
//
//
// }
