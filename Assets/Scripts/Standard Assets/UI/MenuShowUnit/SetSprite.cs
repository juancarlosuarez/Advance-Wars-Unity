using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetSprite : MonoBehaviour
{
    [SerializeField] private Image image;
    public void UpdateData(Sprite unitSprite)
    {
        image.sprite = unitSprite;
    }
}
