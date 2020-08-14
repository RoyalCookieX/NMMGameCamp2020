using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capturepoint : Spawnpoint
{
    public delegate void OnCapture(string teamName, Capturepoint capturepoint);
    public event OnCapture onCaptureEvent;
    public Dictionary<string, float> teamProgress;
    public float maxProgress = 10;

    private void Start()
    {
        teamProgress = new Dictionary<string, float>();
    }

    public void AddProgress(string teamName)
    {
        if (teamName == TeamName) return;
        if (!teamProgress.ContainsKey(teamName)) teamProgress.Add(teamName, 0);
        teamProgress[teamName] += Time.deltaTime;
        if (teamProgress[teamName] >= 10) OnCaptured(teamName);
    }

    [ContextMenu("Blue Capture")]
    public void BlueCapture()
    {
        OnCaptured("Blue");
    }

    [ContextMenu("Red Capture")]
    public void RedCapture()
    {
        OnCaptured("Red");
    }

    void OnCaptured(string teamName)
    {
        print($"{teamName} has captured a spawnpoint!");
        onCaptureEvent?.Invoke(teamName, this);
        TeamName = teamName;
        teamProgress = new Dictionary<string, float>();
    }
}
