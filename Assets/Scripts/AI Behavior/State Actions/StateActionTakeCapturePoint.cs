using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State Nodes/Actions/Take Capture Point")]
public class StateActionTakeCapturePoint : StateAction
{
    //go to capture point to capture it
    private Vector2 destination;

    public override void OnEnter()
    {
        destination = nonPlayerCharacter.captureTarget.PointInsideCircle();
    }

    public override void Tick()
    {
        if (nonPlayerCharacter.captureTarget.teamData == nonPlayerCharacter.teamData)
        {
            nonPlayerCharacter.captureTarget = null;
            parentNode.Fail(this);
            return;
        }
        //destination = nonPlayerCharacter.captureTarget.PointInsideCircle();
        Vector2 dir = nonPlayerCharacter.captureTarget.transform.position - nonPlayerCharacter.transform.position;
        nonPlayerCharacter.transform.Translate(dir.normalized * nonPlayerCharacter.movementSpeed * Time.deltaTime);
        if(Vector2.Distance(nonPlayerCharacter.transform.position, destination) < 0.4f)
        {
            parentNode.Success(this);
            Debug.Log("TAKING SUCCESS");
        }
    }
}
