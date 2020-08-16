using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Queue<GameObject> pool;
    public Queue<GameObject> Pool { get { return pool; } }
    public int Size { get; set; } = 10;
    public Color TeamColor { get; set; }

    [SerializeField] protected WeaponData data;
    [SerializeField] protected Transform firePoint;

    public float CurCooldown { get; private set; }
    public int CurAmmo { get; private set; }

    protected virtual void Start()
    {
        pool = new Queue<GameObject>(Size);
        for(int i = 0; i < Size; i++)
        {
            GameObject obj = Instantiate(data.projectile.gameObject);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
        CurAmmo = data.ammo;
    }

    protected virtual void Update()
    {
        if(gameObject.activeInHierarchy && CurCooldown > 0)
        {
            CurCooldown -= Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        DestroyPool();
    }

    public virtual void Fire()
    {
        if (CurCooldown <= 0 && CurAmmo > 0)
        {
            CurAmmo--;
            CurCooldown = 1f / data.fireRate;
            GetFromPool(firePoint.position, firePoint.rotation);
        }
    }

    public GameObject GetFromPool(Vector3 position, Quaternion rotation)
    {
        GameObject obj = pool.Dequeue();

        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        if(obj.TryGetComponent(out IPoolObject poolObject))
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
        CurAmmo = data.ammo;
    }

    public WeaponData GetData() { return data; }
}
