using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OptionStartSelect
{
    private int idOption;
    public OptionStartSelect(int _idOption)
    {
        idOption = _idOption;
    }
    public IStartOption GetOption()
    {
        switch (idOption)
        {
            case 0:
                return new OptionSaveMenuEdit();
            case 1:
                return new ResetOptionMenuEdit();
            case 4:
                return new EndOptionMenuEdit();
            case 5:
                return new LoadOptionMenuEdit();
            case 6:
                return new SaveOptionMenuEdit();
            case 7:
                return new MoveOptionUI();
            case 8:
                return new AttackOptionUI();
            case 10:
                return new LoadOptionUIUnits();
            case 11:
                return new DropOptionUIUnits();
            case 12:
                return new TakeOptionUI();
            case 13:
                return new OptionMenuGamePlay();
            case 14:
                return new OptionSaveMenu();
            case 15:
                return new EndOptionMenuEdit();
            case 16:
                return new DeleteOptionMenu();
            case 17:
                return new YieldOptionMenu();
            case 18:
                return new StopMusicOptionMenu();
            case 19:
                return new ExitMapOptionMenu();
            case 20:
                return new VersusOptionMenuUI();
            case 21:
                return new EditorOptionMenuUI();
            case 22:
                return new EditorOptionsStartMenuUI();
            case 23:
                return new ExitOptionMenu();
            default: 
                return new NullOptionStart();
        }
    }
    public class NullOptionStart : IStartOption
    {
        public void Trigger()
        {
            Debug.Log("option no encontrada o programada");
        }
    }
}
    public interface IStartOption
    {
        public void Trigger();
    }
