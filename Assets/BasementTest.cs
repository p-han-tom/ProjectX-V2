using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementTest : MonoBehaviour
{

    GameObject above;

    
    // Start is called before the first frame update
    void Start()
    {
        above = GameObject.Find("Test Room");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
