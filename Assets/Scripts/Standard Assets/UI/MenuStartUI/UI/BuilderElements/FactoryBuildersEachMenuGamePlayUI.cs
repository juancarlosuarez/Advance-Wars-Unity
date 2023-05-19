using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

//Ok, this system need one controller asign with the enum NameMenuStart, after one OptionStartData, with all the data.
public enum NameElementWithFramesGeneric
{
    Null, EditMenu, FileMenu, FillMenu, HelpMenu1, SaveMenu, LoadMenu, OptionUnits, BarrackMenu, StartMenuGamePlay,
    SelectMap, DisplayBuilds, OptionStartGamePlay, OptionStartEdit
}
public class FactoryBuildersEachMenuGamePlayUI : MonoBehaviour
{
    private Dictionary<NameElementWithFramesGeneric, DataUIMenuStart> _allMenuStartGameObjects;
    private Dictionary<ControllerMenuName, DataUIMenuStart> _allCustomMenus;

    [SerializeField] private DataPermanentForMenus dataPermanentMenus;
    public static FactoryBuildersEachMenuGamePlayUI sharedInstance;
    [SerializeField] private SOGameObject smallArrowHighLight;
    [SerializeField] private SOGameObject regularArrowHighLight;
    private MenuStartBuildWithFrame _builderEachMenu;

    [SerializeField] private List<ControllerMenuName> _customMenus;

    private void Start()
    {
        sharedInstance = this;
        MakeArrowHighLight();
        ImplementValuesForDictionaryGenericMenu();
        ImplementElementValuesDictionaryCustom();
    }

    private void MakeArrowHighLight()
    {
        var builderIcons = new BuildElementsMenuStart();
        GameObject canvas = GameObject.Find("Canvas");
        
        smallArrowHighLight.reference =
            builderIcons.BuildArrowHighLight(dataPermanentMenus.sizeIcon / 2, dataPermanentMenus.spriteArrow, "SmallArrow");
        smallArrowHighLight.reference.SetParent(canvas);
        smallArrowHighLight.reference.SetActive(false);
        WorldObjectsCanvas.GetInstance().smallArrowHighLight = smallArrowHighLight.reference;
        
        regularArrowHighLight.reference =
            builderIcons.BuildArrowHighLight(dataPermanentMenus.sizeIcon, dataPermanentMenus.spriteArrow, "BigArrow");
        regularArrowHighLight.reference.SetParent(canvas);
        regularArrowHighLight.reference.SetActive(false);
        WorldObjectsCanvas.GetInstance().bigArrowHighLight = regularArrowHighLight.reference;
    }
    private void ImplementValuesForDictionaryGenericMenu()
    {
        //If i wanna to add some new i need to access to ManagerBuilderElementStart
        _builderEachMenu = new MenuStartBuildWithFrame();
        _allMenuStartGameObjects = new Dictionary<NameElementWithFramesGeneric, DataUIMenuStart>();

        var valuesElement = System.Enum.GetValues(typeof(NameElementWithFramesGeneric));
        
        foreach (NameElementWithFramesGeneric c in valuesElement)
        {
            var valueDictionary = new DataUIMenuStart();
            valueDictionary.referenceToMenu = _builderEachMenu.MakeBuild(c, false); 
            valueDictionary.elementsMenu = _builderEachMenu.GetListElements();
            valueDictionary.arrowHighLight = _builderEachMenu.GetArrowHighLight();
            valueDictionary.arrowPositions = _builderEachMenu.GetArrowPositions();
            valueDictionary.frameworkMenu = _builderEachMenu.GetFrameworkPieces();
           
           var keyDictionary = c;
           _allMenuStartGameObjects.Add(keyDictionary, valueDictionary);
        }
    }
    private void ImplementElementValuesDictionaryCustom()
    {
        //If i wanna to add some new i need to access to ManagerBuilderElementStart
        var _builderEachMenu = new GetterCustomMenu();
        _allCustomMenus = new Dictionary<ControllerMenuName, DataUIMenuStart>();
        
        
        foreach (ControllerMenuName c in _customMenus)
        {
            var valueDictionary = new DataUIMenuStart();
            valueDictionary.referenceToMenu = _builderEachMenu.MakeBuild(c); 
            valueDictionary.elementsMenu = _builderEachMenu.GetListElements();
            valueDictionary.arrowHighLight = _builderEachMenu.GetArrowHighLight();
           
            var keyDictionary = c;
            _allCustomMenus.Add(keyDictionary, valueDictionary);
        }
    }
    public void UpdateMenu(NameElementWithFramesGeneric controllerMenuNameMenu)
    {
        var menuOverWrite = _allMenuStartGameObjects[controllerMenuNameMenu];

        menuOverWrite.referenceToMenu = _builderEachMenu.OverWriteMenu(controllerMenuNameMenu, menuOverWrite);
        
        menuOverWrite.elementsMenu = _builderEachMenu.GetListElements();
        menuOverWrite.arrowHighLight = _builderEachMenu.GetArrowHighLight();
        menuOverWrite.arrowPositions = _builderEachMenu.GetArrowPositions();
        menuOverWrite.frameworkMenu = _builderEachMenu.GetFrameworkPieces();
        
    }
    private void DestroyMenu(DataUIMenuStart dataMenuThatIWannaDestroy)
    {
        Destroy(dataMenuThatIWannaDestroy.referenceToMenu);
    }

    public DataUIMenuStart GetDataMenu(NameElementWithFramesGeneric controllerMenuNameMenu) => _allMenuStartGameObjects[controllerMenuNameMenu];
    
    public void ActiveUIMenu(NameElementWithFramesGeneric controllerMenuNameMenu, Vector2 position)
    {
        DisableMenusControllers();
        var menuUIData = _allMenuStartGameObjects[controllerMenuNameMenu];
        menuUIData.referenceToMenu.SetActive(true);
        menuUIData.referenceToMenu.transform.position = position;
    }

    public void JustActiveUIMenu(NameElementWithFramesGeneric controllerMenuNameMenu, Vector2 position)
    {
        var menuUIData = _allMenuStartGameObjects[controllerMenuNameMenu];
        menuUIData.referenceToMenu.SetActive(true);
        menuUIData.referenceToMenu.transform.position = position;
    }
    public void DisableMenusControllers()
    {
        foreach (var c in _allMenuStartGameObjects.Values) c.referenceToMenu.SetActive(false);
    }
}
    public class DataUIMenuStart
    {
        public GameObject referenceToMenu;
        public List<GameObject> elementsMenu;
        public GameObject arrowHighLight;
        public List<Vector2> arrowPositions;
        public List<Image> frameworkMenu;
    }
