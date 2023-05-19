using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetterTileCentral
{
    private int _tileIDCentral;
    private MapData _currentMap;
    public GetterTileCentral(MapData currentMap)
    {
        _currentMap = currentMap;
    }
    public int GetTileCentralLeftDown()
    {
        _tileIDCentral = _currentMap.positionIDCornerLeftDown;

        CalculateIDTileCentralLeftDown();
        return _tileIDCentral;
    }
    public int GetTileCentralLeftUp()
    {
        _tileIDCentral = _currentMap.positionIDCornerLeftUp;
        
        CalculateIDTileCentralLeftUp();
        return _tileIDCentral;
    }

    public int GetTileCentralRightDown()
    {
        _tileIDCentral = _currentMap.positionIDCornerRightDown;
        
        CalculateIDTileCentralRightDown();
        return _tileIDCentral;
    }

    public int GetTileCentralRightUp()
    {
        _tileIDCentral = _currentMap.positionIDCornerRightUp;
        
        CalculateIDTileCentralRightUp();
        return _tileIDCentral;
    }
    private void CalculateIDTileCentralLeftDown()
    {
        RightNeighbour();
        UpNeighbour();
    }
    private void CalculateIDTileCentralLeftUp()
    {
        RightNeighbour();
        DownNeighbour();
    }

    private void CalculateIDTileCentralRightDown()
    {
        LeftNeighbour();
        UpNeighbour();
    }

    private void CalculateIDTileCentralRightUp()
    {
        LeftNeighbour();
        DownNeighbour();
    }
    private void UpNeighbour() =>_tileIDCentral += 4;
    private void DownNeighbour() =>_tileIDCentral -= 4;
    private void RightNeighbour() =>_tileIDCentral = _tileIDCentral + _currentMap.height + (6 * _currentMap.height);
    private void LeftNeighbour() => _tileIDCentral = _tileIDCentral - _currentMap.height - (7 * _currentMap.height);
}
