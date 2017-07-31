using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    

	// Use this for initialization
	void Start () {

    GameObject NewGame = GameObject.Find("New Game");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("Train_Kinder");
        }
	}


        

}
