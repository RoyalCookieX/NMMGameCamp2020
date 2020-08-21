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

    }

    public override void Tick()
    {
        //if capturepoint is ours, return fail
        //else if in destination inside capture point, success  
        //else keep going to capture point
        //if (!nonPlayerCharacter.captureTarget)
        //{
            //List<Capturepoint> capturePoints = nonPlayerCharacter.GetTeam().uncapturedPoints;
            //Capturepoint closestPoint = null;
            //float closestDistance = Mathf.Infinity;
            //foreach (Capturepoint point in capturePoints)
            //{
            //    float distanceToPoint = Vector2.Distance(nonPlayerCharacter.transform.position, point.transform.position);
            //    if (distanceToPoint < closestDistance)
            //    {
            //        closestPoint = point;
            //        closestDistance = distanceToPoint;
            //    }
            //}
            //nonPlayerCharacter.captureTarget = closestPoint;
            destination = nonPlayerCharacter.captureTarget.PointInsideCircle();
        //}
        Vector2 dir = nonPlayerCharacter.captureTarget.transform.position - nonPlayerCharacter.transform.position;
        nonPlayerCharacter.transform.Translate(dir.normalized * nonPlayerCharacter.movementSpeed * Time.deltaTime);
        if(Vector2.Distance(nonPlayerCharacter.transform.position, destination) < nonPlayerCharacter.captureTarget.radius-0.5f)
        {
            parentNode.Success(this);
            Debug.Log("TAKING SUCCESS");
        }
    }
}
