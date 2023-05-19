using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderElementsFillOptionStart : IBuilderMenuStart
{
    private BuildElementsMenuStart _elementBuilder;
    private OptionStartSomeElementsWithIconAndText _data;
    private SOGameObject _regularArrowHighLight;
    public BuilderElementsFillOptionStart()
    {
        _data = Resources.Load<OptionStartSomeElementsWithIconAndText>(
            "ScriptableObject/UI/OptionsStart/FillOptionData");
        _regularArrowHighLight =
            Resources.Load<SOGameObject>("ScriptableObject/Data/GameObjectData/RegularHighLightArrowUI");
    }
    
    public List<GameObject> Build(bool isThisMenuDinamic)
    {
        _elementBuilder = new BuildElementsMenuStart();

        var allElementsDataWithIcons = new List<ElementUIWithTextAndIcon>();
        var elementDataWithText = new ElementUIWithText();
        var allElements = new List<GameObject>();

        Vector2 sizeIcon = new Vector2(1.75f, 1.53f);
        for (int i = 0; i < _data.GetData().Count + 1; i++)
        {
            if (i != 4)
            {
                var c = _data.GetData()[i];
                var elementData = _elementBuilder.GetElementWithTextAndIcon(c.text, c.icon, sizeIcon, false);
                allElementsDataWithIcons.Add(elementData);
                allElements.Add(elementData.parent);
            }
            else
            {
                elementDataWithText = _elementBuilder.GetElementWithText(_data.GetDataElementWithoutIcon);
                allElements.Add(elementDataWithText.parent);
            }
        }
        var resizer = new ReSizerElementsWithTextAndIcon();
        resizer.RelocateElements(allElementsDataWithIcons, elementDataWithText);
        
        SizeReference = allElementsDataWithIcons[0].coverImage.rectTransform.sizeDelta;
        GetCount = allElements.Count;
        return allElements;
    }

    public Vector2 SizeReference { get; set; }
    public GameObject GetArrowHighLight() => _regularArrowHighLight.reference;
    public int GetCount { get; set; }
}
