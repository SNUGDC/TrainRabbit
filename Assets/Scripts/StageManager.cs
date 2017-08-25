using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
	public GameObject[] StageButton;
	public Text[] StageConscience;

	private int?[] Conscience = new int?[5] {null, null, null, null, null};

	private void Start()
	{
		GetSavedConscience();
		StageLockAndShowStartConscience();
	}

	private void GetSavedConscience()
	{
		Conscience[0] = 100;

		if(PlayerPrefs.HasKey("Conscience_Elementary"))
		{
			Conscience[1] = PlayerPrefs.GetInt("Conscience_Elementary");
		}
		else
			return;

		if(PlayerPrefs.HasKey("Conscience_Middle"))
		{
			Conscience[2] = PlayerPrefs.GetInt("Conscience_Middle");
		}
		else
			return;

		if(PlayerPrefs.HasKey("Conscience_High"))
		{
			Conscience[3] = PlayerPrefs.GetInt("Conscience_High");
		}
		else
			return;

		if(PlayerPrefs.HasKey("Conscience_Graduate"))
		{
			Conscience[4] = PlayerPrefs.GetInt("Conscience_Graduate");
		}
		else
			return;
	}

	private void StageLockAndShowStartConscience()
	{
		for(int i = 0; i < Conscience.Length; i++)
		{
			if(Conscience[i].HasValue)
			{
				StageButton[i].GetComponent<Button>().interactable = true;
				StageButton[i].GetComponent<Image>().color = new Color (1,1,1,0);
				StageConscience[i].text = Conscience[i].Value.ToString();
			}
			else
			{
				StageButton[i].GetComponent<Button>().interactable = false;
				StageButton[i].GetComponent<Image>().color = new Color (1, 1, 1, 0.8f);
				StageConscience[i].text = "";
			}
		}
	}
}