using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="State Nodes/Actions/Occupy Capture Point")]
public class StateActionOccupyCapturePoint : StateAction
{
    //stay in capture point until it's captured

    public override void Tick(){
        //if capturetarget progress isnt 100, keep staying and defending
        //is progress is 100, return success
    }
}
