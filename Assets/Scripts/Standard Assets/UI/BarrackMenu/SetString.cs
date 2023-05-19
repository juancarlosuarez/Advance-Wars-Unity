using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]
public class SetString : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textPro;

    public void UpdateData(string stringToShow)
    {
        
        textPro.SetText(stringToShow);
    }
}
