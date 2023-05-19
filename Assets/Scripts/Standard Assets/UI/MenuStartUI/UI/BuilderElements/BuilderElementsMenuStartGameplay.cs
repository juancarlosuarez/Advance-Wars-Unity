using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderElementsMenuStartGameplay : IBuilderMenuStart
{
    private BuildElementsMenuStart _elementBuilder;
    private OptionStartTextAndIcon _data;
    
    private readonly SOGameObject _regularArrowHighLight;
    
    public BuilderElementsMenuStartGameplay()
    {
        _data = Resources.Load<OptionStartTextAndIcon>(
            "ScriptableObject/UI/OptionsStart/MenuStartData");
        _regularArrowHighLight =
            Resources.Load<SOGameObject>(
                "ScriptableObject/Data/GameObjectData/RegularHighLightArrowUI");
    }
    public List<GameObject> Build(bool isDinamicThisMenu)
    {
        _elementBuilder = new BuildElementsMenuStart();

        var allElementsData = new List<ElementUIWithTextAndIcon>();
        var allElements = new List<GameObject>();
        Vector2 sizeIcon = new Vector2(1.75f, 1.53f);
        
        foreach (var c in _data.GetData())
        {
            var elementData = _elementBuilder.GetElementWithTextAndIcon(c.text, c.icon, sizeIcon, false);

            allElements.Add(elementData.parent);
            allElementsData.Add(elementData);
        }

        var setterElements = new ReSizerElementsWithTextAndIcon();
        setterElements.RelocateElements(allElementsData);

        SizeReference = allElementsData[0].coverImage.rectTransform.sizeDelta;
        GetCount = allElements.Count;
        return allElements;
    }

    public Vector2 SizeReference { get; set; }
    public GameObject GetArrowHighLight() => _regularArrowHighLight.reference;
    public int GetCount { get; set; }
}
