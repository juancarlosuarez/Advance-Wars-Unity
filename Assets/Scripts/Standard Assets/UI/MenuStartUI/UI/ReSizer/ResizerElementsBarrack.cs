using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public struct ResizerElementsBarrack
{
    private float _heightIcon;
    private float _widthIcon;

    private float _widthMax;
    private float _widthMaxText2;
    private ElementUIWithTwoTextAndIcon _currentData;

    public void RelocateElements(List<ElementUIWithTwoTextAndIcon> dataElements)
    {
        _heightIcon = dataElements[0].iconImage.rectTransform.sizeDelta.y;
        _widthIcon = dataElements[0].iconImage.rectTransform.sizeDelta.x;
        _widthMax = 8f;
        _widthMaxText2 = dataElements.Select(c => c.textElement2.preferredWidth).Max();
        
        foreach (var c in dataElements)
        {
            _currentData = c;
            ResizeCoverImage();
            RelocateIcon();
            RelocateText1();
            RelocateText2();
        }
    }

    private void ResizeCoverImage() => _currentData.coverImage.rectTransform.sizeDelta = SizeCover();
    private Vector2 SizeCover()
    {
        float widthImage = _widthMax;
        float heightImage = _heightIcon + 0.06f;

        Vector2 size = new Vector2(widthImage, heightImage);
        return size;
    }

    private void RelocateIcon()
    {
        Vector2 positionCoverImage = _currentData.coverImage.transform.position;
        Vector2 sizeCoverImage = _currentData.coverImage.rectTransform.sizeDelta;

        Vector2 sizeIcon = _currentData.iconImage.rectTransform.sizeDelta;

        //This formula send the icon to the left end of the cover image. 
        _currentData.iconImage.transform.position = new Vector2(positionCoverImage.x - sizeCoverImage.x / 2 + sizeIcon.x / 2,
            positionCoverImage.y);
    }

    private void RelocateText1()
    {
        float offset = 0.08f;
        Vector2 positionCoverImage = _currentData.coverImage.transform.position;
        Vector2 sizeCoverImage = _currentData.coverImage.rectTransform.sizeDelta;

        Vector2 sizeIcon = _currentData.iconImage.rectTransform.sizeDelta;
        
        _currentData.textElement1.rectTransform.sizeDelta = sizeCoverImage;

        float positionXText = positionCoverImage.x - sizeCoverImage.x / 2 + sizeIcon.x +
                              _currentData.textElement1.rectTransform.sizeDelta.x / 2 + offset;
        float positionYText = positionCoverImage.y;
        Vector2 positionText = new Vector2(positionXText, positionYText);
        
        _currentData.textElement1.transform.position = positionText;
    }

    private void RelocateText2()
    {
        float offset = .2f;
        Vector2 positionCoverImage = _currentData.coverImage.transform.position;
        Vector2 sizeCoverImage = _currentData.coverImage.rectTransform.sizeDelta;

        Vector2 sizeIcon = _currentData.iconImage.rectTransform.sizeDelta;

        Vector2 sizeText = new Vector2(_widthMaxText2, _currentData.coverImage.rectTransform.sizeDelta.y);
        _currentData.textElement2.rectTransform.sizeDelta = sizeText;

        float positionXText = positionCoverImage.x + sizeCoverImage.x / 2 - sizeText.x / 2 - offset;
        float positionYText = positionCoverImage.y;
        Vector2 positionText = new Vector2(positionXText, positionYText);

        _currentData.textElement2.transform.position = positionText;
    }
}
