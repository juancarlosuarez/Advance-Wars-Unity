using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelAction : ActionUnit
{
    public override void Trigger()
    {
        print("cancel");
        StateGamePlay.GetInstance().ChangeState(GameState.GamePlayActionable);
    }
}
