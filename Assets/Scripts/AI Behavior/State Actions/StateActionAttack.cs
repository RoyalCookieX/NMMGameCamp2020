using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(menuName="State Nodes/Actions/Attack")]
public class StateActionAttack : StateAction
{
    public override void Tick()
    {
        WeaponBehaviorStatus status = nonPlayerCharacter.weapon.weaponData.weaponBehavior.Attack();
        if (status == WeaponBehaviorStatus.Success)
        {
            parentNode.Success(this);
        }
        else if(status == WeaponBehaviorStatus.Fail){
            parentNode.Fail(this);
        }
    }
}
