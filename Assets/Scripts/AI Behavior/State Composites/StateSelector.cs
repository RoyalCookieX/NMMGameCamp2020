using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State Nodes/Composites/Selector")]
public class StateSelector : StateComposite
{
    public override void Success(StateNode finishedNode)
    {
        //Reset();
        parentNode.Success(this);
    }

    public override void Fail(StateNode finishedNode)
    {
        if (nodes[nodes.Count - 1] == finishedNode)
        {
            //Reset();
            parentNode.Fail(this);
        }
        else
        {
            SetCurrentNode(nodes[nodes.IndexOf(finishedNode) + 1]);
        }
    }
}