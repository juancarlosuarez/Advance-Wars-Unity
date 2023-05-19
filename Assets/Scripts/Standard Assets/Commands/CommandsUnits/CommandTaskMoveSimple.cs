using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandTaskMoveSimple : ICommandTask
{
    private readonly Soldier _currentSoldier;
    private readonly List<TileRefactor> _currentPath;

    public CommandTaskMoveSimple()
    {
        _currentSoldier = WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference.occupiedSoldier;
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
        Vector2 destination = _currentPath[0].positionTileInGrid;
        bool itIsNecessaryUpdateTheData = false;
        bool itIsFinishPath = false;

        while (!itIsFinishPath)
        {
            if (itIsNecessaryUpdateTheData) destination = _currentPath[count].positionTileInGrid;
            
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
                itIsFinishPath = true;
                
                FinishExecute();
            }
            //await Task.Yield();
            yield return null;
        }
        
        
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
