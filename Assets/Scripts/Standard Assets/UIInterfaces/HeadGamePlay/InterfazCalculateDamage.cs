using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfazCalculateDamage : MonoBehaviour
{
    [SerializeField] private SetString damageUI;
    [SerializeField] private GameObject desactivable;
    public void UpdateData(int damage)
    {
        if (desactivable.activeSelf == false) desactivable.SetActive(true);
        damageUI.UpdateData(damage.ToString());
    }

    public void StopShow()
    {
        desactivable.SetActive(false);
    }
}
