using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public StateRoot rootNode;

    void Awake()
    {
        rootNode.InitializeNode(this);
    }

    void Update()
    {
        rootNode.Tick();
    }
}
