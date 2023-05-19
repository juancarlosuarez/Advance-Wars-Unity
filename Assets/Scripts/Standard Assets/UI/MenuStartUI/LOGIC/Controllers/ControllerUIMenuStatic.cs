using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ControllerUIMenuStatic : ControllerUIStartParent, IMenuStaticOptions
{
    public virtual void HighLight(int count, int countUI) =>  print("No implemented HighLight");
    public virtual void LowLight(int count, int countUI) => print("No implemented LowHighLight");
    public virtual void Trigger(int count) => print("No implemented Trigger");
    public virtual int GetCountList() => throw new System.NotImplementedException();
    public virtual bool CanSelectOption(int count) => throw new System.NotImplementedException();
    protected IStartOption OptionSelect(int idOption) => new OptionStartSelect(idOption).GetOption();
}

public class ControllerUIMenuOptionsSlider : ControllerUIStartParent, ISliderMenuOptions
{
    public virtual void MoveElementsInTheSelector(bool isMovementRight) => print("No implemented MoveElementsSelector");
    public virtual void Trigger() => print("No implemented Trigger");
    public virtual void ChangeElementsShowed(bool isMovementUp) => print("No implement ChangeElement");
}
public class ControllerUIStartParent : SerializedMonoBehaviour, IPlainOptions
{
    public ControllerMenuName controllerMenu;
    public NameElementWithFramesGeneric nameFrameGeneric;
    public virtual void DisplayMenu() => print("No implemented DisplayMenu");
    public virtual void CloseMenuByPressB() => print("No implemented CloseMenuByPressB");
    public virtual void CloseMenuByTriggerButton() => print("No implemented CloseMenuByTriggerButton");
}

