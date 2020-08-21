using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Team
{
    public TeamData teamData;
    public List<Character> characterList;
    public List<Spawnpoint> teamSpawnpoints;
    public List<Capturepoint> uncapturedPoints;

    public Team(TeamData teamData)
    {
        this.teamData = teamData;
        characterList = new List<Character>();
        teamSpawnpoints = new List<Spawnpoint>();
        uncapturedPoints = new List<Capturepoint>();

    }
}
