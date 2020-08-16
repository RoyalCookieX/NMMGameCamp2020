using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateNode : ScriptableObject
{
    public StateMachine character;

    public StateComposite parentNode;

    public virtual void InitializeNode(StateMachine character)
    {
        this.character = character;
    }

    public virtual void OnEnter()
    {

    }

    public abstract void Tick();

    public virtual void OnExit()
    {

    }
}
