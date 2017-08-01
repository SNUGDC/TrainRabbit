using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPageController : MonoBehaviour
{

    public GameObject StoryPageControl;
    public int TrainNum;


    // Use this for initialization
    void Start()
    {
        TrainNum = Train.trainNumber.Value;

        if (TrainNum != 20)
        { 
            StoryPageControl.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StoryPageControl.SetActive(false);
        }
	}
}
