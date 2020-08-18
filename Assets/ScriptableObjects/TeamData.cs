using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TeamData", menuName = "new TeamData")]
public class TeamData : ScriptableObject
{
    public string teamName;
    public Color teamColor;
}
