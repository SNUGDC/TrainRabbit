using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPageController : MonoBehaviour {

    public GameObject StoryPageControl;

    

	// Use this for initialization
	void Start () {
        
        StoryPageControl.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            StoryPageControl.SetActive(false);
        }
	}
}
