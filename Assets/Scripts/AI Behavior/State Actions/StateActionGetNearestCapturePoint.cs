using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="State Nodes/Actions/Get Nearest Capture Point")]
public class StateActionGetNearestCapturePoint : StateAction
{
    public override void Tick()
    {
        //if (nonPlayerCharacter.captureTarget)
        //{
        //    parentNode.Fail(this);
        //    return;
        //}
        if (nonPlayerCharacter.GetTeam().uncapturedPoints.Count == 0)
        {
            parentNode.Fail(this);
            return;
        }
        List<Capturepoint> capturePoints = nonPlayerCharacter.GetTeam().uncapturedPoints;
        Capturepoint closestPoint = null;
        float closestDistance = Mathf.Infinity;
        foreach (Capturepoint point in capturePoints)
        {
            float distanceToPoint = Vector2.Distance(nonPlayerCharacter.transform.position, point.transform.position);
            if (distanceToPoint < closestDistance)
            {
                closestPoint = point;
                closestDistance = distanceToPoint;
            }
        }
        nonPlayerCharacter.captureTarget = closestPoint;
        parentNode.Success(this);
    }
}
