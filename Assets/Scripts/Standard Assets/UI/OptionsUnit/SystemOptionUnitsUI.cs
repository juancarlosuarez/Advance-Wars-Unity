using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemOptionUnitsUI : MonoBehaviour
{
    //"DELETE THIS SHIT PLEASE
    //Este script se adjunta a un panel en blanco que se ira rellenando, dependiendo de la accion que se mande.
    [SerializeField] SetSprite spriteIconComponent;
    [SerializeField] SetString nameAction;

    [SerializeField] GameObject highLight;

    public void Init(MultiplesOptionsUnit optionValues, GameObject parent)
    {
        spriteIconComponent.UpdateData(optionValues.iconAction);
        nameAction.UpdateData(optionValues.nameAction);

        transform.SetParent(parent.transform);
    }
    public void HighLight()
    {
        highLight.SetActive(true);
    }
    public void LowLight()
    {
        highLight.SetActive(false);
    }

}
