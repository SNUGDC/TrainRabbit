﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
	public void MoveScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
		SoundManager.SetIsOnTrain(false);
        Train.trainNumber = 20;
	}
}