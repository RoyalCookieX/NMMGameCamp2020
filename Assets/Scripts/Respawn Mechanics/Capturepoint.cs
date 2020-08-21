using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capturepoint : Spawnpoint
{
    public delegate void OnCapture(TeamData teamData, Capturepoint capturepoint);
    public event OnCapture onCaptureEvent;

    [Header("Capturepoint Components")]
    [SerializeField] ParticleSystem particles;
    public Dictionary<TeamData, float> teamProgress;
    public TrackingArrow trackingArrow;

    [Space]
    [Header("Capturepoint Properties")]
    public float maxProgress = 10;
    public float radius;

    private void Start()
    {
        teamProgress = new Dictionary<TeamData, float>();
        var main = particles.main;
        main.startColor = teamData != null ? teamData.teamColor : Color.white;
        trackingArrow = Instantiate(trackingArrow, transform.position, transform.rotation);
        trackingArrow.SetColor(teamData != null ? teamData.teamColor : Color.white);
        trackingArrow.target = transform;
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
        trackingArrow.SetColor(teamData.teamColor);
    }

    public Vector2 PointInsideCircle()
    {
        return (Vector2)transform.position + (Random.insideUnitCircle * (radius-0.5f));
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
