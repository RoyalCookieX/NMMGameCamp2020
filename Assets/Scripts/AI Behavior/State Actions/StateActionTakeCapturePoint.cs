using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="State Nodes/Actions/Take Capture Point")]
public class StateActionTakeCapturePoint : StateAction
{
    //go to capture point to capture it

    public override void Tick(){
        //if capturepoint is ours, return fail
        //else if in destination inside capture point, success
        //else keep going to capture point
    }
}
