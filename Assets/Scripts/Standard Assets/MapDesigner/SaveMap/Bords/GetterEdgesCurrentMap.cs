using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetterEdgesCurrentMap
{
    private MapData _currentMap;
    public GetterEdgesCurrentMap(MapData currentMap)
    {
        _currentMap = currentMap;
    }
    public List<int> SouthEdges()
    {
        List<int> currentEdges = new List<int>(); 
        currentEdges.Add(0);
        for (int i = 0; i < _currentMap.width; i++)
        {
            int currentID = _currentMap.height + (i * _currentMap.height);
            currentEdges.Add(currentID);
        }
        return currentEdges;
    }

    public List<int> NorthEdges()
    {
        List<int> currentEdges = new List<int>();
        currentEdges.Add( _currentMap.height - 1);
        for (int i = 0; i < _currentMap.width; i++)
        {
            int currentID = (_currentMap.height) + _currentMap.height + (i * _currentMap.height) - 1;
            currentEdges.Add(currentID);
        }
        return currentEdges;
    }

    public List<int> WestEdges()
    {
        List<int> currentEdges = new List<int>();
        for (int i = 0; i < _currentMap.height; i++) currentEdges.Add(i);
        return currentEdges;
    }

    public List<int> EastEdges()
    {
        List<int> currentEdges = new List<int>();
        var currentID = _currentMap.positionIDCornerRightDown;
        //_currentEdges.Add(currentID);
        currentEdges.Add(currentID);
        for (int i = 0; i < _currentMap.height; i++)
        {
            currentID++;
            currentEdges.Add(currentID);
        }
        return currentEdges;
    }
}
