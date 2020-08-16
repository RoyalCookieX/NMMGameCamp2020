using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State Nodes/Composites/Root")]
public class StateRoot : StateComposite
{
    public override void InitializeNode(StateMachine character)
    {
        base.InitializeNode(character);
        SetCurrentNode(nodes[0]);
    }

    public override void Success(StateNode finishedNode)
    {
        SetCurrentNode(nodes[0]);
    }

    public override void Fail(StateNode finishedNode)
    {
        if (nodes[nodes.Count - 1] == finishedNode)
        {
            SetCurrentNode(nodes[0]);
        }
        else
        {
            SetCurrentNode(nodes[nodes.IndexOf(finishedNode) + 1]);
        }
    }
}
