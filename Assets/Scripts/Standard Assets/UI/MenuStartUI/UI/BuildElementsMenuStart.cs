using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
public class BuildElementsMenuStart
{
    private bool _isSmallSize;
    public GameObject BuildArrowHighLight(Vector2 size, Sprite sprite, string name)
    {
        var arrowImage = new GameObject(name).AddComponent<Image>();

        arrowImage.sprite = sprite;
        arrowImage.rectTransform.sizeDelta = size;

        return arrowImage.gameObject;
    }
    private Image BuildMenuParent()
    {
        var parentMenu = new GameObject("ElementMenuStart ").AddComponent<Image>();
        
        return parentMenu;
    }
    private TextMeshProUGUI BuildText(string textForELement)
    {
        var textMesh = new GameObject("Text").AddComponent<TextMeshProUGUI>();
        var sizeText = 0f;

        if (_isSmallSize) sizeText = 0.42f;
        else sizeText = 0.42f * 2;
        
        textMesh.fontSize = sizeText;
        textMesh.SetText(textForELement);
        return textMesh;
    }
    private Image BuildIcon(Sprite sprite, Vector2 size)
    {
        var iconImage = new GameObject("Icon").AddComponent<Image>();
        
        iconImage.preserveAspect = true;
        iconImage.rectTransform.sizeDelta = size;
        iconImage.sprite = sprite;
        return iconImage;
    }

    public ElementUIWithTwoTextAndIcon GetElementWithTwoTextAndIcon(string text1, string text2, Sprite spriteIcon,
        Vector2 sizeIcon, bool isSmallSize)
    {
        var element = new ElementUIWithTwoTextAndIcon();
        var parent = new GameObject("Element Soldier " + text1);

        element.iconImage = BuildIcon(spriteIcon, sizeIcon);
        element.textElement1 = BuildText(text1);
        element.textElement2 = BuildText(text2);
        element.coverImage = BuildMenuParent();
        element.parent = parent;
        
        element.coverImage.gameObject.SetParent(parent);
        element.iconImage.gameObject.SetParent(parent);
        element.textElement1.gameObject.SetParent(parent);
        element.textElement2.gameObject.SetParent(parent);
        
        return element;
    }
    public ElementUIWithTextAndIcon GetElementWithTextAndIcon(string text, Sprite spriteIcon, Vector2 sizeIcon, bool isSmallSize)
    {
        _isSmallSize = isSmallSize;
        
        var elementStart = new ElementUIWithTextAndIcon();
        
        var parent = new GameObject("Element" + text);
        
        elementStart.iconImage = BuildIcon(spriteIcon, sizeIcon);
        
        elementStart.textElement = BuildText(text);
        elementStart.coverImage = BuildMenuParent();
        elementStart.parent = parent;
        
        elementStart.coverImage.gameObject.SetParent(parent);
        elementStart.iconImage.gameObject.SetParent(parent);
        elementStart.textElement.gameObject.SetParent(parent);
        
        return elementStart;
    }

    public ElementUIWithText GetElementWithText(string text)
    {
        var elementStart = new ElementUIWithText();

        var parent = new GameObject("Element" + text);

        elementStart.coverImage = BuildMenuParent();
        elementStart.textElement = BuildText(text);
        elementStart.parent = parent;
        
        elementStart.coverImage.gameObject.SetParent(parent);
        elementStart.textElement.gameObject.SetParent(parent);

        return elementStart;
    }

    public ElementUI4IconsAndCount GetElementUI4IconsAndCount(List<Sprite> allIcons, List<string> allTexts, Vector2 sizeIcon)
    {
        var elementStart = new ElementUI4IconsAndCount();

        var parent = new GameObject("ElementParent");
        elementStart.coverImage = BuildMenuParent();
        elementStart.parent = parent;
        elementStart.iconImage = new List<Image>();
        elementStart.textsIcons = new List<TextMeshProUGUI>();
        
        elementStart.coverImage.gameObject.SetParent(parent);
        for (int i = 0; i < 4; i++)
        {
            elementStart.iconImage.Add(BuildIcon(allIcons[i], sizeIcon));
            elementStart.textsIcons.Add(BuildText(allTexts[i])); 
            
            elementStart.textsIcons[i].rectTransform.sizeDelta = new Vector2(1, .7f);
            
            elementStart.iconImage[i].gameObject.SetParent(parent);
            elementStart.textsIcons[i].gameObject.SetParent(parent);
        }
        return elementStart;
    }
}
public struct ElementUIWithTextAndIcon
{
    public Image iconImage;
    public TextMeshProUGUI textElement;
    public Image coverImage;
    public GameObject parent;
}

public struct ElementUIWithText
{
    public Image coverImage;
    public TextMeshProUGUI textElement;
    public GameObject parent;
}

public struct ElementUIWithTwoTextAndIcon
{
    public Image iconImage;
    public TextMeshProUGUI textElement1;
    public TextMeshProUGUI textElement2;
    public Image coverImage;
    public GameObject parent;
}

public struct ElementUI4IconsAndCount
{
    public List<Image> iconImage;
    public List<TextMeshProUGUI> textsIcons;
    public Image coverImage;
    public GameObject parent;
}