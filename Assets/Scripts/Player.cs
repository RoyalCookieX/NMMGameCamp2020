using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    Camera cam;

    [Header("Player Properties")]
    [SerializeField] Rect healthRect;
    [SerializeField] Rect ammoRect;
    [SerializeField] Rect cooldownRect;
    [SerializeField] Gradient gradient;

    [Space]
    [Header("Player Properties")]
    //From Playermov.cs
    //[SerializeField] float playerSpeedHorizontal = 10;
    //[SerializeField] float playerSpeedVertical = 10;
    [SerializeField] float playerSpeed = 5;
    [SerializeField] float lerpSpeed = 2;
    float smoothSpeed;
    [SerializeField] protected LayerMask weaponMask;

    [Space]
    [SerializeField] AudioObjectSettings audioSettings;

    private void Awake()
    {
        PlayerGUI.CurrentPlayer = this; 
    }

    private void Start()
    {
        cam = GameCamera.Instance.Cam;
    }

    void Update()
    {
        //arm movement
        if(cam)
        {
            Vector2 armCenter = cam.WorldToScreenPoint(arm.transform.position);
            float angle = Vector2.SignedAngle(Vector2.right, (Vector2)Input.mousePosition - armCenter);
            SetArmAngle(angle);
        }

        //equiping a weapon
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position, .75f, weaponMask);
            if (col)
            {
                if (col.transform.TryGetComponent(out Weapon weapon))
                {
                    if (this.weapon) DropWeapon();
                    if(weapon.teamData == null) EquipWeapon(weapon);
                }
            }
        }

        //shooting a weapon
        if (weapon)
        {
            if (Input.GetButtonDown("Fire1") && weapon.GetWeaponData().type == WeaponType.SEMIAUTO) weapon.Fire();
            else if (Input.GetButton("Fire1") && weapon.GetWeaponData().type == WeaponType.AUTO) weapon.Fire();
            if (Input.GetKeyDown(KeyCode.R)) weapon.Reload();
        }

        /*player movement
          --From Playermov.cs--
          float transV = Input.GetAxis("Vertical") * playerSpeedVertical * Time.deltaTime;
          float transH = Input.GetAxis("Horizontal") * playerSpeedHorizontal * Time.deltaTime;
        */

        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        smoothSpeed = Mathf.Lerp(smoothSpeed, dir.magnitude > 0 ? playerSpeed : 0, Time.deltaTime * lerpSpeed);

        if(!IsSpawning) transform.Translate(dir * smoothSpeed * Time.deltaTime);

        //animation
        anim.SetBool("isMoving", dir.magnitude > 0.15f);
    }

    public override void TakeDamage(float damage)
    {
        if (Health <= 0) return;
        base.TakeDamage(damage);
        anim.SetTrigger("damaged");
    }

    public override void Die()
    {
        AudioManager.Instance.ResetSounds();
        AudioManager.Instance.PlaySound("meow-death", audioSettings, transform.position, Quaternion.identity);
        IEnumerator DieEnumerator()
        {
            anim.SetTrigger("die");
            while (!anim.GetCurrentAnimatorStateInfo(0).IsTag("disable")) yield return null;
            base.Die();
        }
        StopAllCoroutines();
        StartCoroutine(DieEnumerator());
    }
}
