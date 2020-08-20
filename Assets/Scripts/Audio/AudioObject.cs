using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour, IPoolObject
{
    [SerializeField] AudioSource source;

    public void OnSpawnObject(params object[] args)
    {
        StopAllCoroutines();
        StartCoroutine(AudioEnumerator((AudioClip)args[0]));
    }

    IEnumerator AudioEnumerator(AudioClip clip)
    {
        source.PlayOneShot(clip);
        yield return null;
        yield return new WaitUntil(() => !source.isPlaying);
        gameObject.SetActive(false);
    }
}
