using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="State Nodes/Actions/SuccessTwo")]
public class TestActionSuccessTwo : StateAction
{
    public override void Tick()
    {
        Debug.Log("Action Success 2");
        parentNode.Success(this);
    }
}
