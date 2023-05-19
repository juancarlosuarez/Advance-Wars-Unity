using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateImageFromZero
{
    private GameObject parent;
    private Vector2 positionImage;
    public CreateImageFromZero(Vector2 _positionImage, GameObject _parent)
    {
        parent = _parent;
        positionImage = _positionImage;
    }
    public Image CreateImage(string nameImage, Color colorImage)
    {
        var newImageObject = new GameObject(nameImage).AddComponent<Image>();
        newImageObject.transform.SetParent(parent.transform);
        newImageObject.transform.position = positionImage;
        newImageObject.color = colorImage;
        return newImageObject;
    }
}
