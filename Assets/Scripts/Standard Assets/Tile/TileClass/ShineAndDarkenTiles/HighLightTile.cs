using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightTile
{
    public void PutHighLightTile (int idListHighLight, List<Tile> tileHighLight)
    {
        ResetHighLight(tileHighLight);

        foreach (Tile c in tileHighLight) c.highLightObject[idListHighLight].SetActive(true);
        
        HighLightTiles.sharedInstance.AddNewTiles(tileHighLight);
    }
    public void ResetHighLight(List<Tile> tilesToReset)
    {
        foreach (Tile c in tilesToReset)
        {
            foreach (GameObject e in c.highLightObject)
            {
                e.SetActive(false);
            }
        }
        HighLightTiles.sharedInstance.RemoveAllTiles(tilesToReset);

    }
    

     
        

}
