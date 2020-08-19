using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Behaviors/Medium Rifle Behavior")]
public class WeaponBehaviorMediumRifle : WeaponBehavior
{
    public override void InitializeNode(NonPlayerCharacter nonPlayerCharacter)
    {
        base.InitializeNode(nonPlayerCharacter);
        weapon = nonPlayerCharacter.weapon;
    }
    public override void Tick()
    {
        float angle = Vector2.SignedAngle(Vector2.right, nonPlayerCharacter.target.transform.position - nonPlayerCharacter.transform.position);
        nonPlayerCharacter.SetArmTemp(angle);
    }

    protected override void Attack()
    {
        if (weapon.CurrentAmmo == 0) weapon.Reload();
        if (weapon.CurrentCooldown <= 0)
        {
            weapon.Fire();
        }
    }
}
