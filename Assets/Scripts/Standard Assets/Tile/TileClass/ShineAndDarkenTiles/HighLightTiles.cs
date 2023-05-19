using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighLightTiles : MonoBehaviour
{
    //"BORRAR ESTA MIERDA
    private HashSet<Tile> currentTilesOn = new HashSet<Tile>();
    private enum HighLightTypes
    {
        PathFindingCurrentPlayer, PathFindingEnemy, ObjectiveAttack, RangeActionCurrentPlayer, RangeActionEnemy
    }
    public static HighLightTiles sharedInstance;
    private void Awake() => sharedInstance = this;
    public void AddNewTiles(List<Tile> tilesToCheck)
    {
        foreach(Tile tile in tilesToCheck)
        {
            if (currentTilesOn.Contains(tile)) continue;

            currentTilesOn.Add(tile);
        }
    }
    public void RemoveAllTiles(List<Tile> tilesToCheck)
    {
        try
        {
            foreach (Tile tile in tilesToCheck)
            {
                if (!currentTilesOn.Contains(tile)) continue;

                currentTilesOn.Remove(tile);
            }
        }
        catch (MissingReferenceException u)
        {
            print(u);
        }
    }
    public void TurnOffAllTiles()
    {
        foreach(Tile c in currentTilesOn)
        {
            foreach (GameObject u in c.highLightObject)
            {
                u.SetActive(false);
            }
        }
        //RemoveCurrentList();
    }
    // private void RemoveCurrentList()
    // {
    //     int numberOfLoops = currentTilesOn.Count;
    //
    //     for (int i = 0; i < numberOfLoops; i++)
    //     {
    //         currentTilesOn.Remove(currentTilesOn[0]);
    //     }
    // }
    public List<Tile> GetCurrentTilesON()
    {
        return currentTilesOn.ToList();
    }
}

