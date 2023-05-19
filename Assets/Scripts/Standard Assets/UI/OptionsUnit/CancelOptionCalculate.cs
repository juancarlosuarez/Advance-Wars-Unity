using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelOptionCalculate : IOptionsUnitConditions
{
    public OptionsUnit GetOptionAssociated() => OptionsUnit.Cancel;
    public bool DoesOptionMeetCondition() => true;
}
