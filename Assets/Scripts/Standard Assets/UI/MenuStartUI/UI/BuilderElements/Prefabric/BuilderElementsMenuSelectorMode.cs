using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderElementsMenuSelectorMode : IBuilderMenuStart
{
    private ListGameObjectsReference listElementsPreFabricData;
    private SOGameObject _regularArrowHighLight;
    
    private List<GameObject> _elements;

    public BuilderElementsMenuSelectorMode()
    {
        listElementsPreFabricData =
            Resources.Load<ListGameObjectsReference>("ScriptableObject/Data/ListGameObjects/SelectorMapData");
        _regularArrowHighLight =
            Resources.Load<SOGameObject>("ScriptableObject/Data/GameObjectData/RegularHighLightArrowUI");
    }
    public List<GameObject> Build(bool isDinamicThisMenu)
    {
        _elements = new List<GameObject>();
        foreach (var prefab in listElementsPreFabricData.reference)
        {
            var element = GameObject.Instantiate(prefab);
            _elements.Add(element);
        }
        GetCount = _elements.Count;
        SizeReference = Vector2.zero;
        return _elements;
    }

    public Vector2 SizeReference { get; set; }
    public GameObject GetArrowHighLight() => _regularArrowHighLight.reference;

    public int GetCount { get; set; }
}
