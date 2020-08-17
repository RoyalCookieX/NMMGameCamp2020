using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameCamera : MonoBehaviour
{
    public static GameCamera Instance { get; private set; }

    [SerializeField] Camera cam;
    public Camera Cam { get { return cam; } }

    private void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if(Instance == this) Instance = null;
    }
}
