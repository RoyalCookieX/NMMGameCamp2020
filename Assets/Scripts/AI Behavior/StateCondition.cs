using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateCondition : StateNode
{
    public abstract bool CheckCondition();
}
