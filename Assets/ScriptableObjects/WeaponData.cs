using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "New WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float fireRate;
    public int ammo;
    public Projectile projectile;
}
