using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public static PathFinding sharedInstance;
    private Tile _startCell;
    private Tile _endCell;

    private int numberMoveAvailable;


    [SerializeField] ListTilesReference tilesPathHighLight;
    private void Awake()
    {
        sharedInstance = this;
    }
    public List<Tile> FindPathDefinitive(Tile tileStart, Tile tileFinal)
    {
        if (!IsTileInPathAvailable(tileFinal)) return null;
        
        SetValues(tileStart, tileFinal);
        ResetWalk();

        return CalculatePath();
    }
    public bool IsTileInPathAvailable(Tile tileFinal) //Se encarga de determinar si el ultimo tile del camino, se encuentra
    //dentro del rango establecido.
    {
        if (tilesPathHighLight.reference.Contains(tileFinal)) return true;

        return false;
    }
    private void SetValues(Tile tileStart, Tile tileFinal)
    {
        numberMoveAvailable = tileStart.occupiedSoldier.numberMoveAvailable;
        _startCell = tileStart;
        _endCell = tileFinal;
    }
    private void ResetWalk() //Reseteamos los valores de los Tiles que vamos a usar, para que no haya problemas
    //con anteriores caminos.
    {
        foreach (var tile in tilesPathHighLight.reference)
        {
            tile.nodeBaseComponent.SetGCost(int.MaxValue);
            tile.nodeBaseComponent.CalculateFCost();
            tile.nodeBaseComponent.SetCameFromCell(null);
        }
        _startCell.nodeBaseComponent.SetGCost(0);
        _startCell.nodeBaseComponent.SetHCost(CalculateDistanceCost(_startCell, _endCell));
        _startCell.nodeBaseComponent.CalculateFCost();
    }
    private List<Tile> CalculatePath()
    {
        List<Tile> openList = new List<Tile> { _startCell };
        List<Tile> closedList = new List<Tile>();
        /* Ya reseteado nuestro camino, creamos un bucle por cada elemento metido dentro de nuestra lista abierta, luego calculamos el valor f mas bajo
         Primero se van comprobando los elementos de la lista, si el tile a revisar es el tileFinal se calcula finalmente el camino, sino pues se borra el elemento
         de la lista abierta y se anade a la cerrada para evitar anadirlo de nuevo. Luego recorremos los 4 tiles adyacentes y calculamos unos valores para determinar
         si es valido y se anade a la lista abierto, si no esta ya en ella, y se repite el bucle con el valor mas optimo al principio de la funcion. Importante
         que antes de calcular los valores adyacente, hemos filtrado los resultados a los que solos estan dentro del rango de accion permitido, para que no pueda
         hacer cosas raras el pathfinding.
         */
        
        
        while (openList.Count > 0) 
        {
            Tile currentCell = GetLowestFCostCell(openList);

            if (currentCell == _endCell)
            {
                return CalculatePathDefinitive(_endCell);
            }
            openList.Remove(currentCell);
            closedList.Add(currentCell);

            List<Tile> currentCellStickFilter =
                currentCell.stickNeightbourTile.Where(x => tilesPathHighLight.reference.Contains(x)).ToList();
            foreach (Tile neighbourCell in currentCellStickFilter)
            {
                if (closedList.Contains(neighbourCell)) continue;

                int tentativeCost = currentCell.nodeBaseComponent.GetGCost() + CalculateDistanceCost(currentCell, neighbourCell);
                if (tentativeCost < neighbourCell.nodeBaseComponent.GetGCost())
                {
                    neighbourCell.nodeBaseComponent.SetCameFromCell(currentCell);
                    neighbourCell.nodeBaseComponent.SetGCost(tentativeCost);
                    neighbourCell.nodeBaseComponent.SetHCost(CalculateDistanceCost(neighbourCell, _endCell));
                    neighbourCell.nodeBaseComponent.CalculateFCost();
                }
                if (!openList.Contains(neighbourCell))
                {
                    openList.Add(neighbourCell);
                }
            }
        }
        return null;
    }
    private Tile GetLowestFCostCell(List<Tile> openList)
    {
        Tile lowestFCostCell = openList[0];
        for (int i = 0; i < openList.Count; i++)
        {
            if (openList[i].nodeBaseComponent.GetFCost() < lowestFCostCell.nodeBaseComponent.GetFCost())
            {
                lowestFCostCell = openList[i];
            }
        }
        return lowestFCostCell;
    }
    private int CalculateDistanceCost(Tile startCell, Tile endCell)
    {
        int diagonalCost = 14;
        int straightCost = 10;

        int xDistance = Mathf.Abs((int)startCell.GetTilePosition().x - (int)endCell.GetTilePosition().x);
        int yDistance = Mathf.Abs((int)startCell.GetTilePosition().y - (int)endCell.GetTilePosition().y);
        int remaining = Mathf.Abs(xDistance - yDistance);

        return diagonalCost * Mathf.Min(xDistance, yDistance) + straightCost * remaining;
    }

    private List<Tile> CalculatePathDefinitive(Tile endCell)
    {
        /* Se crean dos listas de vectores, una que almacenara el camino completo y la otra que devolvera el camino segun el numero de movimiento disponibles, luego la lista guarda el valor de la celda final
            y se guarda el tile actual del que hemos dado en la funcion
            */
        List<Tile> pathComplete = new List<Tile>();
        List<Tile> pathReturn = new List<Tile>();
        pathComplete.Add(endCell);

        Tile currentCell = endCell;
        while (currentCell.nodeBaseComponent.GetCameFromCell() != null)
        {
            pathComplete.Add(currentCell.nodeBaseComponent.GetCameFromCell());
            currentCell = currentCell.nodeBaseComponent.GetCameFromCell();
        }
        pathComplete.Reverse();
        for (int i = 0; i < pathComplete.Count; i++)
        {
            if (numberMoveAvailable > 0)
            {
                numberMoveAvailable--;
                pathReturn.Add(pathComplete[i]);
            }
            else break;
        }
        return pathReturn;
    }


}
