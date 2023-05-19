using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBSelect : MonoBehaviour
{
    private bool isButtonBPress = false;
    private void OnDisable()
    {
        GamePlayControls.PressButtonB -= ButtonB;
        GamePlayControls.StopPressButtonB -= StopPressButtonB;
    }

    private void OnEnable()
    {
        GamePlayControls.PressButtonB += ButtonB;
        GamePlayControls.StopPressButtonB += StopPressButtonB;
    }

    private void ButtonB()
    {
        if (WorldScriptableObjects.GetInstance().isDeleteOn.boolReference)
        {
            WorldScriptableObjects.GetInstance().isDeleteOn.boolReference = false;
            InterfacesManager.sharedInstance.OpenGamePlayInterfaces();
            return;
        }
        
        if (WorldScriptableObjects.GetInstance().hadPointerUnitSelected.boolReference) DeselectUnit();
        else ShowRangeAction();
    }

    private void StopPressButtonB()
    {
        StopShowRange();
    }
    private void DeselectUnit()
    {
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.Exit);
        CommandQueue.GetInstance.AddCommand(new DeselectUnitUICommand(),false);
        CommandQueue.GetInstance.AddCommand(new DeselectUnitCommand(),false);
        CommandQueue.GetInstance.AddCommand(new ActiveSelectCommand(), false);
    }

    private void ShowRangeAction()
    {
        var tileSelected = WorldScriptableObjects.GetInstance().tileSelected.reference;
        if (!tileSelected.occupiedSoldier) return;
        
        SoundManager._sharedInstance.PlayEffectSound(EffectNames.SelectUnit);
        WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference = tileSelected; 
        isButtonBPress = true;
        CommandQueue.GetInstance.AddCommand(new CommandDisplayRangeActionUnit(), false);
    }

    private void StopShowRange()
    {
        if (!isButtonBPress) return;
        WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference = null;
        isButtonBPress = false;
        CommandQueue.GetInstance.AddCommand(new ResetHighLightTilesCommand(WorldScriptableObjects.GetInstance().tilesRangeAction.reference), false);
    }
}
