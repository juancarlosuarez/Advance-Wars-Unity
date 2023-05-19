using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemUnitBarrack : MonoBehaviour
{
    [SerializeField] SetSprite spriteSetComponent;
    [SerializeField] NumberSet ammountGoldComponent;
    [SerializeField] SetString nameComponent;

    [SerializeField] GameObject highLight;
    public void Init(UnitDisplay unit, GameObject parent)
    {
        spriteSetComponent.UpdateData(unit.unitSprite);
        ammountGoldComponent.UpdateData(unit.goldCost);
        nameComponent.UpdateData(unit.nameUnit);

        transform.SetParent(parent.transform);
    } 
    public void HighLight()
    {
        highLight.SetActive(true);
    }
    public void LowHighLight()
    {
        highLight.SetActive(false);
    }
}
