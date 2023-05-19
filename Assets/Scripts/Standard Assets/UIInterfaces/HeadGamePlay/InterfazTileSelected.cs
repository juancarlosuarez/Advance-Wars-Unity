using System;
using System.Collections;
using System.Collections.Generic;
using Extensibles;
using UnityEngine;
using UnityEngine.UI;
public class InterfazTileSelected : MonoBehaviour
{
    [SerializeField] private SetTileUI setTile;
    [SerializeField] private SetString nameTile;
    [SerializeField] private NumberSet ammountArmor;

    [SerializeField] private GameObject desactivable;


    
    public void UpdateData()
    {
        var tileWhereMySelectorIs = WorldScriptableObjects.GetInstance().tileSelected.reference;
        
        setTile.UpdateData(tileWhereMySelectorIs);
        nameTile.UpdateData(tileWhereMySelectorIs.dataVariable.typeTile.ToString());
        ammountArmor.UpdateData(tileWhereMySelectorIs.dataVariable.terrainDefense);
    }
    public void StopShowMenu() => MoveMenu(-1);
    public void ShowMenu() => MoveMenu(1);
    private async void MoveMenu(int offset)
    {
        Vector2 positionElement = transform.position;
        float x = positionElement.x;
        float y = positionElement.y;
        Vector2 destination = new Vector2(x, y + (5 * offset));
        //await transform.MoveElement(destination);
        DisableMenu();
    }
    private void DisableMenu()
    {
        if (desactivable.activeInHierarchy)
        {
            desactivable.SetActive(false);
            return;
        }
        desactivable.SetActive(true);
    }
}
