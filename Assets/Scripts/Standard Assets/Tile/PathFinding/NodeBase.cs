using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBase : MonoBehaviour
{
    private int _gCost;
    private int _hCost;
    private int _fCost;
    private Tile _cameFromCell;
    private Tile tileComponent;

    private void Awake()
    {
        tileComponent = GetComponent<Tile>();
    }
    public void SetGCost(int value)
    {
        _gCost = value;
    }
    public int GetGCost()
    {
        return _gCost;
    }
    public void SetHCost(int value)
    {
        _hCost = value;
    }
    public void CalculateFCost()
    {
        _fCost = _gCost + _hCost + tileComponent.ammountEffortToPass;
    }
    public int GetFCost()
    {
        return _fCost;
    }
    public void SetCameFromCell(Tile previousCell)
    {
        _cameFromCell = previousCell;
    }
    public Tile GetCameFromCell()
    {
        return _cameFromCell;
    }
}
