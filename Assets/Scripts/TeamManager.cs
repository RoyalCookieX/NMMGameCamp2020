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
    [SerializeField] float respawnTime = 1;
    int totalCapturepoints;

    Coroutine countdownCoroutine;

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

    public Team GetTeam(TeamData teamData)
    {
        Team team = teams.Find(_team => _team.teamData == teamData);
        return team;
    }

    void OnStartGame()
    {
        //create all teams
        teams = new List<Team>();
        for(int teamIndex = 0; teamIndex < teamDatas.Count; teamIndex++)
        {
            //create team based on teamData
            TeamData teamData = teamDatas[teamIndex];
            Team team = new Team(teamData);

            if (teamData.PresetTeam != null) team.characterList.AddRange(teamData.PresetTeam);

            for(int playerIndex = 0; playerIndex < team.characterList.Count; playerIndex++)
            {
                Character character = Instantiate(team.characterList[playerIndex]);
                team.characterList[playerIndex] = character;
                character.teamData = team.teamData;
                character.gameObject.SetActive(false);
                character.OnDeathEvent += OnDeathEvent;
            }

            //get all spawnpoints / capturepoints
            List<Spawnpoint> allSpawnpoints = FindObjectsOfType<Spawnpoint>().ToList();
            List<Capturepoint> allCapturepoints = FindObjectsOfType<Capturepoint>().ToList();

            //set totalCapturepoints
            totalCapturepoints = allSpawnpoints.Count;

            //get all team's spawnpoints in the scene
            team.teamSpawnpoints = allSpawnpoints.FindAll(spawnpoint => spawnpoint.teamData == team.teamData);
            //get all uncapturedpoints for this team in the scene
            team.uncapturedPoints = allCapturepoints.FindAll(spawnpoint => spawnpoint.teamData != team.teamData);
            teams.Add(team);

            //sub all capturepoint methods to 
            foreach(Capturepoint capturepoint in allCapturepoints)
            {
                capturepoint.onCaptureEvent += OnPointCaptured;
            }

            //spawn each character
            for(int characterIndex = 0; characterIndex < team.characterList.Count; characterIndex++)
            {
                SpawnCharacter(teamIndex, characterIndex, 0);
            }
        }
    }

    private void OnPointCaptured(TeamData teamData, Capturepoint capturepoint)
    {
        //set each team's capturepoints
        foreach(Team team in teams)
        {
            if(team.teamData == teamData)
            {
                if (countdownCoroutine != null) StopCoroutine(countdownCoroutine);
                if(!team.teamSpawnpoints.Contains(capturepoint)) team.teamSpawnpoints.Add(capturepoint);
                if(team.uncapturedPoints.Contains(capturepoint)) team.uncapturedPoints.Remove(capturepoint);
            }
            else
            {
                if (!team.uncapturedPoints.Contains(capturepoint)) team.uncapturedPoints.Add(capturepoint);
                if (team.teamSpawnpoints.Contains(capturepoint)) team.teamSpawnpoints.Remove(capturepoint);
            }

            //check victory condition
            if(team.teamSpawnpoints.Count == totalCapturepoints)
            {
                countdownCoroutine = StartCoroutine(Countdown(team.teamData));
            }
        }
    }

    void OnDeathEvent(Character character)
    {
        IEnumerator SpawnEnumerator()
        {
            yield return new WaitForSecondsRealtime(respawnTime);
            SpawnCharacter(character, Random.Range(0, character.GetTeam().teamSpawnpoints.Count));
        }
        StartCoroutine(SpawnEnumerator());
    }

    public void SpawnCharacter(Character character, int spawnpointIndex)
    {
        if (character.GetTeam().teamSpawnpoints.Count == 0) return;
        Transform point = character.GetTeam().teamSpawnpoints[spawnpointIndex].transform;
        character.transform.position = (Vector2)point.position + Random.insideUnitCircle * radius;
        character.transform.rotation = point.rotation;

        character.gameObject.SetActive(true);
    }

    public void SpawnCharacter(int teamIndex, int characterIndex, int spawnpointIndex)
    {
        if (teams[teamIndex].teamSpawnpoints.Count == 0) return;
        Transform point = teams[teamIndex].teamSpawnpoints[spawnpointIndex].transform;
        teams[teamIndex].characterList[characterIndex].transform.position = (Vector2)point.position + Random.insideUnitCircle * radius;
        teams[teamIndex].characterList[characterIndex].transform.rotation = point.rotation;

        teams[teamIndex].characterList[characterIndex].gameObject.SetActive(true);
    }

    IEnumerator Countdown(TeamData teamData)
    {
        for(float i = 5; i >= 0; i -= Time.deltaTime)
        {
            PlayerGUI.Instance.SetTitle($"{teamData.teamName}: {Mathf.Ceil(i)}", 0.5f);
            yield return null;
        }
        PlayerGUI.Instance.SetTitle($"{teamData.teamName} Wins!", 3f);
        foreach (Team team in teams)
        {
            if (team.teamData != teamData)
            {
                foreach (Character character in team.characterList)
                {
                    character.Die();
                }
            }
        }
    }
}
