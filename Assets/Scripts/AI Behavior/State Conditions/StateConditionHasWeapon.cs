using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State Nodes/Conditions/Has Weapon")]
public class StateConditionHasWeapon : StateCondition
{
    public override bool CheckCondition()
    {
        if (stateMachine.nonPlayerCharacter.weapon)
        {
            return true;
        }
        return false;
    }
}
