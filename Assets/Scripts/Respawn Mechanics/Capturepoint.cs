using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capturepoint : Spawnpoint
{
    public delegate void OnCapture(TeamData teamName, Capturepoint capturepoint);
    public event OnCapture onCaptureEvent;

    [Header("Capturepoint Components")]
    [SerializeField] ParticleSystem particles;
    public Dictionary<TeamData, float> teamProgress;

    [Space]
    [Header("Capturepoint Properties")]
    public float maxProgress = 10;
    public float radius;
  

    private void Start()
    {
        teamProgress = new Dictionary<TeamData, float>();
        var main = particles.main;
        main.startColor = teamData != null ? teamData.teamColor : Color.white;
    }

    public void AddProgress(TeamData teamData, float progressToAdd = 0.01f)
    {
        if (this.teamData == teamData) return;
        if (!teamProgress.ContainsKey(teamData)) teamProgress.Add(teamData, 0);
        if (teamProgress[teamData] < maxProgress) teamProgress[teamData] += progressToAdd;
        else SetTeam(teamData);
    }

    void SetTeam(TeamData teamData)
    {
        if (!teamData || this.teamData == teamData) return;
        onCaptureEvent?.Invoke(teamData, this);
        this.teamData = teamData;
        var main = particles.main;
        main.startColor = teamData.teamColor;
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
