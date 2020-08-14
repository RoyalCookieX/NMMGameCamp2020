using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capturepoint : Spawnpoint
{
    public Dictionary<string, float> teamProgress;
    public float maxProgress = 10;

    public void AddProgress(string teamName)
    {
        if (!teamProgress.ContainsKey(teamName)) teamProgress.Add(teamName, 0);
        teamProgress[teamName] += Time.deltaTime;
        if (teamProgress[teamName] >= 10) OnCaptured(teamName);
    }

    void OnCaptured(string teamName)
    {
        print($"{teamName} has captured a spawnpoint!");
        TeamName = teamName;
    }
}
