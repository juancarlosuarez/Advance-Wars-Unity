using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Extensibles;
using UnityEngine;

public class MenuOptionsUnitsSelector : MonoBehaviour, IMoveElement, MoveWithEnd
{// O SI NENA YA CASI PUEDES BORRAR ESTA MIERDA
    [SerializeField] private ListGameObjectsReference prefabsUnits;
    
    private List<ElementsUnitSelectorMenu> _allUnitsShowedAndHidden;
    private List<ElementsUnitSelectorMenu> _allUnitsCurrentShowed;
    private Dictionary<int, List<ElementsUnitSelectorMenu>> _elementsUnitsSplitInEachPlayer;  
        
    private List<Vector2> _positionElementShowed;
    [SerializeField] private ElementsUnitSelectorMenu prefab;

    public static event Action triggerBottonMenuUnitSelector;
    
    [SerializeField] private SOListUnitsPrefabs dictionaryUnits;
    private bool _isAlreadySpawnedMenu;
 
    private int _selectPositionList = 2;
    private int _finalPositionList = 4;
    private int _localPosition;
    private int _initialPosition = 0;
    
    public int InitialPositionList
    {
        get => _initialPosition;
        set
        {
            if (value <= GetCountListElement() && value >= 0) _initialPosition = value;
            if (value > GetCountListElement()) _initialPosition = 0;
            if (value < 0) _initialPosition = GetCountListElement();
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

    private int playerUnits = 1;
    [Header("Scriptable Objects")] 
    [SerializeField] private SOElementSelectedEditorMap elementSelected;
    public void HighLight(int count, int countUI)
    {
        
    }

    public void LowLight(int count, int countUI)
    {
        
    }

    public void Trigger()
    {
        elementSelected.SetValuesElementMenuSelectorUnit(_allUnitsShowedAndHidden[SelectPositionList]);
        triggerBottonMenuUnitSelector?.Invoke();
    }

    public void CloseMenu()
    {
        //UIManagerGamePlay.sharedInstance.OpenEditorFromStart();
        StateGamePlay.GetInstance().ChangeState(GameState.EditingMap);
        PlayerController.sharedInstance.ChangeControlToEditMap();

        gameObject.SetActive(false);
    }

    public void DisplayElements()
    {
        if (_isAlreadySpawnedMenu) return;

        //UIManagerGamePlay.sharedInstance.CloseEditorFromStart();
        
        StartMenuWithAllElements();
        
        DisplayElementsOfOnePlayer();
    }
    public async void ActiveAgainElements()
    {
        gameObject.SetActive(true);
        int count = 0;
        foreach (var c in _allUnitsCurrentShowed)
        {
            c.transform.localScale = Vector3.one;
            c.transform.position = transform.position;
            
            count++;
        }
        await MoveElements();

    }
    #region SpawnElements
    private void StartMenuWithAllElements()
    {
        ResetElements();
        
        var dictionary = dictionaryUnits.GetDictionaryOfUnits();
        
        for (int numberPlayer = 0; numberPlayer <= 4; numberPlayer++)
        {
            var listLocal = new List<ElementsUnitSelectorMenu>();
            
            for (int idUnit = 0; idUnit <= GetCountListElement(); idUnit++)
            {
                var listUnits = dictionary[(NameUnit)idUnit];
                var unit = listUnits[numberPlayer];

                var element = InstantiateElement(unit);
                listLocal.Add(element);    
                
                if (idUnit != GetCountListElement()) continue;
                
                _elementsUnitsSplitInEachPlayer.Add(numberPlayer, listLocal); //Anado la lista al final del bucle
                
                if (numberPlayer == 2) _allUnitsShowedAndHidden = _elementsUnitsSplitInEachPlayer[2];

            }
        }
    }

    private void ResetElements()
    {
        _isAlreadySpawnedMenu = true;
        _allUnitsCurrentShowed = new List<ElementsUnitSelectorMenu>();
        _allUnitsShowedAndHidden = new List<ElementsUnitSelectorMenu>();
        _positionElementShowed = new List<Vector2>();
        _elementsUnitsSplitInEachPlayer = new Dictionary<int, List<ElementsUnitSelectorMenu>>();
    }
    private ElementsUnitSelectorMenu InstantiateElement(AbstractBaseUnit unit)
    {
        var unitDisplayed = Instantiate(prefab, transform.position, Quaternion.identity);
        unitDisplayed.InitElementMenuUnit(unit);
        unitDisplayed.transform.SetParent(transform);
        unitDisplayed.gameObject.SetActive(false);

        return unitDisplayed;
    }
    private async void DisplayElementsOfOnePlayer()
    {
        Vector2 position = transform.position;
        Vector2 positionElement = new Vector2();
        float x = position.x;
        float y = position.y;
        float offset = 4f;
        
        for (int i = 0; i < GetCountListElement(); i++)
        {
            OffSetPosition(ref offset, i, ref positionElement, x, y);
            ShowElements(_allUnitsShowedAndHidden[i], positionElement, i);
        }

        await MoveElements();
    }
    public void MoveElementsInTheSelector(bool isMovementRight)
    {
        foreach (var unitCurrentShowed in _allUnitsCurrentShowed)
        {
            unitCurrentShowed.gameObject.SetActive(false);
        } 
        _allUnitsCurrentShowed.Clear();
        for (int i = 0; i <= 4; i++)
        {
            if (i == 0) LocalPosition = _initialPosition;
            
            var unitThatWillAdd = _allUnitsShowedAndHidden[_localPosition];
            
            _allUnitsCurrentShowed.Add(unitThatWillAdd);
            unitThatWillAdd.gameObject.SetActive(true);
            unitThatWillAdd.transform.localScale = Vector3.one;
            unitThatWillAdd.transform.position = _positionElementShowed[i];
            
            LocalPosition++;
        }
    }
    #endregion
    public async void AnimationDisappearMenu()
    {
        PlayerController.sharedInstance.StopControls();
        
        var listTasks = new Task[_allUnitsCurrentShowed.Count];
        for (int i = 0; i < _allUnitsCurrentShowed.Count; i++) 
            listTasks[i] = _allUnitsCurrentShowed[i].transform.MoveScaleYTest(_allUnitsCurrentShowed[i].gameObject);

        await Task.WhenAll(listTasks);
        
        AllElementsDissapeared();
    }
    public int GetCountListElement() => dictionaryUnits.GetCountIndividualList - 1;
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

    private void ShowElements(ElementsUnitSelectorMenu element, Vector2 positionElement, int count)
    {
        if (count <= 4)
        {
            _positionElementShowed.Add(positionElement);
            _allUnitsCurrentShowed.Add(element);
            element.gameObject.SetActive(true);
            
            //MoveElementCorrectPosition(element.gameObject, positionElement);
            
            return;
        }
        element.gameObject.SetActive(false);
    }

    private async Task MoveElements()
    {
        PlayerController.sharedInstance.StopControls();
        var ctc = new CancellationTokenSource();
        
        var listTasks = new Task[_allUnitsCurrentShowed.Count];

        for (int i = 0; i < _allUnitsCurrentShowed.Count; i++)
        {
            listTasks[i] = _allUnitsCurrentShowed[i].transform
                .MoveElement(_positionElementShowed[i], ctc);
        }

        await Task.WhenAll(listTasks);
        FinishMove();
    }
    public void FinishMove()
    {
        PlayerController.sharedInstance.ChangeControlToUI();
    }

    public void AllElementsDissapeared()
    {
        PlayerController.sharedInstance.ChangeControlToUI();
        CloseMenu();
        //ManagerMenusEditMapMode.sharedInstance.OpenMenuTiles();
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
