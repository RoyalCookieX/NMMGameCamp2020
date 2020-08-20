using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHitParticle : MonoBehaviour
{
    [SerializeField] Animator anim;

    private void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsTag("destroy"))
        {
            Destroy(gameObject);
        }
    }
}
