using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : Character
{
    public Character target;
    public Capturepoint captureTarget;
    public StateRoot rootNode;

    void Awake()
    {
        rootNode.InitializeNode(this);
    }

    private void OnEnable()
    {
        weapon.weaponData.weaponBehavior.InitializeWeaponBehavior(this);
    }

    void Update()
    {
        rootNode.Tick();
    }

    public void SetArmTemp(float angle)
    {
        SetArmAngle(angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if entered aggro range, set target
        //(could use ontriggerstay instead)
    }
}
