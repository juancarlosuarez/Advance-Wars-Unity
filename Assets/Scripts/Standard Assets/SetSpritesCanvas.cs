using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetSpritesCanvas : MonoBehaviour
{
    [SerializeField] private Image image;
    public void UpdateData(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
