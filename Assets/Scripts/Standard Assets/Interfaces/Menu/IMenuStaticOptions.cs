using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface IMenuStaticOptions 
{
    //Esta interfaz exigue una lista, si no esta creada la lista de objecto o variables, pues simplemente esta estructura no tiene sentido.
    //Tambien es necesario una lista de UIs para poder activar el highlight

    //Scripts que debo refactorizar Option, ObjectiveAttack y Action
    //public void ShowOptions<T>(List<T> optionsAvailables);
    public int GetCountList();
    public bool CanSelectOption(int count);
    public void HighLight(int countData, int countUI);
    public void LowLight(int countData, int countUI);
    public void Trigger(int count);
}

public interface IPlainOptions
{
    public void DisplayMenu();
    public void CloseMenuByPressB();
    public void CloseMenuByTriggerButton();

}
