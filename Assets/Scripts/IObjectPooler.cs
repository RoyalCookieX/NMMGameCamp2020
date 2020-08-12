using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPooler<T> where T : Object
{
    Queue<T> Pool { get; }
    int Size { get; set; }
    T GetFromPool(Vector3 position, Quaternion rotation);
    void DestroyPool();
}
