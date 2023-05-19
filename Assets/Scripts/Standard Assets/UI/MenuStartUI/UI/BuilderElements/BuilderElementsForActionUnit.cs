using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuilderElementsForActionUnit : IBuilderMenuStart
{
    
    public Vector2 SizeReference { get; set; }

    private OptionStartTextAndIcon _data;
    private BuildElementsMenuStart _elementBuilder;
    
    private SOListInt _listActionsID;
    private SOVector _sizeReference;
    private ListGameObjectsReference _optionsUnitUIGameObject;
    private readonly SOGameObject _smallSizeHighLightArrow;

    public BuilderElementsForActionUnit()
    {
        _listActionsID = Resources.Load<SOListInt>("ScriptableObject/Data/ListIntData/OptionsUnit/ListActionsID");
        _optionsUnitUIGameObject =
            Resources.Load<ListGameObjectsReference>(
                "ScriptableObject/Data/ListGameObjects/OptionsUnitUIGameObject");
        _data = Resources.Load<OptionStartTextAndIcon>("ScriptableObject/UI/OptionsStart/OptionsUnitData");
        _sizeReference = Resources.Load<SOVector>("ScriptableObject/Data/VectorData/SizeReferenceOptionUnitUI");
        _smallSizeHighLightArrow =
            Resources.Load<SOGameObject>("ScriptableObject/Data/GameObjectData/SmallHighLightArrowUI");
    }
    public List<GameObject> Build(bool isThisMenuDynamic)
    {
        _elementBuilder = new BuildElementsMenuStart();
        
        if (isThisMenuDynamic) return OffSetElements();

        _listActionsID.reference = new List<int>();
        for (int i = 0; i < 6; i++) _listActionsID.reference.Add(i);
        

        var allElementsData = new List<ElementUIWithTextAndIcon>();

        Vector2 sizeIcon = new Vector2(1.75f / 2, 1.53f / 2);

        foreach (var c in _data.GetData())
        {
            var elementData = _elementBuilder.GetElementWithTextAndIcon(c.text, c.icon, sizeIcon, true);
            
            _optionsUnitUIGameObject.reference.Add(elementData.parent);
            allElementsData.Add(elementData);
        }

        var relocater = new ReSizerElementsWithTextAndIcon();
        relocater.RelocateElements(allElementsData);

        SizeReference = allElementsData[0].coverImage.rectTransform.sizeDelta;
        _sizeReference.reference = SizeReference;
        
        return OffSetElements();
    }

    private List<GameObject> OffSetElements()
    {
        foreach (var c in _optionsUnitUIGameObject.reference) c.SetActive(false);
        
        var elementsSelect = _listActionsID.reference.Select(c =>
        {
            _optionsUnitUIGameObject.reference[c].SetActive(true);
            return _optionsUnitUIGameObject.reference[c];
        }).ToList();
        SizeReference = _sizeReference.reference;
        GetCount = elementsSelect.Count;
        return elementsSelect;
    }
    public GameObject GetArrowHighLight() => _smallSizeHighLightArrow.reference;
    public int GetCount { get; set; }
}
