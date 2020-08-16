using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State Nodes/Condition/ToggleChecker")]
public class TestConditionToggleChecker : StateCondition
{
    public override void Tick()
    {

    }

    public override bool CheckCondition()
    {
        if (stateMachine.toggleTest)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
