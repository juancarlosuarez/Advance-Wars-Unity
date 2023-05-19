using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRefactorMovement : MonoBehaviour
{
    private FindTileWithNormalizedVector _findPossibleTile;
    private float _delayToChangePositionSelect;
    private readonly float _delayToChangePositionSelectStore = .15f;
    private bool _canMoveSelect = true;

    public static event Action SelectorIsMoving;
    public static event Action<Vector2> GetterDirectionSelect;

    public float limitSize = 1.6f;
    private Transform currentTranform;
    private void Awake()
    {
        _findPossibleTile = new FindTileWithNormalizedVector();
        currentTranform = transform;
    }

    private void Update()
    {
        var currentTransformLocalScale = currentTranform.localScale;
        currentTranform.localScale = new Vector2(currentTransformLocalScale.x + Time.deltaTime * 0.6f,
            currentTransformLocalScale.y + Time.deltaTime * 0.6f);
        
        if (currentTranform.localScale.x > limitSize) currentTranform.localScale = Vector3.one;

    }

    private void AnimationSelect()
    {
        
    }
    private void OnDisable()
    {
        GamePlayControls.PressSomeArrows -= MoveSelect;
        GamePlayControls.StopPressButtonArrows -= StickReset;
        CommandMoveSelectorImmediatly.MoveSelectorImmediatly -= MoveSelectImmediately;
    }

    private void OnEnable()
    {
        GamePlayControls.PressSomeArrows += MoveSelect;
        GamePlayControls.StopPressButtonArrows += StickReset;
        CommandMoveSelectorImmediatly.MoveSelectorImmediatly += MoveSelectImmediately;
    }

    private void MoveSelect(Vector2 direction)
    {
        if (_canMoveSelect)
        {
            TileRefactor possibleNewTile = _findPossibleTile.GetTileByVectorNormalized(direction);
            if (possibleNewTile == null) return;
            StartDelay();
            WorldScriptableObjects.GetInstance().tileSelected.reference = possibleNewTile;
            WorldScriptableObjects.GetInstance().lastDirection.reference = direction;
            transform.position = new Vector3(possibleNewTile.positionTileInGrid.x, possibleNewTile.positionTileInGrid.y,
                transform.position.z);
            GetterDirectionSelect?.Invoke(direction);
            SelectorIsMoving?.Invoke();

            CommandQueue.GetInstance.AddCommand(new PutArrowCommand(), false);
            SoundManager._sharedInstance.PlayEffectSound(EffectNames.MoveBetweenGrid);
        }
        else
        {
            Chronometer();
        }
    }

    private void MoveSelectImmediately(int idTile)
    {
        
        var allTiles = WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference;
        WorldScriptableObjects.GetInstance().tileSelected.reference = allTiles[idTile];
        var tileSelected = WorldScriptableObjects.GetInstance().tileSelected.reference;

        transform.position = new Vector3(tileSelected.positionTileInGrid.x, tileSelected.positionTileInGrid.y,
            transform.position.z);
        WorldScriptableObjects.GetInstance().currentVoxelID.reference = idTile;
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.MoveBetweenGrid);
        //SelectorIsMoving?.Invoke();

    }
    
    private void StartDelay()
    {
        _delayToChangePositionSelect = _delayToChangePositionSelectStore;
        _canMoveSelect = false;
    }
    private void Chronometer()
    {
        if (_delayToChangePositionSelect > 0) _delayToChangePositionSelect -= Time.deltaTime;
        else _canMoveSelect = true;
    }
    private void StickReset()
    {
       // if (!(StateGamePlay.sharedInstance.IsStateGamePlayActionable() || StateGamePlay.sharedInstance.IsStateEditingMap())) return;
        _canMoveSelect = true;    
    }
}