using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObject/Soldiers/ManagerLife")]
public class ManagerLifeUnitUI : ScriptableObject
{
    [SerializeField] private List<Sprite> _numbersSprites0to9;

    public void SetLife(Soldier unit)
    {
        var lifeUI = unit.lifeUISpriteRenderer;
        if (unit.currentLifeUI == 10)
        {
            if (lifeUI.transform.gameObject.activeSelf) lifeUI.transform.gameObject.SetActive(false);
            return;
        }
        var spriteNumber = ConverterIntToSprite(unit.currentLifeUI);
        
        lifeUI.transform.gameObject.SetActive(true);
        lifeUI.sprite = spriteNumber;
        lifeUI.sortingOrder = 3;
    }
    private Sprite ConverterIntToSprite(int splitNumber)
    {
        switch (splitNumber)
        {
            case 1: return _numbersSprites0to9[0];
            case 2: return _numbersSprites0to9[1];
            case 3: return _numbersSprites0to9[2];
            case 4: return _numbersSprites0to9[3];
            case 5: return _numbersSprites0to9[4];
            case 6: return _numbersSprites0to9[5];
            case 7: return _numbersSprites0to9[6];
            case 8: return _numbersSprites0to9[7];
            case 9: return _numbersSprites0to9[8];
            default: return null;
        }
    }
}
