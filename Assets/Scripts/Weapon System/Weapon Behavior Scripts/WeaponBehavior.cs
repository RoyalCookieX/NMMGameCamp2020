using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehavior : StateNode
{
    protected Weapon weapon;

    public virtual void InitializeWeaponBehavior(NonPlayerCharacter nonPlayerCharacter)
    {
        this.nonPlayerCharacter = nonPlayerCharacter;
        weapon = nonPlayerCharacter.weapon;
    }

    protected abstract void Attack();
}
