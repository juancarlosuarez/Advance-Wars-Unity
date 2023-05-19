using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementLinear : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private DataCamera _dataCamera;

    public static event Action MoveInterfaceRight;
    public static event Action MoveInterfaceLeft;
    private void OnDisable() => SelectRefactorMovement.GetterDirectionSelect -= NewDirectionToCamera;
    private void OnEnable() => SelectRefactorMovement.GetterDirectionSelect += NewDirectionToCamera;
    

    public void NewDirectionToCamera(Vector2 direction)
    {
        var offsetX = (int)direction.x;
        var offsetY = (int)direction.y;

        if (offsetX > 0)
        {
            MoveCameraRightLeft(true);
        }
        if (offsetX < 0)
        {
            MoveCameraRightLeft(false);
        }
        if (offsetY > 0)
        {
            MoveCameraUpDown(true);
        }
        if (offsetY < 0)
        {
            MoveCameraUpDown(false);
        }
    }
    private void MoveCameraUpDown(bool isDirectionUp)
    {
        if (isDirectionUp)
        {
            ReachLimitNorth();
        }
        else
        {
            ReachLimitSouth();
        }
    }

    private void MoveCameraRightLeft(bool isDirectionRight)
    {
        if (isDirectionRight)
        {
            ReachLimitRight();
        }
        else
        {
            ReachLimitLeft();
        }
    }
    private void ReachLimitNorth()
    {
        if (_dataCamera.southCount == 0) _dataCamera.northCount++;
        
        //if (northCount == limitUPDOWN)
        if (_dataCamera.northCount >= _dataCamera.limitUpDown)
        {
            //northCount--;
            if (TryToMoveCameraNorth())
            {
                _dataCamera.northCount--;
                
                    _dataCamera.idTileCamera += 1;
                MoveCamera();
            }   
            return;
        }
        if (_dataCamera.southCount > 0)
        {
            _dataCamera.southCount--;
        }
    }
    private void ReachLimitSouth()
    {
        if (_dataCamera.northCount == 0) _dataCamera.southCount++;
        
        //if (southCount == limitUPDOWN)
        if (_dataCamera.southCount >= _dataCamera.limitUpDown)
        {
            //southCount--;
            if (TryToMoveCameraSouth())
            {
                _dataCamera.southCount--; //ESto es nuevo
                
                _dataCamera.idTileCamera -= 1;
                MoveCamera();
            }   
            return;
        }
        if (_dataCamera.northCount > 0)
        {
            _dataCamera.northCount--;
        }
    }

    private void ReachLimitRight()
    {
        if (_dataCamera.westCount == 0) _dataCamera.eastCount++;
        
        if (_dataCamera.eastCount >= _dataCamera.limitRight)
        {
            //_dataCamera.eastCount--;
            if (TryToMoveCameraRight())
            {
                MoveInterfaceLeft?.Invoke();
                _dataCamera.eastCount--;
                _dataCamera.idTileCamera += WorldScriptableObjects.GetInstance()._currentMapDisplayData.height;
                MoveCamera();
            }   
            return;
        }
        if (_dataCamera.westCount > 0)
        {
            _dataCamera.westCount--;
        }
    }
    private void ReachLimitLeft()
    {
        if (_dataCamera.eastCount == 0) _dataCamera.westCount++;
        
        if (_dataCamera.westCount >= _dataCamera.limitLeft)
        {
            if (TryToMoveCameraLeft())
            {
                MoveInterfaceRight?.Invoke();
                _dataCamera.westCount--;
                _dataCamera.idTileCamera -= WorldScriptableObjects.GetInstance()._currentMapDisplayData.height;
                MoveCamera();
            }   
            return;
        }
        if (_dataCamera.eastCount > 0)
        {
            _dataCamera.eastCount--;
        }
    }
    private bool TryToMoveCameraSouth()
    {
        bool thereIsTileWhenCameraMove = true;
        for (int i = 1; i < 5; i++)
        {
            var currentTileIdToCheck = _dataCamera.idTileCamera - i;
            if (WorldScriptableObjects.GetInstance()._currentMapDisplayData.allTilesEdgeSouth.Contains(currentTileIdToCheck))
                thereIsTileWhenCameraMove = false;
        }

        if (_dataCamera.southCount > 4) _dataCamera.southCount--;
        return thereIsTileWhenCameraMove;
    }
    private bool TryToMoveCameraNorth()
    {
        bool thereIsTileWhenCameraMove = true;
        for (int i = 1; i < 5; i++)
        {
            var currentTileIdToCheck = _dataCamera.idTileCamera + i;
            if (WorldScriptableObjects.GetInstance()._currentMapDisplayData.allTilesEdgeNorth.Contains(currentTileIdToCheck))
                thereIsTileWhenCameraMove = false;
        }
        if (_dataCamera.northCount > 4) _dataCamera.northCount--;
        return thereIsTileWhenCameraMove;
    }
    private bool TryToMoveCameraRight()
    {
        bool thereIsTileWhenCameraMove = true;
        for (int i = 0; i < 8; i++)
        {
            var height = WorldScriptableObjects.GetInstance()._currentMapDisplayData.height;
            int currentTileIDToCheck = _dataCamera.idTileCamera + height + (i * height);

            if (WorldScriptableObjects.GetInstance()._currentMapDisplayData.allTilesEdgeRight.Contains(currentTileIDToCheck))
                thereIsTileWhenCameraMove = false;
        }
        if (_dataCamera.eastCount > 8) _dataCamera.eastCount--;
        return thereIsTileWhenCameraMove;
    }
    private bool TryToMoveCameraLeft()
    {
        bool thereIsTileWhenCameraMove = true;
        for (int i = 0; i < 7; i++)
        {
            var height = WorldScriptableObjects.GetInstance()._currentMapDisplayData.height;
            int currentTileIDToCheck = _dataCamera.idTileCamera - height - (i * height);

            if (WorldScriptableObjects.GetInstance()._currentMapDisplayData.allTilesEdgeLeft.Contains(currentTileIDToCheck))
                thereIsTileWhenCameraMove = false;
        }
        if (_dataCamera.westCount > 7) _dataCamera.westCount--;
        return thereIsTileWhenCameraMove;
    }
    private void MoveCamera()
    {
        var allTiles = WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference;
        
        float xPositionCamera = allTiles[_dataCamera.idTileCamera].positionTileInGrid.x + .5f;
        float yPositionCamera = allTiles[_dataCamera.idTileCamera].positionTileInGrid.y;
        float zPositionCamera = transform.position.z;
            
        GameObject canvas = GameObject.Find("Canvas");
        float xPositionCanvas = xPositionCamera - _dataCamera.offsetCanvasPosition.x;
        float yPositionCanvas = yPositionCamera - _dataCamera.offsetCanvasPosition.y;
        
        transform.position = new Vector3(xPositionCamera, yPositionCamera, zPositionCamera);
        canvas.transform.position = new Vector2(xPositionCanvas, yPositionCanvas);
    }
}
