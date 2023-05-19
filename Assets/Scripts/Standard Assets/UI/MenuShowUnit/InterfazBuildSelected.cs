using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfazBuildSelected : MonoBehaviour
{
    [SerializeField] private SetSprite sprite;
    [SerializeField] private SetString nameBuild;
    [SerializeField] private SetString life;

    [SerializeField] private GameObject desactivable;

    public void UpdateData()
    {
        var buildSelected = WorldScriptableObjects.GetInstance().tileSelected.reference.occupiedBuild;
        if (buildSelected == null)
        {
            DisableMenu();
            return;
        }
        sprite.UpdateData(buildSelected.spriteUI);
        nameBuild.UpdateData(buildSelected.nameUnit.ToString());
        life.UpdateData(buildSelected.currentLifeUI.ToString());
        
        desactivable.SetActive(true);
    }

    private void DisableMenu()
    {
        desactivable.SetActive(false);
    }
}
