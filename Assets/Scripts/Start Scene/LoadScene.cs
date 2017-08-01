using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
	public void NewGame()
	{
		SceneManager.LoadScene("Opening Story");
	}
}
