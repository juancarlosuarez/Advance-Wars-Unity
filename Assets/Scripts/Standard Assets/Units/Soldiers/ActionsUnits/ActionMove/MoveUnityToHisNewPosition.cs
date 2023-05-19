using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveUnityToHisNewPosition : MonoBehaviour
{
    private List<Tile> pathCurrent;
    private Tile tileWhereMyUnitIs;
    private Tile tileWhereIWannaToPutMyUnit;
    [FormerlySerializedAs("currentUnit")] [SerializeField] private Soldier current;

    private int x = 0;
    private bool isMoving = false;
    private Vector2 currentPosition;
    private bool unitWillMakeSomeActionAFterMove;

    public static MoveUnityToHisNewPosition sharedInstance;
    [Header("ScriptableObjects")]
    [SerializeField] ListActionUnit actionsAvailables;
    [SerializeField] ActionReference currentActionSelected;
    private void Awake() => sharedInstance = this;
    public void StartMoveUnit(List<Tile> _pathCurrent, bool _unitWillMakeSomeActionAFterMove)
    {
        pathCurrent = _pathCurrent;
        tileWhereMyUnitIs = pathCurrent[0];
        tileWhereIWannaToPutMyUnit = pathCurrent[pathCurrent.Count - 1];

        current = tileWhereMyUnitIs.occupiedSoldier;

        isMoving = true;
        unitWillMakeSomeActionAFterMove = _unitWillMakeSomeActionAFterMove;

        //while (isMoving)
        //{
        //    Patrol();
        //}
    }
    private void Update()
    {
        if (isMoving)
        {
            Patrol();
        }
    }
    private void Patrol()
    {
        if (pathCurrent != null)
        {
            StateGamePlay.GetInstance().ChangeState(GameState.GamePlayWithOutControls);
            float distance = Vector2.Distance(current.transform.position, pathCurrent[x].GetTilePosition());

            if (distance > 0)
            {
                MoveUnit(pathCurrent[x].GetTilePosition(), current);
            }
            else if (distance == 0)
            {
                x++;
            }
            if (x == pathCurrent.Count)
            {
                FinishPath();
            }
        }
    }
    private void MoveUnit(Vector2 destination, Soldier selected)
    {
        selected.transform.position = Vector2.MoveTowards(selected.transform.position, destination, 5 * Time.deltaTime);
    }
    private void FinishPath()
    {
        x = 0;
        isMoving = false;

        //UnitSelectorSystem.sharedInstance.DeselectUnit();


        tileWhereIWannaToPutMyUnit.setUnitComponent.SetUnitToTheGrid(current, tileWhereIWannaToPutMyUnit, false);
        StateGamePlay.GetInstance().ChangeState(GameState.GamePlayActionable);


        if (unitWillMakeSomeActionAFterMove) 
        {
            currentActionSelected.reference.Init();
            currentActionSelected.reference.Trigger();
        }
        //if (unitWillAttack) AttackAction.sharedInstance.MakeDamage();

        //uiControllerReference.reference.DestroyMenu();

    }
}
