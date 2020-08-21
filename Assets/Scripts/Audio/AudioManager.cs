using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] GameObject audioObjectPrefab;
    [SerializeField] protected Queue<GameObject> pool;
    public Queue<GameObject> Pool { get { return pool; } }
    public int poolSize = 100;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        pool = new Queue<GameObject>();
        for(int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(audioObjectPrefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    private void OnDestroy()
    {
        if(Instance == this) Instance = null;
        DestroyPool();
    }

    void PlaySound(AudioClip clip, AudioObjectSettings settings, Vector3 position, Quaternion rotation)
    {
        GameObject obj = pool.Dequeue();

        obj.gameObject.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        if (obj.TryGetComponent(out IPoolObject poolObject))
        {
            poolObject.OnSpawnObject(clip, settings);
        }

        pool.Enqueue(obj);
    }

    public void PlaySound(string clipName, AudioObjectSettings settings, Vector3 position, Quaternion rotation)
    {
        PlaySound(Resources.Load<AudioClip>($"Audio/{clipName}"), settings, position, rotation);
    }

    public void PlayRandom(string folderName, AudioObjectSettings settings, Vector3 position, Quaternion rotation)
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>($"Audio/{folderName}");
        PlaySound(clips[Random.Range(0, clips.Length)], settings, position, rotation);
    }

    public void ResetSounds()
    {
        foreach (GameObject obj in pool) obj.SetActive(false);
    }

    public void DestroyPool()
    {
        int poolSize = pool.Count;
        for (int i = 0; i < poolSize; i++)
        {
            Destroy(pool.Dequeue());
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(20, Screen.height - 20, 300, 300), $"{pool.Count(o => o.activeInHierarchy)} / {poolSize}");
    }
}
