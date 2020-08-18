﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State Nodes/Conditions/Has Target")]
public class StateConditionHasTarget : StateCondition
{
    public override bool CheckCondition()
    {
        if (stateMachine.nonPlayerCharacter.target)
        {
            return true;
        }
        return false;
    }
}
