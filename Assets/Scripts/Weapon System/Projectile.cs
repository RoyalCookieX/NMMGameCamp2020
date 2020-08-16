using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolObject
{
    [Header("Projectile Components")]
    [SerializeField] Rigidbody2D rb = null;
    [SerializeField] SpriteRenderer spriteRenderer;

    [Space]
    [Header("Projectile Properties")]
    [SerializeField] float damage = 0;
    [SerializeField] float maxLifetime = 1;
    [SerializeField] float speed = 10;
    float curLifetime = 1;

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

    public void OnSpawnObject(object spawnedBy)
    {
        curLifetime = maxLifetime;
        rb.velocity = transform.right * speed;

        spriteRenderer.color = ((Weapon)spawnedBy).TeamColor;
    }
}
