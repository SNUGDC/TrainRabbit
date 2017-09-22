using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
	public void Reset()
	{
        PlayerData.HP = 100;
        PlayerData.Conscience = 100;
		PlayerPrefs.DeleteAll();
	}
}