using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnpointManager : MonoBehaviour
{
    public Dictionary<string, List<Spawnpoint>> SpawnpointDictionary { get; set; }
    public static SpawnpointManager Instance { get; set; }
    const string uncaptured = "UNCAPTURED";

    private void Start()
    {
        //singleton pattern
        if(Instance == null) Instance = this;
        else Destroy(gameObject);

        //Initalize spawnpoints
        List<Spawnpoint> spawnpoints = FindObjectsOfType<Spawnpoint>().ToList();
        AddEntry(uncaptured, spawnpoints);
    }

    public void AddEntry(string key, List<Spawnpoint> value)
    {
        SpawnpointDictionary.Add(key, value);
    }

    public void CaptureSpawnpoint(string teamName, Spawnpoint spawnpoint)
    {
        if(SpawnpointDictionary[uncaptured].Contains(spawnpoint))
        {
            SpawnpointDictionary[teamName].Add(spawnpoint);
            SpawnpointDictionary[uncaptured].Remove(spawnpoint);
        }
    }
}
