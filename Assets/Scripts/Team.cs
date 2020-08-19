using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Team
{
    public TeamData teamData;
    public List<Character> characterList;
    public List<Spawnpoint> teamSpawnpoints;
    public List<Capturepoint> uncapturedpoints;

    public Team(TeamData teamData)
    {
        this.teamData = teamData;
        characterList = new List<Character>();
        teamSpawnpoints = new List<Spawnpoint>();
        uncapturedpoints = new List<Capturepoint>();

    }
}
