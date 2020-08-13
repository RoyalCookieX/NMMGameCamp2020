using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateNode : ScriptableObject
{
    public Character character;

    public virtual void InitializeNode(Character character){
        this.character = character;
    }

    public virtual void OnEnter(){

    }

    public abstract void Tick();

    public virtual void OnExit(){

    }
}
