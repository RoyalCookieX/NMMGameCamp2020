using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateComposite : StateNode
{
    public abstract void Success(StateNode finishedNode);

    public abstract void Fail(StateNode finishedNode);
}
