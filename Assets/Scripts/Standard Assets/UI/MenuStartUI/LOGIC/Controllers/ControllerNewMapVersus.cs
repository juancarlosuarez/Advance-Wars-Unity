using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ControllerNewMapVersus : ControllerUIMenuStatic
{
    [SerializeField] private List<Vector2> positionArrows;
    [SerializeField] private Vector2 _positionMenu;

    private string pathJSON;
    private TextMeshProUGUI textContinue;
    private bool _firstRound;
    private GameObject _arrow;
    public override void HighLight(int count, int countUI)
    {
        if (!_firstRound) SoundManager._sharedInstance.PlayEffectSound(EffectNames.MoveBetweenMenus);
        else _firstRound = false;

        _arrow.transform.position = positionArrows[count];
    }
    public override void LowLight(int count, int countUI)
    {
        
    }

    public override void Trigger(int count)
    {
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.SelectOption);
        if (count == 0) NewGame();
        if (count == 1) ContinueGame();
    }

    public override void DisplayMenu()
    {
        _firstRound = true;
        FactoryPrefabricMenusUI.GetInstance().ActiveMenuUI(controllerMenu, _positionMenu);
        FindArrow();
        FindText();
    }

    private void FindArrow()
    {
        _arrow = WorldObjectsCanvas.GetInstance().smallArrowHighLight;
        _arrow.SetActive(true);
    }

    private void FindText()
    {
        pathJSON = Application.dataPath + Path.AltDirectorySeparatorChar +
                   "JSON/MapsLoad/DesignMap/DataUnit/SaveDataUnit" + 4 + ".json";
        
        textContinue = GameObject.Find("Continue/Text").GetComponent<TextMeshProUGUI>();
        if (GetCountList() == 1) ChangeColorContinueTextToGray();
        else ChangeColorContinueTextToRed();
    }
    public override void CloseMenuByTriggerButton()
    {
        FactoryPrefabricMenusUI.GetInstance().DisableCurrentMenu();
        _arrow.SetActive(false);
    }
    public override int GetCountList()
    {
        var id = File.Exists(pathJSON) ? 2 : 1;
        return id;
    }
    private void ChangeColorContinueTextToRed()
    {
        textContinue.color = Color.red;
    }

    private void ChangeColorContinueTextToGray()
    {
        var colorGray = Color.gray;
        textContinue.color = colorGray;
    }
    private void ContinueGame()
    {
        CommandQueue.GetInstance.AddCommand(new CommandSelectMapID(new DataStartMap(4, true)), false);
        CommandQueue.GetInstance.AddCommand(new CommandChangeScene("Gameplay"), false);
    }

    private void NewGame()
    {
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.SelectMap, 0, true);
    }

    public override bool CanSelectOption(int count)
    {
        return true;
    }

    public override void CloseMenuByPressB()
    {
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.Exit);
        FactoryPrefabricMenusUI.GetInstance().DisableCurrentMenu();
        _arrow.SetActive(false);
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.SelectModeGame, 0, true);
    }
}
