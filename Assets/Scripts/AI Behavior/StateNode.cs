using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateNode : ScriptableObject
{
    [HideInInspector] public StateMachine stateMachine;

    [HideInInspector] public StateComposite parentNode;

    public virtual void InitializeNode(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void OnEnter()
    {

    }

    public abstract void Tick();

    public virtual void OnExit()
    {

    }
}
