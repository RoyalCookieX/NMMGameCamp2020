using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] Camera cam; //Temporary
    [SerializeField] Rect healthRect;
    [SerializeField] Rect ammoRect;
    [SerializeField] Rect cooldownRect;
    [SerializeField] Gradient gradient;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        Vector2 armCenter = cam.WorldToScreenPoint(arm.transform.position);
        float angle = Vector2.SignedAngle(Vector2.right, (Vector2)Input.mousePosition - armCenter);
        SetArmAngle(angle);

        if (weapon)
        {
            if (Input.GetButtonDown("Fire1") && weapon.GetData().type == WeaponType.SEMIAUTO) weapon.Fire();
            else if (Input.GetButton("Fire1") && weapon.GetData().type == WeaponType.AUTO) weapon.Fire();
            if (Input.GetButtonDown("Fire2")) weapon.Reload();
            if (Input.GetKeyDown(KeyCode.Space)) DropWeapon();
        }
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
