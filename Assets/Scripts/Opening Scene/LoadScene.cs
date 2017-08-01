using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
	/*void Start ()
	{
    	GameObject NewGame = GameObject.Find("New Game");
	}
	
	void Update ()
	{
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("Train_Kinder");
        }
	}*/

	public void NewGame()
	{
		SceneManager.LoadScene("Train_Kinder");
	}
}
