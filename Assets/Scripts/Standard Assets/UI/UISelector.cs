using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UISelector : MonoBehaviour
{
    //borrrar oenstuhoeashutraoenshutnhaozemutnshaoetnu
    public static event Action<Tile> SetUISelector;
    public static UISelector sharedInstance;
    private void Awake()
    {
        sharedInstance = this;
    }
    public void SetUIForSelect(Tile tileSelect)
    {
        SetUISelector?.Invoke(tileSelect);
    }

}
