using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float damage = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
        }
    }
}
