using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(menuName="State Nodes/Actions/Wander")]
public class StateActionWander : StateAction
{
    public override void Tick(){
        Debug.Log("wander starting");
        //wander around map, seek to kill enemies
        parentNode.Success(this);
    }
}
