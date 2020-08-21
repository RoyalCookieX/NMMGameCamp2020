using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="State Nodes/Actions/Animator Toggler")]
public class StateActionAnimatorToggler : StateAction
{
    public string[] falseBools;
    public string[] trueBools;
    public string[] triggers;
    public override void OnEnter()
    {
        foreach(string parameter in falseBools)
        {
            nonPlayerCharacter.anim.SetBool(parameter, false);
        }
        foreach (string parameter in trueBools)
        {
            nonPlayerCharacter.anim.SetBool(parameter, true);
        }
        foreach (string parameter in triggers)
        {
            nonPlayerCharacter.anim.SetTrigger(parameter);
        }
        parentNode.Success(this);
    }

    public override void Tick()
    {
        //dont need it m8
    }
}
