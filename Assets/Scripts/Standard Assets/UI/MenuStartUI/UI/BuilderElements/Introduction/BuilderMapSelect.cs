using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BuilderMapSelect : IBuilderMenuStart
{
    private OptionStartJustText _data;
    private string pathJSON = Application.dataPath + Path.AltDirectorySeparatorChar +
                              "/StreamingAssets/JSON/MapsLoad/DesignMap/DataMap/SaveDataMap";
    private readonly SOGameObject _regularArrowHighLight;
    private SaveMap.DataMap _dataMap;
    

    public BuilderMapSelect()
    {
        _data = Resources.Load<OptionStartJustText>("ScriptableObject/UI/OptionsStart/SelectMap");
        _regularArrowHighLight =
            Resources.Load<SOGameObject>("ScriptableObject/Data/GameObjectData/RegularHighLightArrowUI");
    }
    public List<GameObject> Build(bool isDinamicThisMenu)
    {
        var elementsBuilder = new BuildElementsMenuStart();

        var allElementsData = new List<ElementUIWithText>();
        var allElements = new List<GameObject>();
        Vector2 sizeIcon = new Vector2(1.75f, 1.53f);
        var count = 0;
        foreach (var c in _data.GetData())
        {
            GetTextFromJSON(count);
            var elementData = elementsBuilder.GetElementWithText(c.text);

            allElements.Add(elementData.parent);
            allElementsData.Add(elementData);
            count++;
        }

        var setterElements = new ReSizerElementsWithTextAndIcon();
        setterElements.RelocateElements(allElementsData, sizeIcon);

        SizeReference = allElementsData[0].coverImage.rectTransform.sizeDelta;
        GetCount = allElements.Count;
        return allElements;
    }
    private void GetTextFromJSON(int count)
    {
        var path = pathJSON + count + ".json";
            
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        _dataMap = JsonUtility.FromJson<SaveMap.DataMap>(json);
        _data.GetData()[count].text = _dataMap.mapName;
        reader.Close();
    }
    public Vector2 SizeReference { get; set; }
    public GameObject GetArrowHighLight() => _regularArrowHighLight.reference;
    public int GetCount { get; set; }
}
