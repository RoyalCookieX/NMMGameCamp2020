using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    [SerializeField] TeamData team;
    public TeamData Team { get { return team; } private set { } }

    [SerializeField] List<Spawnpoint> teamSpawnpoints;
    [SerializeField] List<Capturepoint> uncapturedpoints;
    [SerializeField] List<Character> teamList;
    [SerializeField] float radius = 1;

    [ContextMenu("Start Game")]
    void OnStartGame()
    {
        //reset spawnpoints
        teamSpawnpoints = new List<Spawnpoint>();
        uncapturedpoints = new List<Capturepoint>();

        //initalize spawnpoints
        foreach (Spawnpoint spawnpoint in FindObjectsOfType<Spawnpoint>())
        {
            if (spawnpoint.team == team) teamSpawnpoints.Add(spawnpoint);
            if(spawnpoint.GetType() == typeof(Capturepoint))
            {
                //subscribing each capturepoint's event to TeamManager_onCaptureEvent method
                ((Capturepoint)spawnpoint).onCaptureEvent += TeamManager_onCaptureEvent;
                if (spawnpoint.team != team) uncapturedpoints.Add((Capturepoint)spawnpoint);
            }
        }

        //gets the first spawnpoint
        if (teamSpawnpoints.Count == 0)
        {
            Debug.LogError($"{team.teamName} Team has no spawnpoints.");
            return;
        }
        Transform startSpawnpoint = teamSpawnpoints[0].transform;

        //initalizes characters and places them at startSpawnpoint
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
        //unsubscribes method from capturepoints
        foreach(Capturepoint capturepoint in FindObjectsOfType<Capturepoint>())
        {
            capturepoint.onCaptureEvent -= TeamManager_onCaptureEvent;
        }
    }

    //calls every time a capturepoint gets captured by a team
    private void TeamManager_onCaptureEvent(TeamData team, Capturepoint capturepoint)
    {
        //if this team captured a capturepoint
        if(this.team == team)
        {
            if (teamSpawnpoints.Contains(capturepoint)) return;

            //add to teamSpawnpoints, remove from uncapturedpoints
            teamSpawnpoints.Add(capturepoint);
            uncapturedpoints.Remove(capturepoint);
        }
        else
        {
            if (!teamSpawnpoints.Contains(capturepoint)) return;

            //remove from teamSpawnpoints, add to uncaptuedpoints
            uncapturedpoints.Add(capturepoint);
            teamSpawnpoints.Remove(capturepoint);
        }
    }

    public void AddCharacter(Character character)
    {
        teamList.Add(character);
    }

    [ContextMenu("Capturepoint Test")]
    public void CapturepointTest()
    {
        if (teamSpawnpoints.Count < 2) return;
        SpawnCharacter(0, 1);
    }

    [ContextMenu("Spawnpoint Test")]
    public void SpawnpointTest()
    {
        if (teamSpawnpoints.Count < 1) return;
        SpawnCharacter(0, 0);
    }

    public void SpawnCharacter(int characterIndex, int spawnpointIndex)
    {
        if (teamSpawnpoints.Count == 0) return;
        Transform point = teamSpawnpoints[spawnpointIndex].transform;
        teamList[characterIndex].transform.position = (Vector2)point.position + Random.insideUnitCircle * radius;
        teamList[characterIndex].transform.rotation = point.rotation;

        teamList[characterIndex].gameObject.SetActive(true);
    }
}
