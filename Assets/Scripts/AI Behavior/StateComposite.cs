using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateComposite : StateNode
{
    public List<StateCondition> conditions;
    public List<StateNode> nodes;
    [HideInInspector] public StateNode currentNode;

    public override void InitializeNode(StateMachine character)
    {
        base.InitializeNode(character);
        InitializeChildrenNodes();
    }

    protected virtual void InitializeChildrenNodes()
    {
        foreach (StateNode node in nodes)
        {
            node.parentNode = this;
            node.InitializeNode(stateMachine);
        }
        foreach(StateNode node in conditions)
        {
            node.InitializeNode(stateMachine);
        }
    }

    public override void OnEnter()
    {
        if (CheckConditions())
        {
            SetCurrentNode(nodes[0]);
        }
        else
        {
            parentNode.Fail(this);
        }
    }

    public override void Tick()
    {
        if (currentNode != null)
        {
            currentNode.Tick();
        }
        else
        {
            Debug.LogError("No Current Node Selected");
        }
    }

    public abstract void Success(StateNode finishedNode);

    public abstract void Fail(StateNode finishedNode);

    protected virtual void SetCurrentNode(StateNode newNode)
    {
        if (currentNode) currentNode.OnExit();
        currentNode = newNode;
        currentNode.OnEnter();
    }

    private bool CheckConditions()
    {
        foreach (StateCondition condition in conditions)
        {
            if (condition.CheckCondition() == false)
            {
                return false;
            }
        }
        return true;
    }

    public virtual void ResetNode()
    {
        Debug.Log("Reset Running");
        currentNode.OnExit();
        currentNode = null;
    }
}
