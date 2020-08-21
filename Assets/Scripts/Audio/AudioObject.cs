using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour, IPoolObject
{
    [SerializeField] AudioSource source;
    public AudioSource Source { get { return source; } }

    Coroutine coroutine;

    public void OnSpawnObject(params object[] args)
    {
        if (source.isPlaying) return;

        AudioObjectSettings settings = (AudioObjectSettings)args[1];
        source.priority = settings.priority;
        source.volume = settings.volume;

        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(AudioEnumerator((AudioClip)args[0]));
    }

    IEnumerator AudioEnumerator(AudioClip clip)
    {
        if(source.isPlaying)
        {
            print("sound");
            source.Stop();
        }
        source.PlayOneShot(clip);
        yield return null;
        yield return new WaitUntil(() => !source.isPlaying);
        gameObject.SetActive(false);
    }
}

[System.Serializable]
public struct AudioObjectSettings
{
    public int priority;
    public float volume;

    public AudioObjectSettings(int priority = 128, float volume = 1)
    {
        this.priority = priority;
        this.volume = volume;
    }
}
