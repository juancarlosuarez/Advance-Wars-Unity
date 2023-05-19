using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ContainerStruct/DataUnitEditor")]
public class DataUnitEditorMapUISO : ScriptableObject
{
    [SerializeField] private List<DataUnitEditorMapUI> _getDataMenu;

    public List<DataUnitEditorMapUI> GetDataMenu
    {
        get => _getDataMenu;
        private set => _getDataMenu = value;
    }
}
    [System.Serializable]
    public struct DataUnitEditorMapUI
    {
        public Sprite sprite;
        public NameUnit name;
        public PutElementsEditMap.TypeElementGeneric typeElement;
    }
