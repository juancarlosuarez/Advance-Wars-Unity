using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBuilderElementsMenus
{
    private BuilderBarrackUnitsMenu _buildBarrackMenu;
    private BuilderDisplayBuilds _builderDisplayBuilds;
    public ManagerBuilderElementsMenus()
    {
        _buildBarrackMenu = new BuilderBarrackUnitsMenu();
        _builderDisplayBuilds = new BuilderDisplayBuilds();
    }
    public IBuilderMenuStart GetBuilderGenerics(NameElementWithFramesGeneric newMenu)
    {
        switch (newMenu)
        {
            case NameElementWithFramesGeneric.EditMenu: return new BuilderElementsStartEditMap();
            case NameElementWithFramesGeneric.FileMenu: return new BuilderFileElements();
            case NameElementWithFramesGeneric.FillMenu: return new BuilderElementsFillOptionStart();
            case NameElementWithFramesGeneric.HelpMenu1: return new BuilderElementsHelpOption();
            case NameElementWithFramesGeneric.SaveMenu: return new BuilderSaveElementStartUI();
            case NameElementWithFramesGeneric.LoadMenu: return new BuilderSaveElementStartUI();
            case NameElementWithFramesGeneric.OptionUnits: return new BuilderElementsForActionUnit();
            case NameElementWithFramesGeneric.BarrackMenu: return _buildBarrackMenu;
            case NameElementWithFramesGeneric.StartMenuGamePlay: return new BuilderElementsMenuStartGameplay();
            case NameElementWithFramesGeneric.SelectMap: return new BuilderMapSelect();
            case NameElementWithFramesGeneric.DisplayBuilds: return _builderDisplayBuilds;
            case NameElementWithFramesGeneric.OptionStartGamePlay: return new BuilderOptionsMenuStart();
            case NameElementWithFramesGeneric.OptionStartEdit: return new BuilderOptionMenuEdit();
            default: return new BuilderElementsStartEditMap();
        }
    }

    public IBuilderMenuStart GetBuildersElementsPreFabric(ControllerMenuName newMenu)
    {
        switch (newMenu)
        {
            case ControllerMenuName.SelectModeGame: return new BuilderElementsMenuSelectorMode();
        }

        return null;
    }
}

public interface IBuilderMenuStart
{
    public List<GameObject> Build(bool isDinamicThisMenu);
    public Vector2 SizeReference { get; set; }
    public GameObject GetArrowHighLight();
    public int GetCount { get; set;}
}
