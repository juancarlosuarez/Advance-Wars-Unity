using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsEditMap : MonoBehaviour
{
    private void OnEnable()
    {
        EditMapControllers.PressButtonL1 += OpenMenuTiles;
        EditMapControllers.PressButtonR1 += OpenMenuUnits;
        EditMapControllers.PressButtonStartEditMap += OpenMenuStartEdit;

        EditMapControllers.PressButtonB += CloseEditorToGamePlay;

        // UIControls.PressButtonL1 += MenuUnitWhileIsTileOpen;
        // UIControls.PressButtonR1 += MenuTileWhileIsUnitOpen;
        //
        //
        // PlayerController.ResetEventsNoMonobehavior += ResetEvents;
    }

    private void OpenMenuTiles()
    {
           
    }
    private void OpenMenuUnits()
    {
        
    }

    private void OpenMenuStartEdit()
    {
        
    }

    private void CloseEditorToGamePlay()
    {
        
    }
    private void OnDisable()
    {
        
        
    }
}
