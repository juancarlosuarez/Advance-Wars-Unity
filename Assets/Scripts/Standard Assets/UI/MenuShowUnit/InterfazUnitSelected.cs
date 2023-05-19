using System.Collections;
using System.Collections.Generic;
using Extensibles;
using UnityEngine;
using UnityEngine.UI;
public class InterfazUnitSelected : MonoBehaviour
{
    [SerializeField] private SetSprite _unitSprite;
    [SerializeField] private SetString _unitName;
    [SerializeField] private SetString _unitLife;
    [SerializeField] private SetString _unitEnergy;

    [SerializeField] private GameObject _desactivable;
    public Image coverImage;
    public bool UpdateData(TileRefactor tileSelected)
    {
        var unitSelect = tileSelected.occupiedSoldier;
        if (unitSelect == null)
        {
            DisableMenu();
            return false;
        }
        _unitSprite.UpdateData(unitSelect.spriteUI);
        _unitName.UpdateData(unitSelect.nameUnit.ToString());
        _unitLife.UpdateData(unitSelect.currentLife.ToString());
        _unitEnergy.UpdateData(100.ToString());
        
        _desactivable.SetActive(true);
        return true;
    }
    private void DisableMenu() => _desactivable.SetActive(false);
    //TEngo que poner la animacion aquella
}
