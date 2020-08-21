using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateNode : ScriptableObject
{
    [HideInInspector] public NonPlayerCharacter nonPlayerCharacter;

    [HideInInspector] public StateComposite parentNode;

    public virtual void InitializeNode(NonPlayerCharacter nonPlayerCharacter)
    {
        this.nonPlayerCharacter = nonPlayerCharacter;
    }

    public virtual void OnEnter()
    {

    }

    public abstract void Tick();

    public virtual void OnExit()
    {

    }

    //public abstract void InstantiateSubNodes();
}
