using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
	private PlayerStatus.PlayerAge playerAge;

	private void Start()
	{
		playerAge = GameObject.Find("Train Generator").GetComponent<TrainGenerator>().playerAge;
	}
	public void SaveConscience()
	{
		switch(playerAge)
		{
			case PlayerStatus.PlayerAge.Kinder:
				PlayerPrefs.SetInt("Conscience_Elementary", PlayerData.Conscience);
				return;
			case PlayerStatus.PlayerAge.Elementary:
				PlayerPrefs.SetInt("Conscience_Middle", PlayerData.Conscience);
				return;
			case PlayerStatus.PlayerAge.Middle:
				PlayerPrefs.SetInt("Conscience_High", PlayerData.Conscience);
				return;
			case PlayerStatus.PlayerAge.High:
				PlayerPrefs.SetInt("Conscience_Graduate", PlayerData.Conscience);
				return;
			case PlayerStatus.PlayerAge.Graduate:
				PlayerPrefs.SetInt("Conscience_Ending", PlayerData.Conscience);
				return;
		}
	}
}