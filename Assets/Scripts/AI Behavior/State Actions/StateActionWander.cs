using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="State Nodes/Actions/Wander")]
public class StateActionWander : StateAction
{
    public override void Tick(){
        //wander around map, seek to kill enemies
    }
}
