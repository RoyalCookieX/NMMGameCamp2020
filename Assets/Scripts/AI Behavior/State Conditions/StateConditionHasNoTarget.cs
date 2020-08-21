using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="State Nodes/Conditions/Has No Target")]
public class StateConditionHasNoTarget : StateCondition
{
    public override bool CheckCondition()
    {
        if (!nonPlayerCharacter.target)
        {
            return true;
        }
        return false;
    }
}
