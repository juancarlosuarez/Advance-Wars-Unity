using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOptionUI : IStartOption
{
    public void Trigger()
    {
        CommandQueue.GetInstance.AddCommand(new CommandTaskMove(), true);
    }
}

public class AttackOptionUI : IStartOption
{
    public void Trigger()
    {
        CommandQueue.GetInstance.AddCommand(new CommandAttackOption(), false);
    }
}

public class TakeOptionUI : IStartOption
{
    public void Trigger()
    {
        CommandQueue.GetInstance.AddCommand(new CommandTaskMove(), true);
        CommandQueue.GetInstance.AddCommand(new CommandTake(), false);
    }
}
public class LoadOptionUIUnits : IStartOption
{
    public void Trigger()
    {
        CommandQueue.GetInstance.AddCommand(new CommandTaskMoveSimple(), true);
        CommandQueue.GetInstance.AddCommand(new CommandLoadOption(), false);
    }
}

public class DropOptionUIUnits : IStartOption
{
    public void Trigger()
    {
        CommandQueue.GetInstance.AddCommand(new CommandDropUnitMenu(), false);
    }
}
