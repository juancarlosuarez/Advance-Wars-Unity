using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTransparencyStartButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textStart;
    private float currentValue;
    private bool isAdd;
    private void Start()
    {
        currentValue = 1;
        isAdd = false;
    }

    private void Update()
    {
        if (isAdd) currentValue += 0.003f;
        else currentValue -= 0.003f;

        if (currentValue <= 0 || currentValue >= 1) isAdd = !isAdd;
        
        var colorTextStart = textStart.color;
        colorTextStart.a = currentValue;
        textStart.color = colorTextStart;
    }
}
