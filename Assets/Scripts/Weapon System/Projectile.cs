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
    [SerializeField] int penetration;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.TryGetComponent(out IDamageable damageable))
        {
            if(other.transform.TryGetComponent(out Character character))
            {
                print(character);
                print(character.teamData);
                if (character.teamData.teamName == null)
                {
                    print("No team name! Proj Name: " + gameObject.name);
                }
                print(character.teamData.teamName);
                print(teamData);
                if (character.teamData.teamName == teamData.teamName) 
                {
                    // gameObject.SetActive(false);
                    return;
                };
                GameObject hitParticle = Instantiate(Resources.Load<GameObject>("LaserHitParticle"), transform.position, Quaternion.identity);
            }
            damageable.TakeDamage(damage);
            if (penetration == 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                penetration -= 1;
            }
        }
    }

    public void OnSpawnObject(params object[] args)
    {
        curLifetime = maxLifetime;
        rb.velocity = transform.right * speed;

        teamData = ((Weapon)args[0]).teamData;
        spriteRenderer.color = teamData.teamColor;
    }
}