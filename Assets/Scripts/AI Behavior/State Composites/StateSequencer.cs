using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State Nodes/Composites/Sequencer")]
public class StateSequencer : StateComposite
{
    public override void Success(StateNode finishedNode)
    {
        if (nodes[nodes.Count - 1] == finishedNode)
        {
            //Reset();
            parentNode.Success(this);
        }
        else
        {
            SetCurrentNode(nodes[nodes.IndexOf(finishedNode) + 1]);
        }
    }

    public override void Fail(StateNode finishedNode)
    {
        //Reset();
        parentNode.Fail(this);
    }
}
