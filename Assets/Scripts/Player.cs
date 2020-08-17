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

    private void Start()
    {
        cam = GameCamera.Instance.Cam;
    }

    void Update()
    {
        if(cam)
        {
            Vector2 armCenter = cam.WorldToScreenPoint(arm.transform.position);
            float angle = Vector2.SignedAngle(Vector2.right, (Vector2)Input.mousePosition - armCenter);
            SetArmAngle(angle);
        }

        if (weapon)
        {
            if (Input.GetButtonDown("Fire1") && weapon.GetData().type == WeaponType.SEMIAUTO) weapon.Fire();
            else if (Input.GetButton("Fire1") && weapon.GetData().type == WeaponType.AUTO) weapon.Fire();
            if (Input.GetButtonDown("Fire2")) weapon.Reload();
            if (Input.GetKeyDown(KeyCode.Space)) DropWeapon();
        }

        //From Playermov.cs
        //float transV = Input.GetAxis("Vertical") * playerSpeedVertical * Time.deltaTime;
        //float transH = Input.GetAxis("Horizontal") * playerSpeedHorizontal * Time.deltaTime;

        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        smoothSpeed = Mathf.Lerp(smoothSpeed, dir.magnitude > 0 ? playerSpeed : 0, Time.deltaTime * lerpSpeed);

        transform.Translate(dir * smoothSpeed * Time.deltaTime);
    }

    private void OnGUI()
    {
        GUI.color = gradient.Evaluate(1 - (Health / 100));
        GUI.Label(healthRect, $"Health: {Health}");
        if (!weapon) return;
        GUI.color = gradient.Evaluate(1 - ((float)weapon.CurAmmo / weapon.GetData().ammo));
        GUI.Label(ammoRect, $"Ammo: {weapon.CurAmmo}/{weapon.GetData().ammo}");
        GUI.color = gradient.Evaluate(weapon.CurCooldown * weapon.GetData().fireRate);
        GUI.Label(cooldownRect, $"Cooldown: {Mathf.Round(weapon.CurCooldown * weapon.GetData().fireRate * 100) / 100}");
    }
}
