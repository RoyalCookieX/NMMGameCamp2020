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
    public int Size { get; set; } = 100;
    public int threshold = 30;


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
        for(int i = 0; i < Size; i++)
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

    void PlaySound(AudioClip clip, Vector3 position, Quaternion rotation)
    {
        if (pool.Count(sound => sound.activeInHierarchy) >= threshold) return;
        GameObject obj = pool.Dequeue();

        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        if (obj.TryGetComponent(out IPoolObject poolObject))
        {
            poolObject.OnSpawnObject(clip);
        }

        pool.Enqueue(obj);
    }

    public void PlaySound(string clipName, Vector3 position, Quaternion rotation)
    {
        PlaySound(Resources.Load<AudioClip>($"Audio/{clipName}"), position, rotation);
    }

    public void PlayRandom(string folderName, Vector3 position, Quaternion rotation)
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>($"Audio/{folderName}");
        PlaySound(clips[Random.Range(0, clips.Length)], position, rotation);
    }

    public void DestroyPool()
    {
        int poolSize = pool.Count;
        for (int i = 0; i < poolSize; i++)
        {
            Destroy(pool.Dequeue());
        }
    }
}
