using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "WeaponData", menuName = "New WeaponData")]
public class WeaponData : ScriptableObject
{
    public WeaponType type;
    public WeaponBehavior weaponBehavior;

    public string weaponName;
    public float minAttackRange;      //used for ai distances. minimum distance ai must be from target to attack.
    public float maxAttackRange;      //used for ai distances. maximum distance ai must be from target to attack.
    public float fireRate;
    public float reloadDuration;
    public int ammo;
    public Projectile projectile;
}

public enum WeaponType
{
    AUTO, SEMIAUTO
}
