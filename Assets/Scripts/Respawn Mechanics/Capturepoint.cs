using System.Collections;
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
        OnCaptured(teamData);
    }

    public void AddProgress(TeamData teamData, float progressToAdd = 0.01f)
    {
        if (base.teamData == teamData) return;
        if (!teamProgress.ContainsKey(teamData)) teamProgress.Add(teamData, 0);
        if(teamProgress[teamData] < maxProgress) teamProgress[teamData] += progressToAdd;
        else OnCaptured(teamData);
    }

    void OnCaptured(TeamData teamData)
    {
        if (!teamData) return;
        onCaptureEvent?.Invoke(teamData, this);
        base.teamData = teamData;
        teamProgress = new Dictionary<TeamData, float>();
    }
}
