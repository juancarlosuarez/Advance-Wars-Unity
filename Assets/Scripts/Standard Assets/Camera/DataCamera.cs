using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCamera : MonoBehaviour
{
    public byte northCount;
    public byte southCount;
    public byte westCount;
    public byte eastCount;

    //This limit is like this, because the camera is the size of 16 X 9 Tiles.
    public readonly byte limitUpDown = 3;
    public readonly byte limitRight = 7;
    public readonly byte limitLeft = 6;
    
    public readonly Vector2 offsetCanvasPosition = new Vector2(7.5f, 4); 
    
    public int idTileCamera;
    public int idTileSelected;
}
