using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateCondition : StateNode
{
    public override void Tick()
    {
        //dont need tick for stateconditions
    }

    public abstract bool CheckCondition();
}
