using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public static class ImagenMenuCreationExtension
{
    public static Vector3[] FinderCorners(this Image image)
    {
        Vector3[] corner = new Vector3[4];

        image.rectTransform.GetWorldCorners(corner);

        return corner;
    }
    public static void PutInCorrectPositionElementInCorner(this Image image, Vector2 corner, int count)
    {
        Vector2 anchor = image.rectTransform.sizeDelta;
        float widthOffset = 0;
        float heightOffset = 0;

        switch (count)
        {
            case 0:
                widthOffset = anchor.x / 2;
                heightOffset = anchor.y / 2;
                break;
            case 1:
                widthOffset = anchor.x / 2;
                heightOffset = -anchor.y / 2;
                break;
            case 2:
                widthOffset = -anchor.x / 2;
                heightOffset = -anchor.y / 2;
                break;
            case 3:
                widthOffset = -anchor.x / 2;
                heightOffset = anchor.y / 2;
                break;
        }
        image.transform.position = new Vector2(corner.x - widthOffset, corner.y - heightOffset);
    }
    public static void ResizeImage(this Image imageResize, int numberOfLetter, float sizeLetter)
    {
        float spaceBetweenLettersCons = 1;
        float fontSizeLettersCons = 1.7f;

        imageResize.rectTransform.pivot = new Vector2(0, 0.5f);

        float currentSpaceBetweenLetters = spaceBetweenLettersCons * sizeLetter / fontSizeLettersCons;

        float spaceThatWillIncreaseMenu = currentSpaceBetweenLetters * numberOfLetter;

        imageResize.rectTransform.sizeDelta = new Vector2(spaceThatWillIncreaseMenu, imageResize.rectTransform.sizeDelta.y);

        imageResize.rectTransform.pivot = new Vector2(.5f, .5f);
    }
    public static void ResizeImage(this Image imageResize, Vector2 newSize) => imageResize.rectTransform.sizeDelta = newSize;
    public static void PutImageBehind(this Image mainImage, Image endImage)
    {
        float offsetNewImageX = endImage.rectTransform.sizeDelta.x * 2;
        float offsetNewImageY = endImage.rectTransform.sizeDelta.y * 2;

        GameObject newObj = new GameObject();
        Image newImage = newObj.AddComponent<Image>();

        Transform transformParent = mainImage.gameObject.transform.parent;

        newImage.color = Color.black;

        newImage.rectTransform.sizeDelta = new Vector2(mainImage.rectTransform.sizeDelta.x + offsetNewImageX,
            mainImage.rectTransform.sizeDelta.y + offsetNewImageY);

        newObj.transform.SetParent(transformParent);
        newObj.transform.SetSiblingIndex(0);

        newObj.transform.position = mainImage.transform.position;
    }
}

