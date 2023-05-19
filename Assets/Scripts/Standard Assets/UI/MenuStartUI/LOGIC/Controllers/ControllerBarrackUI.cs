using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllerBarrackUI : ControllerUIMenuStatic
{
    //This manager will give me all the information like the gold cost and the enum, that will be used for spawn units.
    //The key should be update in the moment that the selector detect a military build and take his id.
    [SerializeField] private ManagerOptionsBarrackMenu _managerOptionBarrackMenu;
    [SerializeField] private ManagerHighLightBarrack _managerHighLightBarrack;
    [SerializeField] private IntReference _keyForBarrackMenuData;
    
    //Necesito un manager que me gestione quien es el jugador correspondiente y que me devuelva dicha cantidad, vamos una base de datos con una key y un data.
    private int _goldCurrentPlayer;

    //We need to access to the data to the current menu, because we need to get the list with the elements.
    private DataUIMenuStart _currentDataUI;
    private OptionBarrackMenuUI _optionBarrackMenuUI;
    
    private Vector2 _positionMenu;
    private List<Vector2> _positionArrowHighLight;
    private List<Vector3> _positionElements;

    private int previousData;
    private bool _firstRound;

    public static event Action<Vector2> EventDisplayDataUnits;
    public static event Action DisableDataUnits;
    public static event Action<Sprite> UpdateDataUnits;
    public override void DisplayMenu()
    {
        _firstRound = true;
        _optionBarrackMenuUI = _managerOptionBarrackMenu.GetOptionBarrackUnit((byte)_keyForBarrackMenuData.reference);
        CalculatePositionArrow();
        FactoryBuildersEachMenuGamePlayUI.sharedInstance.ActiveUIMenu(NameElementWithFramesGeneric.BarrackMenu, _positionMenu);
        GetCurrentAmountGold();
        PositionElements();
        DisplayInitialBarrack();
        DisplayDataUnitHighLight();
    }

    private void GetCurrentAmountGold()
    {
        var currentPlayer = WorldScriptableObjects.GetInstance().currentPLayer.reference;
        var currentStatsPlayer = WorldScriptableObjects.GetInstance().statsPlayersManager.GetStatsPlayer(currentPlayer);

        _goldCurrentPlayer = currentStatsPlayer.GoldAmount();
    }
    private void PositionElements()
    {
        _positionElements = new List<Vector3>();

        var iteratorNumber = 0;
        if (_currentDataUI.elementsMenu.Count <= 7) iteratorNumber = _currentDataUI.elementsMenu.Count;
        else iteratorNumber = 7;
        
            for (int i = 0; i < iteratorNumber; i++)
            {
                var currentElement = _currentDataUI.elementsMenu[i];
                var positionElement = currentElement.gameObject.transform.position;
                _positionElements.Add(positionElement);
            }
    }

    private void DisplayInitialBarrack()
    {
        if (_currentDataUI.elementsMenu.Count < 6) return;
        foreach (var c in _currentDataUI.elementsMenu)
        {
            
            c.SetActive(false);
        }
        int countRound = 0;
        for (int i = 0; i < 7; i++)
        {
            var currentElement = _currentDataUI.elementsMenu[i];
            currentElement.SetActive(true);
            
            currentElement.transform.position = _positionElements[countRound];
            countRound++;
        }
    }

    private void DisplayDataUnitHighLight()
    {
        var positionX = _positionMenu.x + 8;
        var positionY = _positionMenu.y;
        var positionDisplay = new Vector2(positionX, positionY);
        EventDisplayDataUnits?.Invoke(positionDisplay);
        
    }
    public override void CloseMenuByPressB()
    {
        _currentDataUI.arrowHighLight.SetActive(false);
        PlayerController.sharedInstance.ChangeControlToGamePlay();
        FactoryControllersMenuStartUI.sharedInstance.CloseCurrentMenu();
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.Exit);
        DisableDataUnits?.Invoke();
    }

    public override void CloseMenuByTriggerButton()
    {
        _currentDataUI.arrowHighLight.SetActive(false);
        PlayerController.sharedInstance.ChangeControlToGamePlay();
        FactoryControllersMenuStartUI.sharedInstance.CloseCurrentMenu();
        _currentDataUI.referenceToMenu.SetActive(false);
        DisableDataUnits?.Invoke();
    }

    public override void HighLight(int count, int countUI)
    {
        if (!_firstRound) SoundManager._sharedInstance.PlayEffectSound(EffectNames.MoveBetweenMenus);
        else _firstRound = false;

        int offset = 0;
        if (countUI >= 7) offset = 6;
        else offset = countUI;
        _currentDataUI.arrowHighLight.transform.position = _positionArrowHighLight[offset];
        bool isValueIncreasing = count > previousData;
        
        DisplayHighlightBarrackMenu(count);
        if (count > 6 || (!isValueIncreasing && count == 6))
        {
            foreach (var c in _currentDataUI.elementsMenu)
            {
                c.SetActive(false);
            }
            int countRound = 0;
            for (int i = count - 6; i <= count; i++)
            {
                var currentElement = _currentDataUI.elementsMenu[i];
                currentElement.SetActive(true);
                
                currentElement.transform.position = _positionElements[countRound];
                countRound++;
            }
        }

        previousData = count;
    }

    private void DisplayHighlightBarrackMenu(int count)
    {
        var currentPlayer = WorldScriptableObjects.GetInstance().currentPLayer.reference;
        var currentUnit = _managerOptionBarrackMenu.GetOptionBarrackUnit((byte)_keyForBarrackMenuData.reference).GetData()[count].nameUnit;
        var spriteSelected = _managerHighLightBarrack.GetSprite(currentUnit, currentPlayer);
        
        UpdateDataUnits?.Invoke(spriteSelected);
        //_displayDataUnits.gameObject.transform.position = Vector3.zero;
    }
    public override void LowLight(int count, int countUI)
    {
        
    }

    public override void Trigger(int count)
    {
        SpawnUnit(count);
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.SelectOption);
    }
    private void SpawnUnit(int count)
    {
        var elementSelected = _optionBarrackMenuUI.GetData()[count];
        var tileSelected = WorldScriptableObjects.GetInstance().tileSelected.reference;
        
        var newDataPrefab = new UnitDataPrefab();
        newDataPrefab.factionUnit = elementSelected.faction;
        newDataPrefab.nameUnit = elementSelected.nameUnit;
        
        var spawnerUnits = new SpawnerUnitRefactor(100);
        spawnerUnits.PutElement(newDataPrefab, tileSelected);
        
        CommandQueue.GetInstance.AddCommand(new CommandBuyUnit(elementSelected.faction, elementSelected.goldCost), false);
    }
    private void CalculatePositionArrow()
    {
        var camera = GameObject.Find("Main Camera");
        var positionCamera = camera.transform.position;
        _positionMenu = new Vector2(positionCamera.x - 3f, positionCamera.y);
        _currentDataUI.arrowHighLight.SetActive(true);
    
        _positionArrowHighLight = _currentDataUI.arrowPositions.Select(pos => 
            new Vector2(pos.x + _positionMenu.x, pos.y + _positionMenu.y)).ToList();
    }
    public override int GetCountList()
    {
        FactoryBuildersEachMenuGamePlayUI.sharedInstance.UpdateMenu(NameElementWithFramesGeneric.BarrackMenu);
        _currentDataUI = FactoryBuildersEachMenuGamePlayUI.sharedInstance.GetDataMenu(NameElementWithFramesGeneric.BarrackMenu);
        return _currentDataUI.elementsMenu.Count - 1;
    }

    public override bool CanSelectOption(int count)
    {
        var elementSelect = _optionBarrackMenuUI.GetData()[count];
        return elementSelect.goldCost <= _goldCurrentPlayer;
    }
}
