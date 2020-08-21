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
        if (nonPlayerCharacter.captureTarget == null) parentNode.Fail(this);
        float distance = Vector2.Distance(nonPlayerCharacter.transform.position, nonPlayerCharacter.captureTarget.transform.position);
        float captureTargetRadius = nonPlayerCharacter.captureTarget.radius;
        if (distance < captureTargetRadius - 0.5f)
        {
            parentNode.Fail(this);
            return;
        }
        destination = nonPlayerCharacter.captureTarget.PointInsideCircle();
    }

    public override void Tick()
    {
        //float distance = Vector2.Distance(nonPlayerCharacter.transform.position, nonPlayerCharacter.captureTarget.transform.position);
        //float captureTargetRadius = nonPlayerCharacter.captureTarget.radius;
        //if (distance < captureTargetRadius - 0.5f)
        //{
        //    parentNode.Fail(this);
        //    return;
        //}
        if (nonPlayerCharacter.captureTarget.teamData == nonPlayerCharacter.teamData)
        {
            //nonPlayerCharacter.captureTarget = null;
            parentNode.Fail(this);
            return;
        }
        //destination = nonPlayerCharacter.captureTarget.PointInsideCircle();
        Vector2 dir = destination - (Vector2)nonPlayerCharacter.transform.position;
        nonPlayerCharacter.transform.Translate(dir.normalized * nonPlayerCharacter.movementSpeed * Time.deltaTime);
        //Debug.Log(destination);
        if(Vector2.Distance(nonPlayerCharacter.transform.position, destination) < .4f)
        {
            parentNode.Success(this);
            Debug.Log("TAKING SUCCESS");
        }
    }
}
