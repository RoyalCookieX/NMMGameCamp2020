using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolObject
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float damage = 0;
    [SerializeField] float maxLifetime;
    float curLifetime;

    private void Update()
    {
        if (gameObject.activeInHierarchy && curLifetime > 0)
        {
            curLifetime -= Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }

    public void OnSpawnObject()
    {
        curLifetime = maxLifetime;
        rb.velocity = transform.right * 10;
    }
}
