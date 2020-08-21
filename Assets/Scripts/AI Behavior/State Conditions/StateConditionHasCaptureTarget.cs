using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State Nodes/Conditions/Has Capture Target")]
public class StateConditionHasCaptureTarget : StateCondition
{
    //checks if capturetarget is a valid capturetarget
    public override bool CheckCondition()
    {
        if (nonPlayerCharacter.captureTarget && nonPlayerCharacter.captureTarget.teamData != nonPlayerCharacter.teamData)
        {
            return true;
        }
        return false;
    }
}
