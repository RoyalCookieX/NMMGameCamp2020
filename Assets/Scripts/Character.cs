using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    [Header("Components")]
    [SerializeField] Weapon weapon;
    [SerializeField] Transform characterGraphics;
    [SerializeField] Transform arm;
    [SerializeField] Transform weaponPoint;
    [SerializeField] Camera cam; //Temperary

    [Space]
    [Header("Properties")]
    private float health = 100;
    public float Health { get { return health; } }
    public bool invert;

    private void Update()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        float angle = Vector2.SignedAngle(Vector2.right, (Vector2)Input.mousePosition - screenCenter);
        SetArmAngle(angle);
    }

    protected virtual void SetArmAngle(float angle)
    {
        arm.localEulerAngles = Vector3.forward * angle;
        characterGraphics.transform.localScale = new Vector3(Mathf.Abs(angle) < 90 ? !invert ? 1 : -1 : invert ? 1 : -1, 1, 1);
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
    }

    public virtual void EquipWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.transform.parent = weaponPoint;
    }
}
