using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.Linq;
using Debug = UnityEngine.Debug;

public class PathFindingRefactor
{
    private TileRefactor _originTile;
    private TileRefactor _finalTile;
    private int _maxSteps;
    private static PathFindingRefactor _sharedInstance;

    private List<TileRefactor> _tilesDiscard;
    private List<TileRefactor> _tilesPossible;
    private bool _firstRound;
    private int _tryCounting = 0;
    
    private PathFindingRefactor(){}

    //Singleton
    public static PathFindingRefactor GetInstance() => _sharedInstance ??= new PathFindingRefactor();
    
    public List<TileRefactor> StartCalculatePath(TileRefactor originTile, TileRefactor finalTile, int movesAvailable)
    {
        if (!IsTileInPathAvailable(finalTile)) return null;

        SetValues(originTile, finalTile, movesAvailable);
        ResetValuesFromPreviousPath();

        return GetPath();
    }

    private void SetValues(TileRefactor originTile, TileRefactor finalTile, int maxSteps)
    {
        _maxSteps = maxSteps;
        _originTile = originTile;
        _finalTile = finalTile;
    }
    private bool IsTileInPathAvailable(TileRefactor tileFinal) =>
        WorldScriptableObjects.GetInstance().tilesPathHighLight.reference.Contains(tileFinal);
    private void ResetValuesFromPreviousPath()
    {
        var tilesCurrentlyAnalyzed = WorldScriptableObjects.GetInstance().tilesPathHighLight.reference; 
        foreach (var tile in tilesCurrentlyAnalyzed)
        {
            tile.dataNodeBase.SetGCost(int.MaxValue);
            tile.dataNodeBase.CalculateFCost();
            tile.dataNodeBase.SetCameFromCell(null);
            tile.dataNodeBase.SetCameFromCellSteps(0); //Test
        }
        _originTile.dataNodeBase.SetGCost(0);
        _originTile.dataNodeBase.SetHCost(CalculateDistanceCost(_originTile, _finalTile));
        _originTile.dataNodeBase.CalculateFCost();

    }

    private int CalculateDistanceCost(TileRefactor originTile, TileRefactor finalTile)
    {
        int diagonalCost = 14;
        int straightCost = 10;

        int xDistance = Mathf.Abs((int)originTile.positionTileInGrid.x - (int)finalTile.positionTileInGrid.x);
        int yDistance = Mathf.Abs((int)originTile.positionTileInGrid.y - (int)finalTile.positionTileInGrid.y);
        int remaining = Mathf.Abs(xDistance - yDistance);

        return diagonalCost * Mathf.Min(xDistance, yDistance) + straightCost * remaining;
    }

