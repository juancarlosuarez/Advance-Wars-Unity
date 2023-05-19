using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetterTilesFromCornersRightUp
{
    private int _currentTileID;
    private MapData _currentMap;
    public List<int> GetTilesPosition(MapData currentMap)
    {
        _currentMap = currentMap;

        var allTiles = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            int count = 0;
            
            if (i == 0) _currentTileID = _currentMap.positionIDCornerRightUp;
            else _currentTileID--;
            allTiles.Add(_currentTileID);
            
            for (int j = 0; j < 15; j++)
            {
                LeftNeighbour(ref allTiles, count);
                count++;
            }
        }
        return allTiles;
    }
    private void LeftNeighbour(ref List<int> allTiles, int count)
    {
        int currentID = _currentTileID - _currentMap.height - (count * _currentMap.height);
        allTiles.Add(currentID);
    }
}
