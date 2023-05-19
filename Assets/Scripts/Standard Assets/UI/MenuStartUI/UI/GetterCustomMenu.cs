using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetterCustomMenu
{
    private ManagerBuilderElementsMenus _managerBuilders;

    private GameObject _finalBuild;
    private GameObject _arrowHighLight;
    private List<GameObject> _listElementsInsideFrameWork;
    private Vector2 _sizeReferenceElement;
    private int countElements;
    private ControllerMenuName _nameNewMenu;
    
    public GameObject MakeBuild(ControllerMenuName nameNewMenu)
    {
        _nameNewMenu = nameNewMenu;
        
        GetParentsMenu();
        PutAllReferences();
        return FinalBuild();
    }
    public GetterCustomMenu()
    {
        _managerBuilders = new ManagerBuilderElementsMenus();
    }
    private GameObject FinalBuild()
    {
        _finalBuild.SetActive(false);
        return _finalBuild;
    }
    private void GetParentsMenu()
    {
        GameObject canvas = GameObject.Find("Canvas");
        _finalBuild = new GameObject(_nameNewMenu.ToString());
        GameObject componentsMenuMain = new GameObject("Components");
        
        componentsMenuMain.SetParent(_finalBuild);
        _finalBuild.SetParent(canvas);
    }

    private void PutAllReferences()
    {
        IBuilderMenuStart builderElementsStart = _managerBuilders.GetBuildersElementsPreFabric(_nameNewMenu);
        _listElementsInsideFrameWork = builderElementsStart.Build(false);
        _sizeReferenceElement = builderElementsStart.SizeReference;
        _arrowHighLight = builderElementsStart.GetArrowHighLight();
        countElements = builderElementsStart.GetCount;
    }

    public List<GameObject> GetListElements() => _listElementsInsideFrameWork;
    public GameObject GetArrowHighLight() => _arrowHighLight;
    



}
