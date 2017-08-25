using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReset : MonoBehaviour
{
	public void Reset()
	{
		PlayerPrefs.DeleteAll();
	}
}