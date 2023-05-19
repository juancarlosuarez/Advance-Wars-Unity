using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderOptionMenuEdit : IBuilderMenuStart
{
    private OptionStartTextAndIcon _data;
    private readonly SOGameObject _regularArrowHighLight;
    private BuildElementsMenuStart _elementBuilder;
    public Vector2 SizeReference { get; set; }
    public int GetCount { get; set; }

    public BuilderOptionMenuEdit()
    {
        _data = Resources.Load<OptionStartTextAndIcon>("ScriptableObject/UI/OptionsStart/OptionEditsData");
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
        
        var relocater = new ReSizerElementsWithTextAndIcon();
        relocater.RelocateElements(allElementsData);

        SizeReference = allElementsData[0].coverImage.rectTransform.sizeDelta;
        GetCount = allElements.Count;
        return allElements;
    }

    public GameObject GetArrowHighLight() => _regularArrowHighLight.reference;
}
