using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Behaviors/Medium Rifle Behavior")]
public class WeaponBehaviorMediumRifle : WeaponBehavior
{
    public void InitializeNode(NonPlayerCharacter nonPlayerCharacter)
    {
        weapon = nonPlayerCharacter.weapon;
    }

    //return true if actions are finished, else false

    public override WeaponBehaviorStatus Attack()
    {
        if (weapon.CurrentAmmo == 0) weapon.Reload();
        if (weapon.CurrentCooldown <= 0)
        {
            weapon.Fire();
            return WeaponBehaviorStatus.Success;
        }
        return WeaponBehaviorStatus.Doing;
    }
}
