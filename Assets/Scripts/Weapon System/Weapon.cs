using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Queue<GameObject> pool;
    public Queue<GameObject> Pool { get { return pool; } }
    public int Size { get; set; } = 10;
    public TeamData teamData;

    public WeaponData weaponData;
    [SerializeField] protected Transform firePoint;

    [HideInInspector] public float decayTimer;
    public float inaccuracy;
    public float CurrentCooldown { get; private set; }
    public int CurrentAmmo { get; private set; }

    [Space]
    [SerializeField] AudioObjectSettings audioSettings;

    private void Awake()
    {
        decayTimer = 5f;
    }

    protected virtual void Start()
    {
        pool = new Queue<GameObject>(Size);
        for (int i = 0; i < Size; i++)
        {
            GameObject obj = Instantiate(weaponData.projectile.gameObject);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
        CurrentAmmo = weaponData.ammo;
    }

    protected virtual void Update()
    {
        if (gameObject.activeInHierarchy && CurrentCooldown > 0)
        {
            CurrentCooldown -= Time.deltaTime;
        }
        else
        {
            CurrentCooldown = 0;
        }
        if (!transform.parent) decayTimer -= Time.deltaTime;
        if (decayTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        DestroyPool();
    }

    public virtual void Fire()
    {
        if (CurrentCooldown <= 0 && CurrentAmmo > 0)
        {
            CurrentAmmo--;
            CurrentCooldown = 1f / weaponData.fireRate;
            GetFromPool(firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, (Random.value * 2 - 1) * inaccuracy));

            AudioManager.Instance.PlayRandom("laser", audioSettings, transform.position, transform.rotation);
        }
    }

    GameObject GetFromPool(Vector3 position, Quaternion rotation)
    {
        GameObject obj = pool.Dequeue();

        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        if (obj.TryGetComponent(out IPoolObject poolObject))
        {
            poolObject.OnSpawnObject(this);
        }

        pool.Enqueue(obj);
        return obj;
    }

    public void DestroyPool()
    {
        int poolSize = pool.Count;
        for (int i = 0; i < poolSize; i++)
        {
            Destroy(pool.Dequeue());
        }
    }

    public void Reload()
    {
        CurrentCooldown = weaponData.reloadDuration;
        CurrentAmmo = weaponData.ammo;
    }

    public WeaponData GetWeaponData() { return weaponData; }

    public void InstantiateWeaponBehavior()
    {
        weaponData = Instantiate(weaponData);
        weaponData.weaponBehavior = Instantiate(weaponData.weaponBehavior);
    }
}
