using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class NonPlayerCharacter : Character
{
    public float movementSpeed;

    public Character target;
    public Capturepoint captureTarget;
    public StateRoot rootNode;

    [HideInInspector] public StateRoot behaviorTree;

    void Awake()
    {
        CreateBehaviorTree();
        behaviorTree.InitializeNode(this);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        weapon.weaponData.weaponBehavior.InitializeWeaponBehavior(this);
    }

    void Update()
    {
        //TEMPORARY. REMOVE THIS AFTER DEBUGGING.
        //if (!target) target = GameObject.Find("Player(Clone)").GetComponent<Character>();
        behaviorTree.Tick();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if entered aggro range, set target
        //(could use ontriggerstay instead)
    }

    void CreateBehaviorTree()
    {
        behaviorTree = Instantiate(rootNode);
        behaviorTree.InstantiateSubNodes();
    }
}
