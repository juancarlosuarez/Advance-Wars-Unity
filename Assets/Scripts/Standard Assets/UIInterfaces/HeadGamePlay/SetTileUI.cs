using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetTileUI : MonoBehaviour
{
    [SerializeField] private DataSpritesTiles dataSprites;
    [SerializeField] private Image _spriteRenderer;
    public void UpdateData(TileRefactor tileSelected)
    {
        var newSprite = dataSprites.GetSpriteTile(tileSelected.dataVariable.textureID);
        _spriteRenderer.sprite = newSprite;
    }
}