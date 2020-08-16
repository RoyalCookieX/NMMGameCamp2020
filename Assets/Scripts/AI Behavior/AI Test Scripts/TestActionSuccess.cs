using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State Nodes/Actions/Success")]
public class TestActionSuccess : StateAction
{
    public override void Tick()
    {
        Debug.Log("Action Success");
        parentNode.Success(this);
    }
}