    private List<TileRefactor> GetPath()
    {
        List<TileRefactor> openList = new List<TileRefactor> { _originTile };
        List<TileRefactor> closedList = new List<TileRefactor>();
        _originTile.dataNodeBase.SetCameFromCellSteps(_originTile.occupiedSoldier.numberMoveAvailable);
        // Test foreach (var tile in _tilesDiscard) closedList.Add(tile);
        /* Ya reseteado nuestro camino, creamos un bucle por cada elemento metido dentro de nuestra lista abierta, luego calculamos el valor f mas bajo
         Primero se van comprobando los elementos de la lista, si el tile a revisar es el tileFinal se calcula finalmente el camino, sino pues se borra el elemento
         de la lista abierta y se anade a la cerrada para evitar anadirlo de nuevo. Luego recorremos los 4 tiles adyacentes y calculamos unos valores para determinar
         si es valido y se anade a la lista abierto, si no esta ya en ella, y se repite el bucle con el valor mas optimo al principio de la funcion. Importante
         que antes de calcular los valores adyacente, hemos filtrado los resultados a los que solos estan dentro del rango de accion permitido, para que no pueda
         hacer cosas raras el pathfinding.
         */
        //_count = 0; //Test 
        while (openList.Count > 0) 
        {
            TileRefactor currentCell = GetLowestFCostCell(openList, false);

            if (currentCell == _finalTile)
            {
                return CalculatePath(_finalTile);
            }
            openList.Remove(currentCell);
            closedList.Add(currentCell);
            List<TileRefactor> currentCellStickFilter =
                currentCell.NeighboursInCross.Where(x => WorldScriptableObjects.GetInstance().tilesPathHighLight.reference.Contains(x) ).ToList();
            foreach (TileRefactor neighbourCell in currentCellStickFilter)
            {
                if (closedList.Contains(neighbourCell)) continue;
                int stepsLocal = currentCell.dataNodeBase.GetCameFromCellSteps() - neighbourCell.dataVariable.ammountEffortToPass; //Test
                int tentativeCost = currentCell.dataNodeBase.GetGCost() + CalculateDistanceCost(currentCell, neighbourCell);
                if (tentativeCost < neighbourCell.dataNodeBase.GetGCost())
                {
                    neighbourCell.dataNodeBase.SetCameFromCell(currentCell);
                    neighbourCell.dataNodeBase.SetCameFromCellSteps(stepsLocal); //Test
                    neighbourCell.dataNodeBase.SetGCost(tentativeCost);
                    neighbourCell.dataNodeBase.SetHCost(CalculateDistanceCost(neighbourCell, _finalTile));
                    neighbourCell.dataNodeBase.CalculateFCost();
                }
                if (!openList.Contains(neighbourCell))
                {
                    openList.Add(neighbourCell);
                    if (neighbourCell.dataNodeBase.GetCameFromCellSteps() < 0)
                    {
                        openList.Remove(neighbourCell);
                        closedList.Add(neighbourCell);
                    }
                }
            }
        }
        ResetValuesFromPreviousPath();
        return GetPathMinimum();
    }

    private List<TileRefactor> GetPathMinimum()
    {
        List<TileRefactor> openList = new List<TileRefactor> { _originTile };
        List<TileRefactor> closedList = new List<TileRefactor>();
        _originTile.dataNodeBase.SetCameFromCellSteps(_originTile.occupiedSoldier.numberMoveAvailable);

        while (openList.Count > 0) 
        {
            TileRefactor currentCell = GetLowestFCostCell(openList, true);

            if (currentCell == _finalTile)
            {
                return CalculatePath(_finalTile);
            }
            openList.Remove(currentCell);
            closedList.Add(currentCell);
            List<TileRefactor> currentCellStickFilter =
                currentCell.NeighboursInCross.Where(x => WorldScriptableObjects.GetInstance().tilesPathHighLight.reference.Contains(x) && x.dataVariable.ammountEffortToPass == 1 || x == _finalTile).ToList();
            foreach (TileRefactor neighbourCell in currentCellStickFilter)
            {
                if (closedList.Contains(neighbourCell)) continue;
                int stepsLocal = currentCell.dataNodeBase.GetCameFromCellSteps() - neighbourCell.dataVariable.ammountEffortToPass; //Test
                int tentativeCost = currentCell.dataNodeBase.GetGCost() + CalculateDistanceCost(currentCell, neighbourCell);
                if (tentativeCost < neighbourCell.dataNodeBase.GetGCost())
                {
                    neighbourCell.dataNodeBase.SetCameFromCell(currentCell);
                    neighbourCell.dataNodeBase.SetCameFromCellSteps(stepsLocal); //Test
                    neighbourCell.dataNodeBase.SetGCost(tentativeCost);
                    neighbourCell.dataNodeBase.SetHCost(CalculateDistanceCost(neighbourCell, _finalTile));
                    neighbourCell.dataNodeBase.CalculateFCost();
                }
                if (!openList.Contains(neighbourCell))
                {
                    openList.Add(neighbourCell);
                    if (neighbourCell.dataNodeBase.GetCameFromCellSteps() < 0)
                    {
                        openList.Remove(neighbourCell);
                        closedList.Add(neighbourCell);
                    }
                }
            }
        }

        _tilesDiscard = new List<TileRefactor>();
        _tilesPossible = new List<TileRefactor>();
        ResetValuesFromPreviousPath();
        return GetPathByBruteForce(true);
    }

