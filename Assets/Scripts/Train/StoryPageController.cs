using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPageController : MonoBehaviour {

    public GameObject StoryPageControl;
    public GameObject TrainNum;


    // Use this for initialization
    void Start()
    {
        GameObject train = GameObject.Find("Train Generator");
        TrainNum = train.transform.Find("Train Num").gameObject;
        if (TrainNum == 0) { 
        StoryPageControl.SetActive(true);
    }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            StoryPageControl.SetActive(false);
        }
	}
}
