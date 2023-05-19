using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/ContainerStruct/SliderMenu/ManagerPositions")]
public class ManagerPositionsListRadialsMenu : ScriptableObject
{
    private int _selectPositionList = 2;
    private int _finalPositionList = 4;
    private int _initialPosition;

    public int _maxValueForList;
    public int _maxValueForDictionary;
    public int GetMaxValueForList => _maxValueForList;
    public int GetMaxValueForDictionary => _maxValueForDictionary;
    private int InitialPositionList
    {
        get => _initialPosition;
        set
        {
            if (value <= _maxValueForList && value >= 0) _initialPosition = value;
            if (value > _maxValueForList) _initialPosition = 0;
            if (value < 0) _initialPosition = _maxValueForList;
        }
    }
    private int SelectPositionList
    {
        get => _selectPositionList;
        set
        {
            if (value <= _maxValueForList && value >= 0) _selectPositionList = value;
            if (value > _maxValueForList) _selectPositionList = 0;
            if (value < 0) _selectPositionList = _maxValueForList;
        }
    }
    private int FinalPositionList
    {
        get => _finalPositionList;
        set
        {
            if (value <= _maxValueForList && value >= 0) _finalPositionList = value;
            if (value > _maxValueForList) _finalPositionList = 0;
            if (value < 0) _finalPositionList = _maxValueForList;
        }
    }
    private int _currentKey;
    private int CurrentKeyForDictionary
    {
        get => _currentKey;
        set
        {
            if (value >= 0 && value <= _maxValueForDictionary) _currentKey = value;
            if (value > _maxValueForDictionary) _currentKey = 0;
            if (value < 0) _currentKey = _maxValueForDictionary;
        }
    }
    public void IncreaseValues()
    {
        InitialPositionList++;
        SelectPositionList++;
        FinalPositionList++;
    }
    public void DecreaseValues()
    {
        InitialPositionList--;
        SelectPositionList--;
        FinalPositionList--;
    }
    public void ChangeKeyDictionary(bool isMovementUp)
    {
        if (isMovementUp) CurrentKeyForDictionary++;
        else CurrentKeyForDictionary--;
    }
    public int GetInitPosition()
    {
        int returner = _initialPosition;
        return returner;
    }
    public int GetSelectPosition()
    {
        int returner = _selectPositionList;
        return returner;
    }
    public int GetFinalPosition()
    {
        int returner = _finalPositionList;       
        return returner;
    }

    public int GetCurrentKeyDictionary()
    {
        int returner = _currentKey;
        return returner;
    }

    public void ResetValues()
    {
        _currentKey = 0;
        _initialPosition = 0;
        _selectPositionList = 2;
        _finalPositionList = 4;
    }
}
