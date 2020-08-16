using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public StateRoot behaviorTree;
    public bool toggleTest;

    void Awake()
    {
        behaviorTree.InitializeNode(this);
    }

    void Update()
    {
        behaviorTree.Tick();
    }
}
