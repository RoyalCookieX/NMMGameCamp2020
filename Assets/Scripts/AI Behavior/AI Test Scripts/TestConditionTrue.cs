using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State Nodes/Condition/True")]
public class TestConditionTrue : StateCondition
{
    public override void Tick()
    {
        CheckCondition();
    }

    public override bool CheckCondition()
    {
        return true;
    }
}
