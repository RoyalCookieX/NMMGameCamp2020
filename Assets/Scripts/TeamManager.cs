using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    [SerializeField] string teamName;
    public string TeamName { get { return teamName; } private set { } }

    [SerializeField] List<Character> teamList;
    [SerializeField] List<Spawnpoint> spawnpoints;

    [ContextMenu("Start Game")]
    void OnStartGame()
    {
        spawnpoints = new List<Spawnpoint>();
        foreach (Spawnpoint spawnpoint in FindObjectsOfType<Spawnpoint>())
        {
            if (spawnpoint.TeamName == TeamName) spawnpoints.Add(spawnpoint);
            if(spawnpoint.GetType() == typeof(Capturepoint))
            {
                ((Capturepoint)spawnpoint).onCaptureEvent += TeamManager_onCaptureEvent;
            }
        }

        if (spawnpoints.Count == 0) return;
        Transform startSpawnpoint = spawnpoints[0].transform;
        for(int i = 0; i < teamList.Count; i++)
        {
            Character character = Instantiate(teamList[i], startSpawnpoint.position, startSpawnpoint.rotation);
            character.gameObject.SetActive(false);
            teamList[i] = character;
        }
    }

    [ContextMenu("End Game")]
    void OnEndGame()
    {
        foreach(Spawnpoint spawnpoint in spawnpoints)
        {
            if (spawnpoint.GetType() == typeof(Capturepoint))
            {
                ((Capturepoint)spawnpoint).onCaptureEvent -= TeamManager_onCaptureEvent;
            }
        }
    }

    private void TeamManager_onCaptureEvent(string teamName, Capturepoint capturepoint)
    {
        if(this.teamName == teamName)
        {
            if (!spawnpoints.Contains(capturepoint)) spawnpoints.Add(capturepoint);
        }
        else
        {
            if (spawnpoints.Contains(capturepoint)) spawnpoints.Remove(capturepoint);
        }
    }

    public void AddCharacter(Character character)
    {
        teamList.Add(character);
    }

    [ContextMenu("Capturepoint Test")]
    public void CapturepointTest()
    {
        if (spawnpoints.Count < 2) return;
        SpawnCharacter(0, 1);
    }

    [ContextMenu("Spawnpoint Test")]
    public void SpawnpointTest()
    {
        if (spawnpoints.Count < 1) return;
        SpawnCharacter(0, 0);
    }


    public void SpawnCharacter(int characterIndex, int spawnpointIndex)
    {
        if (spawnpoints.Count == 0) return;
        Transform point = spawnpoints[spawnpointIndex].transform;
        teamList[characterIndex].transform.position = point.position;
        teamList[characterIndex].transform.rotation = point.rotation;

        teamList[characterIndex].gameObject.SetActive(true);
    }
}
