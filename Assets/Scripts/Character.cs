using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    [Header("Character Components")]
    [SerializeField] protected Weapon weapon = null;
    [SerializeField] protected Transform characterGraphics = null;
    [SerializeField] protected Transform arm = null;
    [SerializeField] protected Transform weaponPoint = null;
    [SerializeField] protected string teamName;

    [Space]
    [Header("Characrter Properties")]
    private float health = 100;
    public float Health { get { return health; } }
    public bool invert;

    [Space]
    [Header("Character Stats")]
    public CharacterStats stats;

    protected virtual void Update()
    {
        if(weapon)
        {
            if (Input.GetButtonDown("Fire1") && weapon.GetData().type == WeaponType.SEMIAUTO) weapon.Fire();
            else if (Input.GetButton("Fire1") && weapon.GetData().type == WeaponType.AUTO) weapon.Fire();
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
        if (health <= 0) Die();
    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
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

    public void SetTeam(string teamName)
    {
        this.teamName = teamName;
    }
}

[System.Serializable]
public struct CharacterStats
{
    public int kills;
    public int assists;
}
