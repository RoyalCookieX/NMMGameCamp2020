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
        OnCaptured(teamData);
    }

    public void AddProgress(TeamData team, float progressToAdd = 0.01f)
    {
        if (base.teamData == team) return;
        if (!teamProgress.ContainsKey(team)) teamProgress.Add(team, 0);
        if (teamProgress[team] < maxProgress) teamProgress[team] += progressToAdd;
        else OnCaptured(team);
    }

    void OnCaptured(TeamData team)
    {
        if (!team || this.teamData == team) return;
        onCaptureEvent?.Invoke(team, this);
        base.teamData = team;
        teamProgress = new Dictionary<TeamData, float>();
    }

    Vector2 PointInsideCircle()
    {
        //var x = transform.position.x;
        //var y = transform.position.y;


        //Vector2 randomPoint = Random.insideUnitCircle * radius;
        //var newx = randomPoint.x + x;
        //var newy = randomPoint.y + y;

        //return new Vector2 (newx, newy);
        return (Vector2)transform.position + (Random.insideUnitCircle * radius);
    }

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        if (colliders != null)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.transform.root.TryGetComponent(out Character character))
                {
                    AddProgress(character.teamData);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
