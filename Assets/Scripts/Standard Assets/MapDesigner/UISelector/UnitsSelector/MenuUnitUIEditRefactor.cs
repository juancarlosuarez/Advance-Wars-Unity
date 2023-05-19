using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Object = System.Object;

public class MenuUnitUIEditRefactor : ScriptableObject
{
    //[SerializeField] private ListGameObjectsReference prefabsUnits;
    
    private List<ElementsUnitSelectorMenu> _allUnitsShowedAndHidden;
    private List<ElementsUnitSelectorMenu> _allUnitsCurrentShowed;
    private Dictionary<int, List<ElementsUnitSelectorMenu>> _elementsUnitsSplitInEachPlayer;  
        
    private List<Vector2> _positionElementShowed;
    [SerializeField] private ElementsUnitSelectorMenu prefab;
    
    [SerializeField] private SOListUnitsPrefabs dictionaryUnits;

    private CancellationTokenSource _ctc;
    private readonly Transform _transformCentral;
    public bool _isAlreadySpawnedMenu;

    private void Start()
    {
        _allUnitsShowedAndHidden = new List<ElementsUnitSelectorMenu>();
        
    }
    public MenuUnitUIEditRefactor(Transform transformCentral)
    {
        PlayerController.ResetEventsNoMonobehavior += OnApplicationQuit;
        _transformCentral = transformCentral;
    }

    public void DisplayElements()
    {
        if (_isAlreadySpawnedMenu) return;

        StartMenuWithAllElements();
        DisplayElementsOfOnePlayer();
    }

    public async void ActiveAgainElements()
    {
        //gameObject.SetActive(true);
        _ctc = new CancellationTokenSource();
        int count = 0;
        foreach (var c in _allUnitsCurrentShowed)
        {
            c.transform.localScale = Vector3.one;
            c.transform.position = _transformCentral.position;
            
            count++;
        }
        await MoveElements(_ctc);
    }
    public void MoveElementsInTheSelector(int localPosition, int initialPosition)
    {
        foreach (var unitCurrentShowed in _allUnitsCurrentShowed)
        {
            unitCurrentShowed.gameObject.SetActive(false);
        } 
        _allUnitsCurrentShowed.Clear();
        for (int i = 0; i <= 4; i++)
        {
            if (i == 0) localPosition = initialPosition;
            
            var unitThatWillAdd = _allUnitsShowedAndHidden[localPosition];
            
            _allUnitsCurrentShowed.Add(unitThatWillAdd);
            unitThatWillAdd.gameObject.SetActive(true);
            unitThatWillAdd.transform.localScale = Vector3.one;
            unitThatWillAdd.transform.position = _positionElementShowed[i];
            
            localPosition++;
        }
    }
    private void StartMenuWithAllElements()
    {
        for (byte numberPlayer = 0; numberPlayer <= 4; numberPlayer++)
        {
            MakeElementsForEachPlayer(numberPlayer);
        } 
    }
    private void MakeElementsForEachPlayer(byte numberPlayer)
    {
        var listLocal = new List<ElementsUnitSelectorMenu>();
        var dictionary = dictionaryUnits.GetDictionaryOfUnits();    
        
        for (int idUnit = 0; idUnit < dictionaryUnits.GetCountIndividualList; idUnit++)
        {
            var listUnits = dictionary[(NameUnit)idUnit];
            var unit = listUnits[numberPlayer];

            var element = InstantiateElement(unit);
            listLocal.Add(element);    
                
            if (idUnit != dictionaryUnits.GetCountIndividualList - 1) continue;
                
            _elementsUnitsSplitInEachPlayer.Add(numberPlayer, listLocal); //Anado la lista al final del bucle
                
            if (numberPlayer == 2) _allUnitsShowedAndHidden = _elementsUnitsSplitInEachPlayer[2];

        }
    }
    private ElementsUnitSelectorMenu InstantiateElement(AbstractBaseUnit unit)
    {
        var unitDisplayed = UnityEngine.Object.Instantiate(prefab, _transformCentral.position, Quaternion.identity);
        unitDisplayed.InitElementMenuUnit(unit);
        unitDisplayed.transform.SetParent(_transformCentral);
        unitDisplayed.gameObject.SetActive(false);

        return unitDisplayed;
    }
    private async void DisplayElementsOfOnePlayer()
    {
        Vector2 position = _transformCentral.position;  
        Vector2 positionElement = new Vector2();
        float x = position.x;
        float y = position.y;
        float offset = 4f;
        _ctc = new CancellationTokenSource();
        
        for (int i = 0; i < dictionaryUnits.GetCountIndividualList; i++)
        {
            OffSetPosition(ref offset, i, ref positionElement, x, y);
            ShowElements(_allUnitsShowedAndHidden[i], positionElement, i);
        }
        await MoveElements(_ctc);
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
            positionElement = _transformCentral.position;
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
    private async Task MoveElements(CancellationTokenSource tokenCancel)
    {
        PlayerController.sharedInstance.StopControls();

        var listTasks = new Task[_allUnitsCurrentShowed.Count];

        for (int i = 0; i < _allUnitsCurrentShowed.Count; i++)
        {
            listTasks[i] = _allUnitsCurrentShowed[i].transform
                .MoveElement(_positionElementShowed[i], tokenCancel);
        }
        await Task.WhenAll(listTasks);
        PlayerController.sharedInstance.ChangeControlToUI();
    }

    private void OnApplicationQuit()
    {
        PlayerController.ResetEventsNoMonobehavior -= OnApplicationQuit;
        _ctc.Cancel();
    }
}
