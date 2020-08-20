﻿using System.Collections;
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
    TeamData teamData;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint2D contact = collision.GetContact(i);
            Instantiate(Resources.Load<GameObject>("LaserHitParticle"), contact.point, transform.rotation);
        }

        if (collision.transform.TryGetComponent(out IDamageable damageable))
        {
            if(collision.transform.TryGetComponent(out Character character))
            {
                if (character.teamData.teamName == teamData.teamName) 
                {
                    // gameObject.SetActive(false);
                    return;
                };
            }
            damageable.TakeDamage(damage);
        }
        gameObject.SetActive(false);
    }

    public void OnSpawnObject(params object[] args)
    {
        curLifetime = maxLifetime;
        rb.velocity = transform.right * speed;

        teamData = ((Weapon)args[0]).teamData;
        spriteRenderer.color = teamData.teamColor;
    }
}