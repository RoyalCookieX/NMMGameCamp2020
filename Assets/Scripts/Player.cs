using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] Camera cam; //Temporary

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
}
