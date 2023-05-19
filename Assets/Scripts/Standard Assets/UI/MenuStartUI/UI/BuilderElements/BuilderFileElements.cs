using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderFileElements : IBuilderMenuStart
{
    private BuildElementsMenuStart _elementBuilder;
    public Vector2 SizeReference { get; set; }

    private OptionStartTextAndIcon _data;
    private readonly SOGameObject _regularArrowHighLight;

    public BuilderFileElements()
    {
        _data = Resources.Load<OptionStartTextAndIcon>(
            "ScriptableObject/UI/OptionsStart/FileOptionData");
        _regularArrowHighLight =
            Resources.Load<SOGameObject>("ScriptableObject/Data/GameObjectData/RegularHighLightArrowUI");
    }

    public List<GameObject> Build(bool isThisMenuDinamic)
    {
        _elementBuilder = new BuildElementsMenuStart();
        
        var allElementsData = new List<ElementUIWithTextAndIcon>();
        var allELements = new List<GameObject>();
        
        Vector2 sizeIcon = new Vector2(1.75f, 1.53f); 

        foreach (var c in _data.GetData())
        {
            var elementData = _elementBuilder.GetElementWithTextAndIcon(c.text, c.icon, sizeIcon, false);
            
            allELements.Add(elementData.parent);
            allElementsData.Add(elementData);
        }
        
        var relocater = new ReSizerElementsWithTextAndIcon();
        relocater.RelocateElements(allElementsData);

        SizeReference = allElementsData[0].coverImage.rectTransform.sizeDelta;
        GetCount = allELements.Count;
        return allELements;
    }
    public GameObject GetArrowHighLight() => _regularArrowHighLight.reference;
    public int GetCount { get; set; }
}
