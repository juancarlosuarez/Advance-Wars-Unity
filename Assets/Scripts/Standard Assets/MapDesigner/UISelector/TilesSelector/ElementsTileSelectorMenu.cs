using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsTileSelectorMenu : MonoBehaviour
{
    [SerializeField] private SetString nameTile;
    [SerializeField] private SetSpritesCanvas iconTile;

    private Tile _tilePrefab;
    private string _nameElement;
    private Sprite _spriteElement;

    public Tile GetTilePrefab() => _tilePrefab;
    public string GetNameElement => _nameElement;
    public Sprite GetSpriteElement => _spriteElement;
    public void InitElementMenuTile(Tile tile)
    {
        _tilePrefab = tile;
        _nameElement = tile.name;
        _spriteElement = tile.tileSpriteRenderer.sprite;
        
        nameTile.UpdateData(_nameElement);
        iconTile.UpdateData(_spriteElement);
    }
}