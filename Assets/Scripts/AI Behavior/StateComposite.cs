using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public abstract class StateComposite : StateNode
{
    public List<StateCondition> conditions;
    public List<StateNode> nodes;
    [HideInInspector] public StateNode currentNode;

    public override void InitializeNode(NonPlayerCharacter character)
    {
        base.InitializeNode(character);
        InitializeChildrenNodes();
    }

    protected virtual void InitializeChildrenNodes()
    {
        foreach (StateNode node in nodes)
        {
            node.parentNode = this;
            node.InitializeNode(nonPlayerCharacter);
        }
        foreach(StateNode node in conditions)
        {
            node.InitializeNode(nonPlayerCharacter);
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
        if (!CheckConditions())
        {
            parentNode.Fail(this);
            return;
        }
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
        if(conditions.Count == 0) return true;
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
        currentNode.OnExit();
        currentNode = null;
    }

    public void InstantiateSubNodes()
    {
        List<StateNode> newNodes = new List<StateNode>();
        foreach(StateNode node in nodes)
        {
            StateNode newNode = Instantiate(node);
            if(newNode is StateComposite composite)
            {
                composite.InstantiateSubNodes();
            }
            newNodes.Add(newNode);
        }
        nodes = newNodes;
    }
}
