using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NumberSet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textPro;
    public void UpdateData(int number)
    {
        textPro.SetText(number.ToString());
    }
}
