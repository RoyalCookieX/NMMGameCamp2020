using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Behaviors/Medium Rifle Behavior")]
public class WeaponBehaviorMediumRifle : WeaponBehavior
{

    //return true if actions are finished, else false

    public override WeaponBehaviorStatus Attack()
    {
        nonPlayerCharacter.SetArmAngle(nonPlayerCharacter.target.transform.position);
        bool isInRange = MoveToRange();
        if (weapon.CurrentAmmo == 0) weapon.Reload();
        if (weapon.CurrentCooldown <= 0 && isInRange)
        {
            //nonPlayerCharacter.SetArmAngle(nonPlayerCharacter.target.transform.position);
            weapon.Fire();
            return WeaponBehaviorStatus.Success;
        }
        return WeaponBehaviorStatus.Doing;
    }

    public override WeaponBehaviorStatus Idle()
    {
        return WeaponBehaviorStatus.Success;
    }

    // public override void Movement(){
    //     Vector2 targetPos = nonPlayerCharacter.target.transform.position;
    //     if()
    // }
}
