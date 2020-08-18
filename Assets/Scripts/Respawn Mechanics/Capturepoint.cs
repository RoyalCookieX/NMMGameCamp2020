using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capturepoint : Spawnpoint
{
    public delegate void OnCapture(TeamData teamName, Capturepoint capturepoint);
    public event OnCapture onCaptureEvent;
    public Dictionary<TeamData, float> teamProgress;
    public float maxProgress = 10;
    public float radius;

    private void Start()
    {
        teamProgress = new Dictionary<TeamData, float>();
        
    }

    [ContextMenu("Update Capturepoint")]
    void UpdateCapturepoint()
    {
        OnCaptured(team);
    }

    public void AddProgress(TeamData team, float progressToAdd = 0.01f)
    {
        if (base.team == team) return;
        if (!teamProgress.ContainsKey(team)) teamProgress.Add(team, 0);
        if (teamProgress[team] < maxProgress) teamProgress[team] += progressToAdd;
        else OnCaptured(team);
    }

    void OnCaptured(TeamData team)
    {
        if (!team || this.team == team) return;
        onCaptureEvent?.Invoke(team, this);
        base.team = team;
        teamProgress = new Dictionary<TeamData, float>();
    }

    bool PointInsideSphere(Vector2 center, float radius)
    {
        Vector2 randomPoint = Random.insideUnitCircle;
        bool payLoad = false;
        var characters = GameObject.FindObjectsOfType<Character>();
        foreach(Character character in characters)
        {
            //check if this character is in or out
            Vector2 pos;
            pos.x = character.transform.position.x;
            pos.y = character.transform.position.y;

            payLoad = Vector2.Distance(pos, center) < radius;
        }
        return payLoad;
    }
}
