using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Object = System.Object;

public class CommandTaskMove : ICommandTask
{
    private readonly TileRefactor _selectedTile;
    private readonly TileRefactor _selectTileWithUnit;

    private readonly Soldier _currentSoldier;

    private readonly List<TileRefactor> _currentPath;

    public CommandTaskMove()
    {
        _selectedTile = WorldScriptableObjects.GetInstance().tileSelected.reference;
        _selectTileWithUnit = WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference;
        
        _currentSoldier = WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference.occupiedSoldier;
        //_currentPath = PathFindingRefactor.GetInstance().StartCalculatePath(_selectTileWithUnit, _selectedTile);
        _currentPath = WorldScriptableObjects.GetInstance().allTilesWHereMyArrowsWillBe.reference;
    }
    public IEnumerator Execute()
    {
        PlayerController.sharedInstance.StopControls();
        CalculatePathArrow.GetInstance().StopShowArrows();
        
        CommandQueue.GetInstance.AddCommand(new ActiveSelectCommand(), false);
        if (!WorldScriptableObjects.GetInstance().willUnitMove.boolReference)
        {
            FinishExecute();
            yield break;
        }
        int count = 0;
        Vector2 destination = _currentPath[count].positionTileInGrid;
        Vector2 currentDirection = _currentSoldier.occupiedTileRefactor.positionTileInGrid -
                                   _currentPath[count].positionTileInGrid;
        PrintDirection(currentDirection);
        bool itIsNecessaryUpdateTheData = false;
        bool itIsFinishThePath = false;
        
        while (!itIsFinishThePath)
        {
            // if (tokenCancel.IsCancellationRequested)
            // {
            //     Debug.Log("Move Command Canceled");
            //     tokenCancel.Dispose();
            //     return;
            // }
            if (itIsNecessaryUpdateTheData)
            {
                destination = _currentPath[count].positionTileInGrid;
                currentDirection = _currentPath[count].positionTileInGrid - _currentPath[count - 1].positionTileInGrid;
                PrintDirection(currentDirection);
                itIsNecessaryUpdateTheData = false;
            }
            
            float distanceBetweenBothOfThem = Vector2.Distance(_currentSoldier.transform.position, destination);
            

            if (distanceBetweenBothOfThem > 0)
            {
                _currentSoldier.gameObject.transform.position =
                    Vector2.MoveTowards(_currentSoldier.transform.position, destination, 5 * Time.deltaTime);
                
            }
            else if (distanceBetweenBothOfThem == 0)
            {
                itIsNecessaryUpdateTheData = true;
                count++;
            }
            if (count == _currentPath.Count)
            {
                itIsFinishThePath = true;
                FinishMove();
                FinishExecute();
            }
            //await Task.Yield();
            yield return null;
        }
        // if (!tokenCancel.IsCancellationRequested)
        // {
        // }
        // else
        // {
        //     Debug.Log("Hijo de la gran puta");
        //     tokenCancel.Dispose();
        // }
    }

    private void PrintDirection(Vector2 direction)
    {
        if (direction == Vector2.down)
        {
            _currentSoldier.animator.Play("DownMovement");
            Debug.Log("Down");
        }
        if (direction == Vector2.up)
        {
            _currentSoldier.animator.Play("UpMovement");
            Debug.Log("Up");
        }
        if (direction == Vector2.right)
        {
            _currentSoldier.animator.Play("HorizontalMove");
            _currentSoldier.spriteRenderer.flipX = true;
            Debug.Log("Right");
        }

        if (direction == Vector2.left)
        {
            _currentSoldier.animator.Play("HorizontalMove");
            _currentSoldier.spriteRenderer.flipX = false;
            Debug.Log("Left");
        }
    }
    public void FinishExecute()
    {
        CommandQueue.GetInstance.AddCommand(new DeselectUnitCommand(), false);
        CommandQueue.GetInstance.CurrentCommandFinish();
        PlayerController.sharedInstance.ChangeControlToGamePlay();
        //We make unusable the unit until the next turn.
        var currentTileUnit = WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference;
        var currentSoldier = currentTileUnit.occupiedSoldier;
        CommandQueue.GetInstance.AddCommand(new CommandDisableUnit(currentSoldier), false);
    }

    private void FinishMove()
    {
        _currentSoldier.animator.Play("Indle");
        _currentSoldier.spriteRenderer.flipX = false;
         StopConquerBuild();
        //We put the unit in his new position.
        UnsetUnitToTheGridRefactor.UnSetUnit(_currentSoldier);
        var setUnitsToTheGrid = new SetUnitToTheGridRefactor();
        setUnitsToTheGrid.SetUnit(_selectedTile, _currentSoldier);
        //We fix the references here.
        WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference = _currentPath.Last();
    }

    private void StopConquerBuild() => CommandQueue.GetInstance.ExecuteCommandImmediately(new CommandStopConquerCity(), false);
    
}
