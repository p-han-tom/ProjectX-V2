using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWithBasementTest : MonoBehaviour
{
    GameObject basement;
    bool displaying = true;

    // Start is called before the first frame update
    void Start()
    {
        basement = GameObject.Find("Test Basement");
        basement.SetActive(!displaying);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F)) {
            displaying = !displaying;
            gameObject.SetActive(displaying);
            basement.SetActive(!displaying);

        }
    }
}
