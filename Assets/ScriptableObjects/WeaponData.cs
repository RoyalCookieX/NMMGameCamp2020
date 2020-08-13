using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "WeaponData", menuName = "New WeaponData")]
public class WeaponData : ScriptableObject
{
    public WeaponType type;

    public string weaponName;
    public float fireRate;
    public int ammo;
    public Projectile projectile;
}

public enum WeaponType
{
    AUTO, SEMIAUTO
}
