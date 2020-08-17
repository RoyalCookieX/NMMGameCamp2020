using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TeamData", menuName = "new TeamData")]
public class TeamData : ScriptableObject
{
    [SerializeField] string teamName;
    public string TeamName { get { return teamName; } }
    [SerializeField] Color color;
    public Color Color { get { return color; } }
}
