using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State Nodes/Conditions/Weapon Reloading")]
public class StateConditionWeaponReloading : StateCondition
{
    public override bool CheckCondition()
    {
        if (nonPlayerCharacter.weapon.CurrentCooldown > 0)
        {
            return true;
        }
        return false;
    }
}
