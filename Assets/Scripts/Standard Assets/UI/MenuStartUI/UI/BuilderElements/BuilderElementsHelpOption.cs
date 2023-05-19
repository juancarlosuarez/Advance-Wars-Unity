using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BuilderElementsHelpOption : IBuilderMenuStart
{
    private BuildElementsMenuStart _elementBuilder;

    private OptionStartJustText _data;
    private readonly SOGameObject _regularArrowHighLight;
    private string pathJSON = Application.dataPath + Path.AltDirectorySeparatorChar +
                              "/JSON/MapsLoad/DesignMap/DataMap/SaveDataMap";
    public BuilderElementsHelpOption()
    {
        _data = Resources.Load<OptionStartJustText>("ScriptableObject/UI/OptionsStart/Help1Data");
        _regularArrowHighLight =
            Resources.Load<SOGameObject>("ScriptableObject/Data/GameObjectData/RegularHighLightArrowUI");
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

    private void UpdateMenu()
    {
        
    }
    private void GetTextFromJSON(int count)
    {
        var path = pathJSON + 0 + ".json";
        
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        var _dataMap = JsonUtility.FromJson<SaveMap.DataMap>(json);
        _data.GetData()[count].text = _dataMap.mapName;
        
        reader.Close();
    }
    public Vector2 SizeReference { get; set; }
    public GameObject GetArrowHighLight() => _regularArrowHighLight.reference;
    public int GetCount { get; set; }
}
