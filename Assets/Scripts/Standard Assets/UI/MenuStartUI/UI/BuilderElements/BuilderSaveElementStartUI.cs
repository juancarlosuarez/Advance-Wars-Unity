using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderSaveElementStartUI : IBuilderMenuStart
{
    private BuildElementsMenuStart _elementBuilder;

    private OptionStartJustText _data;
    private readonly SOGameObject _regularArrowHighLight;

    public BuilderSaveElementStartUI()
    {
        _regularArrowHighLight =
            Resources.Load<SOGameObject>("ScriptableObject/Data/GameObjectData/RegularHighLightArrowUI");
        _data = Resources.Load<OptionStartJustText>("ScriptableObject/UI/OptionsStart/SaveDataElementUI");
    }
    public List<GameObject> Build(bool isThisMenuDinamic)
    {
        _elementBuilder = new BuildElementsMenuStart();
        
        var elementDataWithText = new List<ElementUIWithText>();
        var allElements = new List<GameObject>();

        foreach (var c in _data.GetData())
        {
            var elementData = _elementBuilder.GetElementWithText(c.text);

            elementDataWithText.Add(elementData);
            allElements.Add(elementData.parent);
        }

        var resizer = new ReSizerElementsWithTextAndIcon();
        Vector2 size = new Vector2(1.75f, 1.53f);
        resizer.RelocateElements(elementDataWithText, size);

        SizeReference = elementDataWithText[0].coverImage.rectTransform.sizeDelta;
        GetCount = allElements.Count;
        return allElements;
    }

    public Vector2 SizeReference { get; set; }
    public GameObject GetArrowHighLight() => _regularArrowHighLight.reference;
    public int GetCount { get; set; }
}