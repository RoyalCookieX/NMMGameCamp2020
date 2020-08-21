using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponBehaviorStatus { Success, Doing, Fail }

public abstract class WeaponBehavior : ScriptableObject
{
    public NonPlayerCharacter nonPlayerCharacter;
    protected Weapon weapon;

    public virtual void InitializeWeaponBehavior(NonPlayerCharacter nonPlayerCharacter)
    {
        this.nonPlayerCharacter = nonPlayerCharacter;
        weapon = nonPlayerCharacter.weapon;
    }

    //make ai move toward/away from target to be within distance parameters. returns true if within acceptable range
    public virtual bool MoveToRange()
    {
        Vector2 pos = nonPlayerCharacter.transform.position;
        Vector2 targetPos = nonPlayerCharacter.target.transform.position;
        Vector2 dir = targetPos - pos;
        float distance = Vector2.Distance(pos, targetPos);
        if (distance < weapon.weaponData.minAttackRange)
        {
            nonPlayerCharacter.transform.Translate(-dir.normalized * nonPlayerCharacter.movementSpeed*Time.deltaTime);
            return false;
        }
        if (distance > weapon.weaponData.maxAttackRange)
        {
            nonPlayerCharacter.transform.Translate(dir.normalized * nonPlayerCharacter.movementSpeed*Time.deltaTime);
            return false;
        }
        return true;
    }

    public abstract WeaponBehaviorStatus Attack();

    public abstract WeaponBehaviorStatus Idle();
}
