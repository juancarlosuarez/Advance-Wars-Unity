using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BuilderDisplayBuilds : IBuilderMenuStart
{
    private readonly SOGameObject _regularArrowHighLight;
    private readonly OptionStart4Icons _data;
    private readonly IntReference _currentIDDocummentJSON;
    
    private List<byte> _allAmmountsBuilds;
    private string pathJSON = Application.dataPath + Path.AltDirectorySeparatorChar +
                              "/StreamingAssets/JSON/MapsLoad/DesignMap/DataUnit/SaveDataUnit";

    private SaveMap.ListUnitDataSave _dataUnit;
    private List<ElementUI4IconsAndCount> _allElementsData;

    private List<GameObject> _allElements;
    public BuilderDisplayBuilds()
    {
        _regularArrowHighLight =
            Resources.Load<SOGameObject>(
                "ScriptableObject/Data/GameObjectData/RegularHighLightArrowUI");
        _data = Resources.Load<OptionStart4Icons>("ScriptableObject/UI/OptionsStart/MenuDisplayBuilds");
        _currentIDDocummentJSON = Resources.Load<IntReference>("ScriptableObject/Data/IntData/KeyForBuildsDisplay");
    }
    public List<GameObject> Build(bool isMenuTryingToBeUpdated)
    {
        if (isMenuTryingToBeUpdated)
        {
             UpdateData();
            return _allElements;
        }
        
        var elementsBuilder = new BuildElementsMenuStart();

        _allElementsData = new List<ElementUI4IconsAndCount>();
        _allElements = new List<GameObject>();

        Vector2 sizeIcon = new Vector2(1.75f, 1.53f);

        foreach (var c in _data.GetData())
        {
            var elementData = elementsBuilder.GetElementUI4IconsAndCount(c.icons, c.texts, sizeIcon);
            
            _allElementsData.Add(elementData);
            _allElements.Add(elementData.parent);
        }
        var resizer = new ReSizerElementsWithTextAndIcon();
        resizer.RelocateElements(_allElementsData, sizeIcon);

        SizeReference = _allElementsData[0].coverImage.rectTransform.sizeDelta;
        GetCount = _allElements.Count;
        UpdateData();
        return _allElements;
    }
    private void UpdateData()
    {
        GetTextFromJSON();
        GetNumberOfUnits();
        //Here im doing this, because 0 is city, 1 is HQ, 2 AirPort and 3 Port, im always taking the allElementData[0]
        //because there is just one element.
        for (int i = 0; i < 4; i++)
        {
            if (i == 0) _allElementsData[0].textsIcons[0].text = _allAmmountsBuilds[0].ToString();
            if (i == 1) _allElementsData[0].textsIcons[1].text = _allAmmountsBuilds[1].ToString();
            if (i == 2) _allElementsData[0].textsIcons[2].text = _allAmmountsBuilds[2].ToString();
            if (i == 3) _allElementsData[0].textsIcons[3].text = _allAmmountsBuilds[3].ToString();
        }
    }
    private void GetTextFromJSON()
    {
        var path = pathJSON + _currentIDDocummentJSON.reference + ".json";
            
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        _dataUnit = JsonUtility.FromJson<SaveMap.ListUnitDataSave>(json);
        
        reader.Close();
    }

    private void GetNumberOfUnits()
    {
        _allAmmountsBuilds = new List<byte>();
        for (int i = 0; i < 4; i++) _allAmmountsBuilds.Add(0);
        
        foreach (var unitData in _dataUnit.unitDataList)
        {
            if ((NameUnit)unitData.nameUnit == NameUnit.City || (NameUnit)unitData.nameUnit == NameUnit.Base) _allAmmountsBuilds[0]++;
            if ((NameUnit)unitData.nameUnit == NameUnit.HQ) _allAmmountsBuilds[1]++;
            if ((NameUnit)unitData.nameUnit == NameUnit.Arprt) _allAmmountsBuilds[2]++;
            if ((NameUnit)unitData.nameUnit == NameUnit.Port) _allAmmountsBuilds[3]++;
        }
    }
    public Vector2 SizeReference { get; set; }
    public GameObject GetArrowHighLight() => _regularArrowHighLight.reference;
    
    

    public int GetCount { get; set; }
}
