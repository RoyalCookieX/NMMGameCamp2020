using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCapture : MonoBehaviour
{
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
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "red")
        {
            red += 10;
        }
        else
        {
            if (other.gameObject.tag == "blue")
                blue += 10;
        }
    }
    void OnGUI()
    {
        GUI.Label(new Rect(10, 30, 100, 35), "red: " + red);
        GUI.Label(new Rect(10, 20, 100, 50), "blue: " + blue);
    }
}
