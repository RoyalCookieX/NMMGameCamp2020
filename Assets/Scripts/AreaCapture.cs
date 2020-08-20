using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCapture : MonoBehaviour
{
    public Vector3 speed;
    public float red;
    public float blue;
    // Start is called before the first frame update
    void Start()
    {
        red = 0;
        blue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speed * Time.deltaTime); 
    }
    
    
    void OnGUI()
    {
        GUI.Label(new Rect(10, 30, 100, 35), "red: " + red);
        GUI.Label(new Rect(10, 20, 100, 50), "blue: " + blue);
    }
}
