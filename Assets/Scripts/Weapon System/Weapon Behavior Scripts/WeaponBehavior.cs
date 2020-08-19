using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponBehaviorStatus { Success, Doing, Fail }

public abstract class WeaponBehavior : ScriptableObject
{
    NonPlayerCharacter nonPlayerCharacter;
    protected Weapon weapon;

    public virtual void InitializeWeaponBehavior(NonPlayerCharacter nonPlayerCharacter)
    {
        this.nonPlayerCharacter = nonPlayerCharacter;
        weapon = nonPlayerCharacter.weapon;
    }

    public abstract WeaponBehaviorStatus Attack();
}
