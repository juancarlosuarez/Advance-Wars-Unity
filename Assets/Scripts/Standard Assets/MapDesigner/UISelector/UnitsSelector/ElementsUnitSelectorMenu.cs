using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsUnitSelectorMenu : MonoBehaviour
{
    [SerializeField] private SetString nameUnit;
    [SerializeField] private SetSpritesCanvas iconUnit;

    private AbstractBaseUnit _unitPrefab;
    private string _nameUnit;
    private Sprite _spriteElement;

    public AbstractBaseUnit GetUnitPrefab() => _unitPrefab;
    public string GetNameUnit() => _nameUnit;
    public Sprite GetSpriteUnit() => _spriteElement;

    public void InitElementMenuUnit(AbstractBaseUnit unit)
    {
        _unitPrefab = unit;
        _nameUnit = unit.name;
        _spriteElement = unit.spriteRenderer.sprite;
        
        nameUnit.UpdateData(_nameUnit);
        iconUnit.UpdateData(_spriteElement);
    }
}
