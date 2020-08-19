using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State Nodes/Conditions/Is Inside Capture Target")]
public class StateConditionIsInsideCaptureTarget : StateCondition
{
    public override bool CheckCondition()
    {
        float distance = Vector2.Distance(nonPlayerCharacter.transform.position, nonPlayerCharacter.captureTarget.transform.position);
        float captureTargetRadius = nonPlayerCharacter.captureTarget.GetComponent<CircleCollider2D>().radius;
        if (distance < captureTargetRadius - 1f)
        {
            return true;
        }
        return false;
    }
}
