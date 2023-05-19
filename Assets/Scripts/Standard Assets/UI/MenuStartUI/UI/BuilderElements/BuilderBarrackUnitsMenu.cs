using System.Collections;
using System.Collections.Generic;
using Unity.XR.GoogleVr;
using UnityEngine;

public class BuilderBarrackUnitsMenu : IBuilderMenuStart
{
    //This data is assigned in Resources and manage about which dataUI need to use, because this builder is used
    //for like 12 different cases.
    private OptionBarrackMenuUI _data;

    //This variable keep the builder of the elements like icons or texts.
    private readonly ManagerOptionsBarrackMenu _managerOptionData;
    private readonly SOGameObject _regularArrowHighLight;
    
    private BuildElementsMenuStart _elementBuilder;

    private List<List<GameObject>> _allVariantsMenu;
    private List<List<ElementUIWithTwoTextAndIcon>> _allUIDataVariantsMenu;
    private List<List<int>> _allGoldCost;

    private List<GameObject> currentElementsACtivate;
    private IntReference _keyForBarrackMenuData;
    public BuilderBarrackUnitsMenu()
    {
        _managerOptionData =
            Resources.Load<ManagerOptionsBarrackMenu>(
                "ScriptableObject/UI/OptionsStart/BarrackData/ManagerOptionsBarrackData");
        _regularArrowHighLight =
            Resources.Load<SOGameObject>("ScriptableObject/Data/GameObjectData/SmallHighLightArrowUI");
    }
    public List<GameObject> Build(bool isMenuTryingToBeUpdate)
    {
        if (isMenuTryingToBeUpdate) return UpdateMenu();
        //This part of code just will be launch in the Awake of the Menu's Factory
        //We take a new reference of our builder
        _elementBuilder = new BuildElementsMenuStart();
        
        //This list will keep and used when the controller request an update.
        _allVariantsMenu = new List<List<GameObject>>();
        _allUIDataVariantsMenu = new List<List<ElementUIWithTwoTextAndIcon>>();
        _allGoldCost = new List<List<int>>();
        //This list will be returner, and we will put all the elements without exceptions here inside, because
        //the menu is programming in the way that just will keep correctly in the canvas the list of elements that
        //we will return.
        List<GameObject> allElementsWithOutOffset = new List<GameObject>();
        //This size is generic for all the elements with this characteristics.
        currentElementsACtivate = new List<GameObject>();
        Vector2 sizeIcon = new Vector2(1.75f / 2, 1.53f / 2);

        for (byte i = 0; i < _managerOptionData.CountDictionary(); i++)
        {
            //We got the correct data, the key for the dictionary is assigned in the moment the barrack is selected.
            _data = _managerOptionData.GetOptionBarrackUnit(i);
            //Two list, one contain the data and the another the object.
            var elementDataMenuBarrack = new List<ElementUIWithTwoTextAndIcon>();
            //This two lists are needed for the UpdateMenu
            var currentElements = new List<GameObject>();
            var goldCosts = new List<int>();
            foreach (var c in _data.GetData())
            {
                //The element builded
                var elementData = _elementBuilder.GetElementWithTwoTextAndIcon(c.nameUnit.ToString(), c.goldCost.ToString(),
                    c.spriteSoldier, sizeIcon, false);
                //We keep our references
                elementDataMenuBarrack.Add(elementData);
                currentElements.Add(elementData.parent);
                allElementsWithOutOffset.Add(elementData.parent);
                goldCosts.Add(c.goldCost);
                //Some UI adjusts
                elementData.parent.gameObject.SetActive(false);
            }
            _allVariantsMenu.Add(currentElements);
            _allUIDataVariantsMenu.Add(elementDataMenuBarrack);
            _allGoldCost.Add(goldCosts);
            var resizer = new ResizerElementsBarrack();
            resizer.RelocateElements(elementDataMenuBarrack);
            SizeReference = elementDataMenuBarrack[0].coverImage.rectTransform.sizeDelta;
        }
        //Now we make each element individually and assigned inside his list.
        //We create the resizer for our case
        return allElementsWithOutOffset;
    }

    private List<GameObject> UpdateMenu()
    {
        //In this first part we collected all the dada needed to make this shit work, the first variable is critical,
        //because we will need it for access to all the lists with the information, like the gold, UI data and the
        //raw gameobject.
        var key = WorldScriptableObjects.GetInstance().keyForBarrackMenuData.reference;
        
        var listElementSelected = _allVariantsMenu[key];
        var listUIDataElementSelected = _allUIDataVariantsMenu[key];
        var listGoldSelected = _allGoldCost[key];
        
        var currentPlayer = WorldScriptableObjects.GetInstance().currentPLayer.reference;
        var currentGold = WorldScriptableObjects.GetInstance().statsPlayersManager.GetStatsPlayer(currentPlayer).GoldAmount();

        if (currentElementsACtivate.Count != 0)
        {
            foreach (var c in currentElementsACtivate) c.SetActive(false);
            currentElementsACtivate = new List<GameObject>();
        }

        var counting = 0;
        foreach (var c in listElementSelected)
        {
            c.SetActive(true);
            currentElementsACtivate.Add(c);
            
            if (listGoldSelected[counting] > currentGold) ChangeTransparency(listUIDataElementSelected[counting], true);
            else ChangeTransparency(listUIDataElementSelected[counting], false);
            
            counting++;
        }
        // for (int i = 0; i < count; i++)
        // {
        //     listElementSelected[i].SetActive(true);
        //     currentElementsACtivate.Add(listElementSelected[i]);
        //
        //     if (listGoldSelected[i] > currentGold) ChangeTransparency(listUIDataElementSelected[i], true);
        //     else ChangeTransparency(listUIDataElementSelected[i], false);
        // }
        return listElementSelected;
    }
    private void ChangeTransparency(ElementUIWithTwoTextAndIcon element, bool willBeTransparent)
    {

        float valueTransparency = 0f;
        if (willBeTransparent) valueTransparency = .5f;
        else valueTransparency = 1;
        //For change the color of the elements, we need to put a local variable and assign directly.
        Color transparentColorSprite = element.iconImage.color;
        Color transparentColorText1 = element.textElement1.color;
        Color transparentColorText2 = element.textElement2.color;

        transparentColorSprite.a = valueTransparency;
        transparentColorText1.a = valueTransparency;
        transparentColorText2.a = valueTransparency;

        element.iconImage.color = transparentColorSprite;
        element.textElement1.color = transparentColorText1;
        element.textElement2.color = transparentColorText2;
    }
    public Vector2 SizeReference { get; set; }
    public GameObject GetArrowHighLight()=> _regularArrowHighLight.reference;
    public int GetCount
    {
        get => 7;
        set {}
    }
}
