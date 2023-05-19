using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Extensibles;
using UnityEngine;

public class MenuOptionsTileSelector : MonoBehaviour
{
    [SerializeField] private ListTilesReference allPrefabsTiles; //Este menu obtiene sus valores de los Tiles

    private List<ElementsTileSelectorMenu> _allTilesShowedAndHidden;
    private List<ElementsTileSelectorMenu> _allTilesCurrentShowed;
    private List<Vector2> _positionElementShowed;
    [SerializeField] private ElementsTileSelectorMenu prefab;

    public static event Action triggerBottonMenuTileSelector;
    
    private bool _isAlreadySpawned = false;
    private int _initialPositionList = 0;
    private int _selectPositionList = 2;
    private int _finalPositionList = 4;
    private int _localPosition;

    private ISliderMenuOptions _menuOptionsUnit;
    public int InitialPositionList { get => _initialPositionList;
        set
        {
            if (value <= GetCountListElement() && value >= 0) _initialPositionList = value;
            if (value > GetCountListElement()) _initialPositionList = 0;
            if (value < 0) _initialPositionList = GetCountListElement();
        }
    }
    public int SelectPositionList
    {
        get => _selectPositionList;
        set
        {
            if (value <= GetCountListElement() && value >= 0) _selectPositionList = value;
            if (value > GetCountListElement()) _selectPositionList = 0;
            if (value < 0) _selectPositionList = GetCountListElement();
        }
    }
    public int FinalPositionList
    {
        get => _finalPositionList;
        set
        {
            if (value <= GetCountListElement() && value >= 0) _finalPositionList = value;
            if (value > GetCountListElement()) _finalPositionList = 0;
            if (value < 0) _finalPositionList = GetCountListElement();
        }
    }
    private int LocalPosition
    {
        get => _localPosition;
        set
        {
            if (value <= GetCountListElement() && value >= 0) _localPosition = value;
            if (value > GetCountListElement()) _localPosition = 0;
            if (value < 0) _localPosition = GetCountListElement();
        }
    }

    [Header("Scriptable Objects")] 
    [SerializeField] private SOElementSelectedEditorMap elementSelected;
    public void HighLight(int id, int countUI)
    {
        
    }

    public void LowLight(int id, int countUI)
    {
        
    }
    public void CloseMenu()
    {
        //UIManagerGamePlay.sharedInstance.OpenEditorFromStart();
        StateGamePlay.GetInstance().ChangeState(GameState.EditingMap);
        PlayerController.sharedInstance.ChangeControlToEditMap();
        gameObject.SetActive(false);
    }
    public void Trigger()
    {
        elementSelected.SetValuesElementMenuSelectorTile(_allTilesShowedAndHidden[SelectPositionList]);
        triggerBottonMenuTileSelector?.Invoke(); //Informa a menu del elemento seleccionado.
    }
    public int GetCountListElement() => allPrefabsTiles.reference.Count - 1;

    public async void ActiveAgainElements()
    {
        gameObject.SetActive(true);
        int count = 0;
        foreach (var c in _allTilesCurrentShowed)
        {
            c.transform.localScale = Vector3.one;
            c.transform.position = transform.position;
            
            count++;
        }
        await MoveElements();
    }
    public void MoveElementsInTheSelector(bool isMovementRight)
    {
        
        foreach (var tileCurrentShowed in _allTilesCurrentShowed)
        {
            tileCurrentShowed.gameObject.SetActive(false);
        }
        _allTilesCurrentShowed.Clear();
        for (int i = 0; i <= 4; i++)
        {
            if (i == 0) LocalPosition = _initialPositionList;
            
            _allTilesCurrentShowed.Add(_allTilesShowedAndHidden[_localPosition]);
            _allTilesCurrentShowed[i].gameObject.SetActive(true);
            _allTilesCurrentShowed[i].transform.localScale = Vector3.one;
            _allTilesCurrentShowed[i].transform.position = _positionElementShowed[i];
            
            LocalPosition++;
        }
    }

    private void FinishMoveElements()
    {
        PlayerController.sharedInstance.ChangeControlToUI();
    }
    public async void DisplayElements()
    {
        //UIManagerGamePlay.sharedInstance.CloseEditorFromStart();
        if (_isAlreadySpawned) return;

        _isAlreadySpawned = true;
        _allTilesCurrentShowed = new List<ElementsTileSelectorMenu>();
        _allTilesShowedAndHidden = new List<ElementsTileSelectorMenu>();
        _positionElementShowed = new List<Vector2>();
        
        Vector2 position = transform.position;
        Vector2 positionElement = new Vector2();
        float x = position.x;
        float y = position.y;
        float offset = 4f;
        for (int i = 0; i < allPrefabsTiles.reference.Count; i++) 
        {
            var elementDisplayed = InstantiateElement(i, position);
            OffSetPosition(ref offset, i, ref positionElement, x, y);
            ModifyLists(elementDisplayed, positionElement, i);
        }
        await MoveElements();
    }

    private async Task MoveElements()
    {
        PlayerController.sharedInstance.StopControls();
        
        var listTask = new Task[_allTilesCurrentShowed.Count];
        
        for (int i = 0; i < _allTilesCurrentShowed.Count; i++)
        {
            // listTask[i] = _allTilesCurrentShowed[i].transform
            //     .MoveElement(_positionElementShowed[i]);
        }
        await Task.WhenAll(listTask);
        FinishMoveElements();
    }
    private void OffSetPosition(ref float offset, int count, ref Vector2 positionElement, float x, float y)
    {
        if (count < 2)
        {
            positionElement = new Vector2(x - offset, y);
            offset -= 2;
        }
        if (count == 2)
        {
            positionElement = transform.position;
            offset = 0;
        }
        if (count > 2)
        {
            offset += 2;
            positionElement = new Vector2(x + offset, y);
        }
    }
    private ElementsTileSelectorMenu InstantiateElement(int count, Vector2 pointSpawn)
    {
        var tilesDisplayed = Instantiate(prefab, pointSpawn, Quaternion.identity);
        tilesDisplayed.InitElementMenuTile(allPrefabsTiles.reference[count]);
        tilesDisplayed.transform.SetParent(transform);
        tilesDisplayed.gameObject.SetActive(false);
        
        return tilesDisplayed;
    }
    private void ModifyLists(ElementsTileSelectorMenu element, Vector2 positionElement, int count)
    {
        _allTilesShowedAndHidden.Add(element);
        if (count > 4) return;
        
        _positionElementShowed.Add(positionElement);
        _allTilesCurrentShowed.Add(element);
        element.gameObject.SetActive(true);
    }
    public async void AnimationDisappearMenu()
    {
        PlayerController.sharedInstance.StopControls();
        
        var listTasks = new Task[_allTilesCurrentShowed.Count];
        for (int i = 0; i < _allTilesCurrentShowed.Count; i++)
            listTasks[i] = _allTilesCurrentShowed[i].transform.MoveScaleYTest(_allTilesCurrentShowed[i].gameObject);

        await Task.WhenAll(listTasks);
        AllElementsMissed();
    }
    private void AllElementsMissed()
    {
        PlayerController.sharedInstance.ChangeControlToUI();
        CloseMenu();
        //ManagerMenusEditMapMode.sharedInstance.OpenMenuUnits();
    }

    public void Trigger(int count)
    {
        throw new NotImplementedException();
    }

    public void DisplayMenu()
    {
        throw new NotImplementedException();
    }

    public void CloseMenuByPressB()
    {
        throw new NotImplementedException();
    }

    public void CloseMenuByTriggerButton()
    {
        throw new NotImplementedException();
    }
}
