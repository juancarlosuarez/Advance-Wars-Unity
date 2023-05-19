using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonR1L1Gameplay : MonoBehaviour
{
    private int CurrentPosition
    {
        get => _currentPosition;
        set
        {
            if (value <= _maxCount && value >= 0) _currentPosition = value;
            if (value > _maxCount) _currentPosition = 0;
            if (value < 0) _currentPosition = _maxCount;
        }
    }

    private int _currentPosition;
    private int _maxCount;
    private List<Soldier> _currentSoldiers;

    private void Start()
    {
        UpdateData();
    }

    private void UpdateData()
    {
        var currentPlayer = WorldScriptableObjects.GetInstance().currentPLayer.reference;
        var currentStats = WorldScriptableObjects.GetInstance().statsPlayersManager.GetStatsPlayer(currentPlayer);
        
        CurrentPosition = 0;
        _maxCount = currentStats.GetAllSoldiers().Count - 1;
        _currentSoldiers = currentStats.GetAllSoldiers();
    }

    private void R1Button()
    {
        if (_currentSoldiers.Count == 0) return;
        CurrentPosition++;
        var currentPosition = _currentSoldiers[_currentPosition].occupiedTileRefactor.spawnID;
        CommandQueue.GetInstance.AddCommand(new CommandMoveSelectorImmediatly(currentPosition), false);
        CommandQueue.GetInstance.AddCommand(new CommandMoveCameraInmediatly(currentPosition), false);

    }

    private void L1Button()
    {
        if(_currentSoldiers.Count == 0) return;
        CurrentPosition--;
        var currentPosition = _currentSoldiers[_currentPosition].occupiedTileRefactor.spawnID;
        CommandQueue.GetInstance.AddCommand(new CommandMoveSelectorImmediatly(currentPosition), false);
        CommandQueue.GetInstance.AddCommand(new CommandMoveCameraInmediatly(currentPosition), false);
    }

    private void OnDisable()
    {
        GamePlayControls.PressButtonL1 -= L1Button;
        GamePlayControls.PressButtonR1 -= R1Button;
        CommandUpdateR1L1Stats.Update -= UpdateData;
    }

    private void OnEnable()
    {
        GamePlayControls.PressButtonL1 += L1Button;
        GamePlayControls.PressButtonR1 += R1Button;
        CommandUpdateR1L1Stats.Update += UpdateData;
        //Por unidad muerta
        //Por nueva unidad se debe actualizar
    }
}
