using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateNode : ScriptableObject
{
<<<<<<< HEAD
    [HideInInspector] public StateMachine stateMachine;

    [HideInInspector] public StateComposite parentNode;

    public virtual void InitializeNode(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void OnEnter()
    {
=======
    public Character character;

    public virtual void InitializeNode(Character character){
        this.character = character;
    }

    public virtual void OnEnter(){
>>>>>>> Dev

    }

    public abstract void Tick();

<<<<<<< HEAD
    public virtual void OnExit()
    {
=======
    public virtual void OnExit(){
>>>>>>> Dev

    }
}
