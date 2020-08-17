using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [HideInInspector] public NonPlayerCharacter nonPlayerCharacter;
    public StateRoot rootNode;

    void Awake()
    {
        nonPlayerCharacter = GetComponent<NonPlayerCharacter>();
        rootNode.InitializeNode(this);
    }

    void Update()
    {
        rootNode.Tick();
    }
}
