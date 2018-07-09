using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
	public string name;
	public int index;
	public void MoveScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
        Train.trainNumber = 20;
	}
	public void MoveScene(){
		SceneManager.LoadScene(index);
		Train.trainNumber = 20;
	}
}