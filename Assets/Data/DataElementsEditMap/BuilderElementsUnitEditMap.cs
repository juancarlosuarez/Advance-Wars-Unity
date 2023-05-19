using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BuilderElementsUnitEditMap
{
    private DataUnitEditorMapUISO _dataMenu;

    private Image _coverImage;
    private Image _spriteElement;
    private TextMeshProUGUI _nameElement;
    private GameObject _currentElement;
    private readonly float _offset = 1.1f;

    private CreateImageFromZero _createImageFromZero;

    public List<Vector2> GetPositionElements()
    {
        List<Vector2> positions = new List<Vector2>();

        for (int i = 0; i < 5; i++)
        {
            var cameraPosition = Camera.main.gameObject.transform.position;
            var offset = new Vector2(-2, -2);
            var positionInMenu = new Vector2(cameraPosition.x + offset.x, cameraPosition.y + offset.y);

            Vector2 elementPosition = new Vector2((_offset * i ) + positionInMenu.x, positionInMenu.y);
            positions.Add(elementPosition);
        }

        return positions;
    }
    public List<GameObject> MakeAllElements(DataUnitEditorMapUISO dataMenu, GameObject parent)
    {
        _dataMenu = dataMenu;
        List<GameObject> allElements = new List<GameObject>();
        var subMenu = new GameObject("MenuUnitEditMapUI");
        subMenu.SetParent(parent);
        
        foreach (var data in _dataMenu.GetDataMenu)
        {
            Element(subMenu, data);
            ResizeElements();
            RelocateElements();
            
            allElements.Add(_currentElement);
            _currentElement.SetActive(false);
        }
        return allElements;
    }
    private void Element(GameObject parent, DataUnitEditorMapUI dataMenu)
    {
        _currentElement = new GameObject("Element");
        _currentElement.SetParent(parent);
        
        _createImageFromZero = new CreateImageFromZero(Vector2.zero, _currentElement);
        CoverImage();
        SpriteElement(dataMenu.sprite);
        NameElement(_currentElement, dataMenu.name.ToString());
    }
    
    private void CoverImage()
    {
        _coverImage = _createImageFromZero.CreateImage("CoverImage", Color.black);
        _coverImage.color = new Color(0,0,0, 0.2f);
    }
    private void SpriteElement(Sprite sprite)
    {
        _spriteElement = _createImageFromZero.CreateImage("sprite", Color.white);
        _spriteElement.sprite = sprite;
    }
    private void NameElement(GameObject parent, string nameElement)
    {
        var element = new GameObject("name");
        _nameElement = element.AddComponent<TextMeshProUGUI>();
        _nameElement.text = nameElement;
        element.SetParent(parent);
    }
    private void ResizeElements()
    {
        Vector2 sizeCover = new Vector2(1, 2);
        Vector2 sizeSprite = new Vector2(.5f, .5f);
        Vector2 sizeName = new Vector2(1, .3f);

        _coverImage.rectTransform.sizeDelta = sizeCover;
        _spriteElement.rectTransform.sizeDelta = sizeSprite;
        
        _nameElement.rectTransform.sizeDelta = sizeName;
        _nameElement.fontSize = .2f;
        _nameElement.alignment = TextAlignmentOptions.Center;
    }
    private void RelocateElements()
    {
        Vector3 coverImagePosition = _coverImage.transform.position;
        Vector2 positionSprite = new Vector2(coverImagePosition.x, coverImagePosition.y + .2f);
        Vector2 positionName = new Vector2(coverImagePosition.x, coverImagePosition.y - .5f);
        
        _spriteElement.transform.position = positionSprite;
        _nameElement.transform.position = positionName;
    }
}
