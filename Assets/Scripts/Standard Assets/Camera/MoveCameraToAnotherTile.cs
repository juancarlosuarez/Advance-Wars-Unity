using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraToAnotherTile : MonoBehaviour
{
    [SerializeField] private DataCamera _dataCamera;
    private bool _newTileHadEnoughSpace;

    private int _idTileSelectedVirtual;
    
    public static event Action MoveInterfaceRight;
    public static event Action MoveInterfaceLeft;
    
    private void OnDisable() => CommandMoveCameraInmediatly.SendIDToManagerCamera -= CalculateCameraFrame;
    private void OnEnable() => CommandMoveCameraInmediatly.SendIDToManagerCamera += CalculateCameraFrame;
    private void CalculateCameraFrame(int idNewTileSelected)
    {
        _dataCamera.idTileSelected = idNewTileSelected;
        _idTileSelectedVirtual = idNewTileSelected;
        _newTileHadEnoughSpace = true;
        //This work like that, because when the condiction is true, the system got already the idTileCamera correct, then
        //i just need to worry about to move the camera.
        if (!SidesHadTiles())
        {
            MoveCamera();
            return;
        }
        _dataCamera.idTileCamera = _dataCamera.idTileSelected;
            print("no ahi lados en esta mierda loco");
         MoveCamera();
    }

    private void MoveCamera()
    {
        var allTiles = WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference;

        float xPositionCamera = allTiles[_dataCamera.idTileCamera].positionTileInGrid.x + .5f;
        float yPositionCamera = allTiles[_dataCamera.idTileCamera].positionTileInGrid.y;
        print("El tile de la camara es " + _dataCamera.idTileCamera);
        float zPositionCamera = transform.position.z;
        
        GameObject canvas = GameObject.Find("Canvas");
        float xPositionCanvas = xPositionCamera - _dataCamera.offsetCanvasPosition.x;
        float yPositionCanvas = yPositionCamera - _dataCamera.offsetCanvasPosition.y;
    
        transform.position = new Vector3(xPositionCamera, yPositionCamera, zPositionCamera);
        canvas.transform.position = new Vector2(xPositionCanvas, yPositionCanvas);
        
        CalculateCorrectValues();
    }
    private void CalculateCorrectValues()
    {
        var positionTileSelected = WorldScriptableObjects.GetInstance().allTilesInTheGrid
            .reference[_dataCamera.idTileSelected].positionTileInGrid;
        var positionTileCamera = WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference[_dataCamera.idTileCamera].positionTileInGrid;
    
        var differenceVector = positionTileSelected - positionTileCamera;
        OffsetCorrectValues(differenceVector);
    }
    private void OffsetCorrectValues(Vector2 vectorToOffset)
    {
        var offsetX = (sbyte)vectorToOffset.x;
        var offsetY = (sbyte)vectorToOffset.y;

        _dataCamera.eastCount = 0;
        _dataCamera.westCount = 0;
        _dataCamera.northCount = 0;
        _dataCamera.southCount = 0;

        if (offsetX > 0)
        {
            MoveInterfaceLeft?.Invoke();
            _dataCamera.eastCount = (byte)offsetX;
        }
        
        if (offsetX < 0)
        {
            MoveInterfaceRight?.Invoke();
             offsetX *= -1;
             _dataCamera.westCount = (byte)offsetX;
        }
        if (offsetY > 0) _dataCamera.northCount = (byte)offsetY;
        if (offsetY < 0)
        {
            offsetY *= -1;
            _dataCamera.southCount = (byte)offsetY;
        }
    }
    
    // private bool TileSelectedItIsInCorner()
    // {
    //     var currentMapData = WorldScriptableObjects.GetInstance()._currentMapDisplayData;
    //
    //     if (currentMapData.allTilesCornerLeftDown.Contains(_dataCamera.idTileSelected))
    //     {
    //         Debug.Log(1);
    //         _dataCamera.idTileCamera = currentMapData.positionTileCameraLeftDown;
    //         return true;
    //     }
    //     if (currentMapData.allTilesCornerLeftUp.Contains(_dataCamera.idTileSelected))
    //     {Debug.Log(2);
    //         _dataCamera.idTileCamera = currentMapData.positionTileCameraLeftUp;
    //         return true;
    //     }
    //     if (currentMapData.allTilesCornerRightDown.Contains(_dataCamera.idTileSelected))
    //     {Debug.Log(3);
    //         _dataCamera.idTileCamera = currentMapData.positionTileCameraRightDown;
    //         return true;
    //     }
    //     if (currentMapData.allTilesCornerRightUp.Contains(_dataCamera.idTileSelected))
    //     {Debug.Log(4);
    //         _dataCamera.idTileCamera = currentMapData.positionTileCameraRightUp;
    //         return true;
    //     }
    //     return false;
    // }

    private bool SidesHadTiles()
    {
        //This is done this way, because inside of this for function, i calculate the bool _newTileHad.., but i need
        //to calculate all the sides and make it a function with && just dont work because dont go throught all them.
        //Inside each function, i calculate two cases, first one i check if the tileSelected its in a edge of the map,
        //in that case i need to manually change the position of the camera and the second one, check how much is the
        //offset a need to apply for each side.
        RightSide();
        LeftSide();
        NorthSide();
        SouthSide();
        return _newTileHadEnoughSpace;
        
    }

    private void RightSide()
    {
        var height = WorldScriptableObjects.GetInstance()._currentMapDisplayData.height;
        var currentEdges = WorldScriptableObjects.GetInstance()._currentMapDisplayData.allTilesEdgeRight;

        if (currentEdges.Contains(_dataCamera.idTileSelected))
        {
            _dataCamera.idTileCamera = _dataCamera.idTileSelected - height - (7 * height);
            _newTileHadEnoughSpace = false;
            return;
        }
        
        for (int i = 0; i < 8; i++)
        {
            int currentTileIDToCheck = _dataCamera.idTileSelected + height + (i * height);
            if (currentEdges.Contains(currentTileIDToCheck))
            {
                RightSideOffSet(i);
                _newTileHadEnoughSpace = false;
                break;
            }
        }
    }
    private void RightSideOffSet(int currentFailPosition)
    {
        var currentPosition = 8 - currentFailPosition - 2;
        if (currentPosition < 0) currentPosition = 0;
        var height = WorldScriptableObjects.GetInstance()._currentMapDisplayData.height;
        _dataCamera.idTileCamera = _dataCamera.idTileSelected - height - (currentPosition * height);
        _idTileSelectedVirtual = _dataCamera.idTileCamera;
    }
    private void LeftSide()
    {
        var height = WorldScriptableObjects.GetInstance()._currentMapDisplayData.height;
        var currentEdges = WorldScriptableObjects.GetInstance()._currentMapDisplayData.allTilesEdgeLeft;
        if (currentEdges.Contains(_dataCamera.idTileSelected))
        {
            _dataCamera.idTileCamera = _dataCamera.idTileSelected + height + (6 * height);
            _idTileSelectedVirtual = _dataCamera.idTileCamera;
            _newTileHadEnoughSpace = false;
        }
        for (int i = 0; i < 7; i++)
        {
            int currentTileIDToCheck = _dataCamera.idTileSelected - height - (i * height);
            if (currentEdges.Contains(currentTileIDToCheck))
            {
                LeftSideOffSet(i);
                _newTileHadEnoughSpace = false;
                return;
            }
        }
    }
    private void LeftSideOffSet(int currentFailPosition)
    {
        var currentPosition = 7 - currentFailPosition - 2;
            if (currentPosition < 0) currentPosition = 0; 
        var height = WorldScriptableObjects.GetInstance()._currentMapDisplayData.height;
        _dataCamera.idTileCamera = _dataCamera.idTileSelected + height + (currentPosition * height);
        _idTileSelectedVirtual = _dataCamera.idTileCamera;
    }
    private void NorthSide()
    {
        var currentEdges = WorldScriptableObjects.GetInstance()._currentMapDisplayData.allTilesEdgeNorth;
        if (currentEdges.Contains(_dataCamera.idTileSelected))
        {
            _newTileHadEnoughSpace = false;
            _dataCamera.idTileCamera -= 4;
            return;
        }
        for (int i = 1; i < 5; i++)
        {
            var currentTileIdToCheck = _dataCamera.idTileSelected + i;
            if (currentEdges.Contains(currentTileIdToCheck))
            {
                NorthSideOffset(i);
                _newTileHadEnoughSpace = false;
                break;
            }
        }
    }
    private void NorthSideOffset(int currentFailPosition)
    {
        var currentPosition = 4 - currentFailPosition;
        
        _dataCamera.idTileCamera = _idTileSelectedVirtual - currentPosition;
    }
    private void SouthSide()
    {
        var currentEdges = WorldScriptableObjects.GetInstance()._currentMapDisplayData.allTilesEdgeSouth;
        if (currentEdges.Contains(_dataCamera.idTileSelected))
        {
            _dataCamera.idTileCamera += 4;
            _newTileHadEnoughSpace = false;
            return;
        }
        for (int i = 1; i < 5; i++)
        {
            var currentTileIdToCheck = _dataCamera.idTileSelected - i;
            if (currentEdges.Contains(currentTileIdToCheck))
            {
                SouthSideOffset(i);
                _newTileHadEnoughSpace = false;
                break;
            }
        }
    }
    private void SouthSideOffset(int currentFailPosition)
    {
        var currentPosition = 5 - currentFailPosition;
        _dataCamera.idTileCamera = _dataCamera.idTileCamera + currentPosition;
    }
}
