using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] Camera cam; //Temporary

    protected override void Update()
    {
        Vector2 armCenter = cam.WorldToScreenPoint(arm.transform.position);
        float angle = Vector2.SignedAngle(Vector2.right, (Vector2)Input.mousePosition - armCenter);
        SetArmAngle(angle);
        base.Update();
    }
}
