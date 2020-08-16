﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capturepoint : Spawnpoint
{
    public delegate void OnCapture(TeamData teamName, Capturepoint capturepoint);
    public event OnCapture onCaptureEvent;
    public Dictionary<TeamData, float> teamProgress;
    public float maxProgress = 10;

    private void Start()
    {
        teamProgress = new Dictionary<TeamData, float>();
    }

    [ContextMenu("Update Capturepoint")]
    void UpdateCapturepoint()
    {
        OnCaptured(team);
    }

    public void AddProgress(TeamData team)
    {
        if (base.team == team) return;
        if (!teamProgress.ContainsKey(team)) teamProgress.Add(team, 0);
        if(teamProgress[team] < maxProgress) teamProgress[team] += Time.deltaTime;
        else OnCaptured(team);
    }

    void OnCaptured(TeamData team)
    {
        if (!team) return;
        print($"{team.TeamName} has captured a spawnpoint!");
        onCaptureEvent?.Invoke(team, this);
        base.team = team;
        teamProgress = new Dictionary<TeamData, float>();
    }
}
