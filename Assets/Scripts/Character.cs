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
    [SerializeField] Camera cam; //Temporary

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

        if(weapon)
        {
            if (Input.GetButtonDown("Fire1")) weapon.Fire();
            if (Input.GetButtonDown("Fire2")) weapon.Reload();
            if (Input.GetKeyDown(KeyCode.Space)) DropWeapon();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.TryGetComponent(out Weapon weapon))
        {
            EquipWeapon(weapon);
        }
    }

    protected virtual void SetArmAngle(float angle)
    {
        arm.localEulerAngles = Vector3.forward * angle;
        characterGraphics.transform.localScale = new Vector3(Mathf.Abs(angle) < 90 ? !invert ? 1 : -1 : invert ? 1 : -1, 1, 1);
        weaponPoint.transform.localScale = new Vector3(1, Mathf.Abs(angle) < 90 ? !invert ? 1 : -1 : invert ? 1 : -1, 1);
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
    }

    public virtual void EquipWeapon(Weapon weapon)
    {
        this.weapon = weapon;

        if(weapon.transform.TryGetComponent(out Rigidbody2D rb))
        {
            rb.isKinematic = true;
        }

        weapon.transform.parent = weaponPoint;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        weapon.transform.localScale = Vector3.one;
    }

    public virtual void DropWeapon()
    {
        weapon.transform.parent = null;
        weapon.transform.position = transform.position - Vector3.up;
        weapon.transform.rotation = Quaternion.identity;
        weapon.transform.localScale = Vector3.one;

        if(weapon.transform.TryGetComponent(out Rigidbody2D rb))
        {
            rb.isKinematic = false;
        }

        weapon = null;
    }
}
