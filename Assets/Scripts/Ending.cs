using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
	public void MoveToEnding()
	{
		if(PlayerPrefs.GetInt("Conscience_Ending") >= 30)
		{
			SceneManager.LoadScene("Opening Story_Ending Happy");
		}
		else
		{
			SceneManager.LoadScene("Opening Story_Ending Sad");
		}
	}
}