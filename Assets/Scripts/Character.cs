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
    [SerializeField] protected LayerMask weaponMask;
    [SerializeField] public Team CharTeam { get; protected set; }

    [Space]
    [Header("Characrter Properties")]
    private float health = 100;
    public float Health { get { return health; } }
    public bool invert;
    public bool IsSpawning { get; private set; } = false;
    public float SpawnCooldown { get; private set; } = 0;

    private void OnEnable()
    {
        IsSpawning = false;
        health = 100;
    }

    private void Update()
    {
        if (SpawnCooldown > 0) SpawnCooldown -= Time.deltaTime;
        else SpawnCooldown = 0;
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
        HitParticle hitParticle = Instantiate(Resources.Load<HitParticle>("HitParticle"), transform.position + Random.insideUnitSphere * .7f, Quaternion.identity);
        hitParticle.SetText(damage);
        hitParticle.SetColor(Color.red);
        if (health <= 0) Die();
    }

    public virtual void Die()
    {
        DropWeapon();
        SpawnCooldown = 3;
        gameObject.SetActive(false);
        IsSpawning = true;
    }

    public virtual void EquipWeapon(Weapon weapon)
    {
        if (this.weapon) return;
        this.weapon = weapon;
        weapon.teamData = CharTeam.teamData;

        if(weapon.transform.TryGetComponent(out Rigidbody2D rb))
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }
        if(weapon.transform.TryGetComponent(out Collider2D col))
        {
            col.enabled = false;
        }

        weapon.transform.parent = weaponPoint;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        weapon.transform.localScale = Vector3.one;
    }

    public virtual void DropWeapon()
    {
        if (!weapon) return;

        weapon.transform.parent = null;
        weapon.transform.position = transform.position - Vector3.up;
        weapon.transform.rotation = Quaternion.identity;
        weapon.transform.localScale = Vector3.one;

        if(weapon.transform.TryGetComponent(out Rigidbody2D rb))
        {
            rb.isKinematic = false;
        }
        if (weapon.transform.TryGetComponent(out Collider2D col))
        {
            col.enabled = true;
        }

        weapon.teamData = null;
        weapon = null;
    }
}
