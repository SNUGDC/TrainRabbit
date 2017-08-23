using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timestop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void Timepause()
    {
        Time.timeScale = 0.0f;
    }

    public void Timeresume()
    {
        Time.timeScale = 1.0f;
    }
}
