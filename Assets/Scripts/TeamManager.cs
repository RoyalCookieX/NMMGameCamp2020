using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public static TeamManager Instance { get; private set; }

    [SerializeField] List<TeamData> teamDatas;
    [SerializeField] List<Team> teams;

    [SerializeField] float radius = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else Destroy(gameObject);
        
    }

    private void Start()
    {
        OnStartGame();
    }

    private void OnDestroy()
    {
        if(Instance = this) Instance = null;
    }

    public void OnPointCaptured(Capturepoint capturepoint, Team oldTeam, Team newTeam)
    {
        if (!newTeam.teamSpawnpoints.Contains(capturepoint))
        {
            newTeam.teamSpawnpoints.Add(capturepoint);
            oldTeam.teamSpawnpoints.Remove(capturepoint);
        }
    }

    [ContextMenu("Start Game")]
    void OnStartGame()
    {
        //create all teams
        teams = new List<Team>();
        for(int teamIndex = 0; teamIndex < teamDatas.Count; teamIndex++)
        {
            //create team based on teamData
            TeamData teamData = teamDatas[teamIndex];
            Team team = new Team(teamData);

            if (teamData.PresetTeam != null) team.playerList.AddRange(teamData.PresetTeam);

            for(int playerIndex = 0; playerIndex < team.playerList.Count; playerIndex++)
            {
                Character player = Instantiate(team.playerList[playerIndex]);
                team.playerList[playerIndex] = player;
                player.CharTeam = team;
                player.gameObject.SetActive(false);
            }

            //get all spawnpoints / capturepoints
            List<Spawnpoint> allSpawnpoints = FindObjectsOfType<Spawnpoint>().ToList();
            List<Capturepoint> allCapturepoints = FindObjectsOfType<Capturepoint>().ToList();

            //get all team's spawnpoints in the scene
            team.teamSpawnpoints = allSpawnpoints.FindAll(spawnpoint => spawnpoint.teamData == team.teamData);
            //get all uncapturedpoints for this team in the scene
            team.uncapturedpoints = allCapturepoints.FindAll(spawnpoint => spawnpoint.teamData != team.teamData);
            teams.Add(team);

            //spawn each character
            for(int characterIndex = 0; characterIndex < team.playerList.Count; characterIndex++)
            {
                SpawnCharacter(teamIndex, characterIndex, 0);
            }
        }
    }

    public void SpawnCharacter(int teamIndex, int characterIndex, int spawnpointIndex)
    {
        if (teams[teamIndex].teamSpawnpoints.Count == 0) return;
        Transform point = teams[teamIndex].teamSpawnpoints[spawnpointIndex].transform;
        teams[teamIndex].playerList[characterIndex].transform.position = (Vector2)point.position + UnityEngine.Random.insideUnitCircle * radius;
        teams[teamIndex].playerList[characterIndex].transform.rotation = point.rotation;

        teams[teamIndex].playerList[characterIndex].gameObject.SetActive(true);
    }
}
