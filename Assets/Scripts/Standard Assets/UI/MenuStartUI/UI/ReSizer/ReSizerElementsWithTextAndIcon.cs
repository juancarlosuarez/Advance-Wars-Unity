using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public struct ReSizerElementsWithTextAndIcon
{
    private float _heightIcon;
    private float _widthIcon;

    private float _widthMaxText;
    
    public void RelocateElements(List<ElementUIWithTextAndIcon> dataElements)
    {
        _heightIcon = dataElements[0].iconImage.rectTransform.sizeDelta.y;
        _widthIcon = dataElements[0].iconImage.rectTransform.sizeDelta.x;
        _widthMaxText = dataElements.Select(x => x.textElement.preferredWidth).Max();

        foreach (var c in dataElements)
        {
            ResizeCoverImage(c.coverImage);
            RelocateIcon(c.coverImage, c.iconImage);
            RelocateText(c);
        }
    }


    public void RelocateElements(List<ElementUIWithText> dataElements, Vector2 sizeCover)
    {
        _heightIcon = sizeCover.y;
        _widthIcon = sizeCover.x;
        _widthMaxText = dataElements.Select(x => x.textElement.preferredWidth).Max();

        foreach (var c in dataElements)
        {
            ResizeCoverImage(c.coverImage);
            RelocateTextWithOutIcon(c);
        }
    }
    public void RelocateElements(List<ElementUIWithTextAndIcon> dataElementsWithIcon, ElementUIWithText elementText)
    {
        _heightIcon = dataElementsWithIcon[0].iconImage.rectTransform.sizeDelta.y;
        _widthIcon = dataElementsWithIcon[0].iconImage.rectTransform.sizeDelta.x;
        _widthMaxText = dataElementsWithIcon.Select(x => x.textElement.preferredWidth).Max();
        if (_widthMaxText < elementText.textElement.preferredWidth) _widthMaxText = elementText.textElement.preferredWidth;

        for (int i = 0; i < dataElementsWithIcon.Count + 1; i++)
        {
            if (i < dataElementsWithIcon.Count)
            {
                var c = dataElementsWithIcon[i];
                ResizeCoverImage(c.coverImage);
                RelocateIcon(c.coverImage, c.iconImage);
                RelocateText(c);
            }
            else
            {
                ResizeCoverImage(elementText.coverImage);
                RelocateTextWithOutIcon(elementText);
            }
        }
    }
    public void RelocateElements(List<ElementUI4IconsAndCount> dataElements, Vector2 sizeCover)
    {
        _heightIcon = dataElements[0].iconImage[0].rectTransform.sizeDelta.y;
        _widthIcon = dataElements[0].iconImage[0].rectTransform.sizeDelta.x;
        _widthMaxText = dataElements[0].textsIcons[0].preferredWidth;

        foreach (var data in dataElements)
        {
            SizeCover4Icons(data.iconImage[0].rectTransform.sizeDelta, data.coverImage);
            for (int i = 0; i < 4; i++)
            {
                RelocateIconSplitIn4(data.coverImage, data.iconImage[i], i);
                RelocateTextSplitIn4(data.textsIcons[i], data.coverImage, i);                
            }
        }
    }

    private void RelocateIconSplitIn4(Image coverImage, Image iconImage ,int count)
    {
        float iconImagePositionX = 0;
        float iconImagePositionY = coverImage.transform.position.y + .5f;
        switch (count)
        {
            case 0:
                iconImagePositionX = coverImage.transform.position.x - coverImage.rectTransform.sizeDelta.x / 2 +
                                     iconImage.rectTransform.sizeDelta.x / 2;
                break;
            case 1:
                iconImagePositionX = coverImage.transform.position.x - coverImage.rectTransform.sizeDelta.x / 4 +
                                     iconImage.rectTransform.sizeDelta.x / 2;
                break;
            case 2: 
                iconImagePositionX = coverImage.transform.position.x + coverImage.rectTransform.sizeDelta.x / 4 -
                                     iconImage.rectTransform.sizeDelta.x / 2;
                break;
            case 3:
                iconImagePositionX = coverImage.transform.position.x + coverImage.rectTransform.sizeDelta.x / 2 -
                                     iconImage.rectTransform.sizeDelta.x / 2;
                break;
        }

        iconImage.rectTransform.sizeDelta *= 1.75f;
        iconImage.transform.position =
            new Vector3(iconImagePositionX, iconImagePositionY, iconImage.transform.position.z);
    }
    private void RelocateTextSplitIn4(TextMeshProUGUI textElement, Image coverImage, int count)
    {
        float iconImagePositionX = 0;
        float iconImagePositionY = coverImage.transform.position.y - coverImage.rectTransform.sizeDelta.y / 4;
        switch (count)
        {
            case 0:
                iconImagePositionX = coverImage.transform.position.x - coverImage.rectTransform.sizeDelta.x / 2 +
                                     textElement.rectTransform.sizeDelta.x;
                break;
            case 1:
                iconImagePositionX = coverImage.transform.position.x - coverImage.rectTransform.sizeDelta.x / 4 +
                                     textElement.rectTransform.sizeDelta.x;
                break;
            case 2: 
                iconImagePositionX = coverImage.transform.position.x + coverImage.rectTransform.sizeDelta.x / 4 -
                                     textElement.rectTransform.sizeDelta.x / 2;
                break;
            case 3:
                iconImagePositionX = coverImage.transform.position.x + coverImage.rectTransform.sizeDelta.x / 2 -
                                     textElement.rectTransform.sizeDelta.x / 2;
                break;
        }

        textElement.transform.position =
            new Vector3(iconImagePositionX, iconImagePositionY, textElement.transform.position.z);
    }
    private void RelocateIcon(Image coverImage, Image iconImage)
    {
        Vector2 positionCoverImage = coverImage.transform.position;
        Vector2 sizeCoverImage = coverImage.rectTransform.sizeDelta;

        Vector2 sizeIcon = iconImage.rectTransform.sizeDelta;

        iconImage.transform.position = new Vector2(positionCoverImage.x - sizeCoverImage.x / 2 
                                                        + sizeIcon.x / 2, positionCoverImage.y);
    }
    #region RelocateCoverImageFunctions

    private void ResizeCoverImage(Image coverImage) => coverImage.rectTransform.sizeDelta = SizeCover();
    private Vector2 SizeCover()
    {
        float offset = 0.06f;
        
        float widthImage = _widthMaxText + _widthIcon + offset;
        float heightImage = _heightIcon + 0.06f;

        Vector2 size = new Vector2(widthImage, heightImage);
        return size;
    }
    #endregion

    private void SizeCover4Icons(Vector2 sizeVector, Image coverImage)
    {
        float offset = 0.06f;
        float sizeCoverX = sizeVector.x * 4.5f;
        float sizeCoverY = _heightIcon + offset;

        coverImage.rectTransform.sizeDelta = new Vector2(sizeCoverX, sizeCoverY);
    }
    private void RelocateText(ElementUIWithTextAndIcon data)
    {
        float offset = 0.08f;
        Vector2 positionCoverImage = data.coverImage.transform.position;
        Vector2 sizeCoverImage = data.coverImage.rectTransform.sizeDelta;

        Vector2 sizeIcon = data.iconImage.rectTransform.sizeDelta;
        
        data.textElement.rectTransform.sizeDelta = sizeCoverImage;
        
        Vector2 positionText = new Vector2(positionCoverImage.x - sizeCoverImage.x / 2 + sizeIcon.x + data.textElement.rectTransform.sizeDelta.x / 2 + offset, positionCoverImage.y - 0.36f);
        data.textElement.transform.position = positionText;
        //
        //     textElement.transform.position = positionText;
    }

    private void RelocateTextWithOutIcon(ElementUIWithText data)
    {
        Vector2 positionCoverImage = data.coverImage.transform.position;
        Vector2 sizeCoverImage = data.coverImage.rectTransform.sizeDelta;

        data.textElement.rectTransform.sizeDelta = sizeCoverImage;
        data.textElement.rectTransform.position = positionCoverImage;
        data.textElement.alignment = TextAlignmentOptions.Center;
    }
}
