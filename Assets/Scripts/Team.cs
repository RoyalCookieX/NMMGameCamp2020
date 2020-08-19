using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Team
{
    public TeamData teamData;
    public List<Character> playerList;
    public List<Spawnpoint> teamSpawnpoints;
    public List<Capturepoint> uncapturedpoints;

    public Team(TeamData teamData)
    {
        this.teamData = teamData;
        playerList = new List<Character>();
        teamSpawnpoints = new List<Spawnpoint>();
        uncapturedpoints = new List<Capturepoint>();

    }
}
