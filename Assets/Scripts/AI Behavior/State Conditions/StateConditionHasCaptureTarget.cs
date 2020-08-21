using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State Nodes/Conditions/Has Capture Target")]
public class StateConditionHasCaptureTarget : StateCondition
{
    public override bool CheckCondition()
    {
        if (nonPlayerCharacter.captureTarget)
        {
            return true;
        }
        return false;
    }
}
