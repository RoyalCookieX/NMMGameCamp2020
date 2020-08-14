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

    private void Start()
    {

    }

    [ContextMenu("Start Game")]
    void OnStartGame()
    {
        spawnpoints = new List<Spawnpoint>();
        foreach (Spawnpoint spawnpoint in FindObjectsOfType<Spawnpoint>())
        {
            if (spawnpoint.TeamName == TeamName) spawnpoints.Add(spawnpoint);
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

    public void AddSpawnpoint(Spawnpoint spawnpoint)
    {
        spawnpoints.Add(spawnpoint);
    }

    public void AddCharacter(Character character)
    {
        teamList.Add(character);
    }

    [ContextMenu("Spawn Test")]
    public void SpawnTest()
    {
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
