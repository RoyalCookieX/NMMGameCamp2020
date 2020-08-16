using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class StateComposite : StateNode
{
    public List<StateCondition> conditions;
    public List<StateNode> nodes;
    public StateNode currentNode;

    public override void InitializeNode(StateMachine character){
        base.InitializeNode(character);
        InitializeChildrenNodes();
        SetCurrentNode(nodes[0]);
    }

    protected virtual void InitializeChildrenNodes()
    {
        foreach (StateNode node in nodes)
        {
            node.parentNode = this;
            node.InitializeNode(character);
        }
    }

    public override void OnEnter(){
        if(CheckConditions()){
            SetCurrentNode(nodes[0]);
        }
        else{
            parentNode.Fail(this);
        }
    }

    public override void Tick()
    {
        if (currentNode != null)
        {
            currentNode.Tick();
        }
        // else
        // {
        //     InitializeChildrenNodes();
        //     currentNode = nodes[0];
        // }
    }

    public abstract void Success(StateNode finishedNode);

    public abstract void Fail(StateComposite finishedNode);

    protected virtual void SetCurrentNode(StateNode newNode){
        if(currentNode) currentNode.OnExit();
        currentNode = newNode;
        currentNode.OnEnter();
    }

    private bool CheckConditions(){
        foreach(StateCondition condition in conditions){
            if(condition.CheckCondition() == false){
                return false;
            }
        }
        return true;
    }

    public virtual void Reset(){
        currentNode.OnExit();
        currentNode = null;
    }
}
