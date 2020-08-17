using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    [SerializeField] TeamData team;
    public TeamData Team { get { return team; } private set { } }

    [SerializeField] List<Character> teamList;
    [SerializeField] float radius = 1;

    [ContextMenu("Start Game")]
    void OnStartGame()
    {
        team.teamSpawnpoints = new List<Spawnpoint>();
        foreach (Spawnpoint spawnpoint in FindObjectsOfType<Spawnpoint>())
        {
            if (spawnpoint.team == team) team.teamSpawnpoints.Add(spawnpoint);
            if(spawnpoint.GetType() == typeof(Capturepoint))
            {
                ((Capturepoint)spawnpoint).onCaptureEvent += TeamManager_onCaptureEvent;
            }
        }

        if (team.teamSpawnpoints.Count == 0) return;
        Transform startSpawnpoint = team.teamSpawnpoints[0].transform;
        for(int i = 0; i < teamList.Count; i++)
        {
            Character character = Instantiate(teamList[i], startSpawnpoint.position, startSpawnpoint.rotation);
            character.gameObject.SetActive(false);
            character.SetTeam(team);
            teamList[i] = character;
        }
    }

    [ContextMenu("End Game")]
    void OnEndGame()
    {
        foreach(Spawnpoint spawnpoint in team.teamSpawnpoints)
        {
            if (spawnpoint.GetType() == typeof(Capturepoint))
            {
                ((Capturepoint)spawnpoint).onCaptureEvent -= TeamManager_onCaptureEvent;
            }
        }
    }

    private void TeamManager_onCaptureEvent(TeamData team, Capturepoint capturepoint)
    {
        if(this.team == team)
        {
            if (!team.teamSpawnpoints.Contains(capturepoint)) team.teamSpawnpoints.Add(capturepoint);
        }
        else
        {
            if (team.teamSpawnpoints.Contains(capturepoint)) team.teamSpawnpoints.Remove(capturepoint);
        }
    }

    public void AddCharacter(Character character)
    {
        teamList.Add(character);
    }

    [ContextMenu("Capturepoint Test")]
    public void CapturepointTest()
    {
        if (team.teamSpawnpoints.Count < 2) return;
        SpawnCharacter(0, 1);
    }

    [ContextMenu("Spawnpoint Test")]
    public void SpawnpointTest()
    {
        if (team.teamSpawnpoints.Count < 1) return;
        SpawnCharacter(0, 0);
    }


    public void SpawnCharacter(int characterIndex, int spawnpointIndex)
    {
        if (team.teamSpawnpoints.Count == 0) return;
        Transform point = team.teamSpawnpoints[spawnpointIndex].transform;
        teamList[characterIndex].transform.position = (Vector2)point.position + Random.insideUnitCircle * radius;
        teamList[characterIndex].transform.rotation = point.rotation;

        teamList[characterIndex].gameObject.SetActive(true);
    }
}
