using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptPlayer : MonoBehaviour
{
    float playerSpeedHorizontal;
    float playerSpeedVertical;
    // Start is called before the first frame update
    void Start()
    {
        playerSpeedHorizontal = 10f;
        playerSpeedVertical = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        float transV = Input.GetAxis("Vertical") * playerSpeedVertical * Time.deltaTime;
        float transH = Input.GetAxis("Horizontal") * playerSpeedHorizontal * Time.deltaTime;

        transform.Translate(transH, transV, 0);
    }
}
