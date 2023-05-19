using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminStartMenuUI : MonoBehaviour
{
    //creo que deberia borrarlo.
    public static AdminStartMenuUI sharedInstance;

    private DataPermanentForMenus _currentDataPermanentForMenus;
    private GameObject menuStart;
    private List<GameObject> allCurrentsElements;
    private GameObject arrowHighLight;
    
    private AdminStartMenuUI() { }
    private void Awake()
    {
        sharedInstance = this;
    }
    private void OnDisable()
    {
        UIManagerGamePlay.CloseMenuStartEditUI -= DesactiveMenu;
    }
    private void OnEnable()
    {
        UIManagerGamePlay.CloseMenuStartEditUI += DesactiveMenu;        
    }
    public List<GameObject> GetElementsMenu() => allCurrentsElements;
    public GameObject GetArrowHighLight() => arrowHighLight;
    public void ActiveMenu(DataPermanentForMenus newDataPermanentForMenus, Vector2 positionMenu)
    {
        menuStart = GetMenuStartUI(newDataPermanentForMenus);
        menuStart.SetActive(true);
        menuStart.transform.position = positionMenu;

    }
    public void DesactiveMenu() 
    {
        if (!menuStart) return;

        PlayerController.sharedInstance.ChangeControlToEditMap();
        menuStart.SetActive(false);
    }

    private GameObject GetMenuStartUI(DataPermanentForMenus newDataPermanentForMenus)
    {
        if (IsNeededToMakeOneNewMenu(newDataPermanentForMenus)) return BuildMenuStart(newDataPermanentForMenus);
        else return menuStart;
    }
    private bool IsNeededToMakeOneNewMenu(DataPermanentForMenus newDataPermanentForMenus)
    {
        if (_currentDataPermanentForMenus == null || _currentDataPermanentForMenus != newDataPermanentForMenus) return true;
        if (_currentDataPermanentForMenus == newDataPermanentForMenus) return false;
        return false;
    }
    private GameObject BuildMenuStart(DataPermanentForMenus newDataPermanentForMenus)
    {
        _currentDataPermanentForMenus = newDataPermanentForMenus;
        
        //var startBuild = new MenuStartBuild(newData);
        //menuStart = startBuild.MakeBuild();
        //allCurrentsElements = startBuild.GetListElements();
        //arrowHighLight = startBuild.GetArrowHighLight();

        return menuStart;
    }
}
