using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateAction : StateNode
{
    public virtual void Success(){
        parentNode.Success(this);
    }

    public virtual void Fail(){
        parentNode.Fail(this);
    }
}