using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllerMainMenu : ControllerUIMenuStatic
{
    [SerializeField] private List<Vector2> positionArrows;
    public static Action BackControllerSelectMap;
    
    private readonly int[] _keysOptionId = {20, 21, 23};
    private Vector2 _positionMenu;
    private bool _firstRound;

    private GameObject _arrow;

    //public static event Action CloseMenuStartEditMapByB;
    public override bool CanSelectOption(int count)
    {
        return true;
    }

    public override void CloseMenuByTriggerButton()
    {
        _arrow.SetActive(false);
    }
    public override void CloseMenuByPressB()
    {
        BackControllerSelectMap?.Invoke();
        FactoryPrefabricMenusUI.GetInstance().DisableCurrentMenu();
        _arrow.SetActive(false);
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.Exit);
    }

    public override int GetCountList() => 3;
    public override void HighLight(int count, int countUI)
    {
        if (!_firstRound) SoundManager._sharedInstance.PlayEffectSound(EffectNames.MoveBetweenMenus);
        else _firstRound = false;
        
        _arrow.transform.position = positionArrows[count];
    }

    public override void LowLight(int count, int countUI)
    {
    }

    public override void DisplayMenu()
    {
        _firstRound = true;
        FactoryPrefabricMenusUI.GetInstance().ActiveMenuUI(controllerMenu, Vector2.zero);
        FindArrow();
    }

    private void FindArrow()
    {
        _arrow = WorldObjectsCanvas.GetInstance().bigArrowHighLight;
        _arrow.SetActive(true);
    }
    public override void Trigger(int count)
    {
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.SelectOption);
        
        OptionSelect(_keysOptionId[count]).Trigger();
        
    }
}