    private List<TileRefactor> GetPathDontCareNothing()
    {
        List<TileRefactor> openList = new List<TileRefactor> { _originTile };
        List<TileRefactor> closedList = new List<TileRefactor>();
        _originTile.dataNodeBase.SetCameFromCellSteps(_originTile.occupiedSoldier.numberMoveAvailable);
 
        while (openList.Count > 0)
        {
            TileRefactor currentCell = GetLowestFCostCell(openList, false);

            if (currentCell == _finalTile)
            {
                return CalculatePath(_finalTile);
            }

            openList.Remove(currentCell);
            closedList.Add(currentCell);
            List<TileRefactor> currentCellStickFilter =
                currentCell.NeighboursInCross
                    .Where(x => WorldScriptableObjects.GetInstance().tilesPathHighLight.reference.Contains(x)).ToList();
            foreach (TileRefactor neighbourCell in currentCellStickFilter)
            {
                if (closedList.Contains(neighbourCell)) continue;
                int stepsLocal = currentCell.dataNodeBase.GetCameFromCellSteps() -
                                 neighbourCell.dataVariable.ammountEffortToPass; //Test
                int tentativeCost = currentCell.dataNodeBase.GetGCost() +
                                    CalculateDistanceCost(currentCell, neighbourCell);
                if (tentativeCost < neighbourCell.dataNodeBase.GetGCost())
                {
                    neighbourCell.dataNodeBase.SetCameFromCell(currentCell);
                    neighbourCell.dataNodeBase.SetCameFromCellSteps(stepsLocal); //Test
                    neighbourCell.dataNodeBase.SetGCost(tentativeCost);
                    neighbourCell.dataNodeBase.SetHCost(CalculateDistanceCost(neighbourCell, _finalTile));
                    neighbourCell.dataNodeBase.CalculateFCost();
                }

                if (!openList.Contains(neighbourCell))
                {
                    openList.Add(neighbourCell);
                }
            }

        }
        return null;
    }
    private List<TileRefactor> GetPathByBruteForce(bool firstCall)
    {
        /*Este test, se encarga de buscar practicamente cada caso, hasta dar con el camino correcto, ya que
         * algunos casos dan el camino de manera incorrecto, pero es la ultima opcion, ya que es el mas demandante
         */
        List<TileRefactor> openList = new List<TileRefactor> { _originTile };
        List<TileRefactor> closedList = new List<TileRefactor>();
         _firstRound = true;
        bool notMoreElementsPossible = false;
        _originTile.dataNodeBase.SetCameFromCellSteps(_originTile.occupiedSoldier.numberMoveAvailable);

        if (firstCall){
            _tilesPossible = _originTile.NeighboursInCross
                .Where(x => WorldScriptableObjects.GetInstance().tilesPathHighLight.reference.Contains(x)).ToList();

            _tryCounting = 0;
        }
        foreach (var c in _tilesDiscard)
        {
            if (c != _originTile) closedList.Add(c);
        }

        try
        {
            while (openList.Count > 0)
            {
                TileRefactor currentCell = TestGetFCost(openList);
                
                if (currentCell == _finalTile) return CalculatePath(_finalTile);
                if (_tilesDiscard != null && _tilesDiscard.Count == _tilesPossible.Count) notMoreElementsPossible = true;

                
                openList.Remove(currentCell);
                closedList.Add(currentCell);
                List<TileRefactor> currentCellStickFilter =
                    currentCell.NeighboursInCross
                        .Where(x => WorldScriptableObjects.GetInstance().tilesPathHighLight.reference.Contains(x)).ToList();
                foreach (TileRefactor neighbourCell in currentCellStickFilter)
                {
                    if (closedList.Contains(neighbourCell)) continue;
                    int stepsLocal = currentCell.dataNodeBase.GetCameFromCellSteps() -
                                     neighbourCell.dataVariable.ammountEffortToPass; //Test
                    int tentativeCost = currentCell.dataNodeBase.GetGCost() +
                                        CalculateDistanceCost(currentCell, neighbourCell);
                    if (tentativeCost < neighbourCell.dataNodeBase.GetGCost())
                    {
                        neighbourCell.dataNodeBase.SetCameFromCell(currentCell);
                        neighbourCell.dataNodeBase.SetCameFromCellSteps(stepsLocal); //Test
                        neighbourCell.dataNodeBase.SetGCost(tentativeCost);
                        neighbourCell.dataNodeBase.SetHCost(CalculateDistanceCost(neighbourCell, _finalTile));
                        neighbourCell.dataNodeBase.CalculateFCost();
                    }

                    if (!openList.Contains(neighbourCell))
                    {
                        if (notMoreElementsPossible)
                        {
                            _tilesPossible = new List<TileRefactor>();
                            _tilesDiscard = new List<TileRefactor>();
                            _tilesPossible.Add(neighbourCell);
                            notMoreElementsPossible = false;
                        }
                        openList.Add(neighbourCell);
                        if (neighbourCell.dataNodeBase.GetCameFromCellSteps() < 0)
                        {
                            openList.Remove(neighbourCell);
                            closedList.Add(neighbourCell);
                        }
                    }

                    if (_tilesPossible.Contains(_finalTile)) return GetPathDontCareNothing();
                }
            }

            _tryCounting++;
            if (_tryCounting == 20)
            {
             ResetValuesFromPreviousPath();
             return GetPathDontCareNothing();
            }
            ResetValuesFromPreviousPath();
            return GetPathByBruteForce(false);
        }
        catch (StackOverflowException)
        {
            ResetValuesFromPreviousPath();
            return GetPathDontCareNothing();
        }
    }
    private TileRefactor GetLowestFCostCell(List<TileRefactor> openList, bool isMinimum)
    {
        TileRefactor lowestFCostCell = openList[0];
        
        for (int i = 0; i < openList.Count; i++ )
        {
            if (isMinimum)
            {
                if (openList[i].dataNodeBase.GetFCost() < lowestFCostCell.dataNodeBase.GetFCost() && openList[i].dataVariable.ammountEffortToPass == 1)
                {
                    lowestFCostCell = openList[i];
                }
            }
            else
            {
                if (openList[i].dataNodeBase.GetFCost() < lowestFCostCell.dataNodeBase.GetFCost() && openList[i].dataNodeBase.GetCameFromCellSteps() >= 0)
                {
                    lowestFCostCell = openList[i];
                }
            }
        }
        return lowestFCostCell;
    }
    private TileRefactor TestGetFCost(List<TileRefactor> openList)
    {
        TileRefactor lowestFCostCell = openList[0];
        
        for (int i = 0; i < openList.Count; i++ )
        {
            if (openList[i].dataNodeBase.GetFCost() < lowestFCostCell.dataNodeBase.GetFCost() && openList[i].dataNodeBase.GetCameFromCellSteps() >= 0)
            {
                if (_tilesDiscard != null && _tilesDiscard.Contains(openList[i])) continue;
                lowestFCostCell = openList[i];
            }
            
        }
        if (_firstRound && lowestFCostCell != _originTile)
        {
            _tilesDiscard.Add(lowestFCostCell);
            _firstRound = false;
        }
        return lowestFCostCell;
    }
    private List<TileRefactor> CalculatePath(TileRefactor endCell)
    {
        /* Se crean dos listas de vectores, una que almacenara el camino completo y la otra que devolvera el camino segun el numero de movimiento disponibles, luego la lista guarda el valor de la celda final
           y se guarda el tile actual del que hemos dado en la funcion
           */
        List<TileRefactor> pathComplete = new List<TileRefactor>();
        List<TileRefactor> pathReturn = new List<TileRefactor>();
        pathComplete.Add(endCell);

        TileRefactor currentCell = endCell;
        while (currentCell.dataNodeBase.GetCameFromCell() != null)
        {
            pathComplete.Add(currentCell.dataNodeBase.GetCameFromCell());
            currentCell = currentCell.dataNodeBase.GetCameFromCell();
        }
        pathComplete.Reverse();
        for (int i = 0; i < pathComplete.Count; i++)
        {
            if (_maxSteps > 0)
            {
                _maxSteps--;
                pathReturn.Add(pathComplete[i]);
            }
            else break;
        }
        return pathReturn;
    }
}
